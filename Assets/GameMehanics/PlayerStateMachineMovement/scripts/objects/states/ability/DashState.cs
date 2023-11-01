using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public class DashState : AbilityStates { // состояние рывок

		public bool CanDash { get; private set; } // могу ли делать рывок

		private Vector2 _dashDirection;// направление рывка
		private Vector2 _dashDirectionInput;// данные ввода направления рывка
		private float _lastDashTime;// время послед. рывка
		private bool _isHolding;// режим затормаживания времени для выбора направления рывка
		private bool _dashButtonStop;// отжата ли кнопка рывка

		public DashState(Player player, string nameState) : base(player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.InputHandler.UseDashButton();// юзаем кнопку рывка
			Player.DashDirectionIndicator.gameObject.SetActive(true);// показ индикатора рывка

			CanDash = false;// не могу делать рывок
			_isHolding = true;// режим выбора направления для рывка
			_dashDirection = Player.transform.right;// стартовое направление для рывка

			Time.timeScale = PlayerData.HoldTimeScale;// ум. скорость течения времени
			StartTime = Time.unscaledTime;// запомним время начала рывка не зависимо от скорости времени
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			if (_isHolding) { // режим выбора направления для рывка
				_dashButtonStop = Player.InputHandler.DashButtonStop;// для проверки отпустили кнопку рывка
				_dashDirectionInput = Player.InputHandler.DashDirection;// направление для рывка

				if (_dashDirectionInput != Vector2.zero) // если оно не нулевое
					_dashDirection = _dashDirectionInput.normalized;

				float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);// угол рывка
				Player.DashDirectionIndicator.rotation = Quaternion.Euler(0, 0, angle);// повернем индикатор рывка

				if (_dashButtonStop || Time.unscaledTime >= StartTime + PlayerData.MaxHoldTime) { // если отменен рывок или закончилось время для выбора направления рывка
					_isHolding = false;

					Time.timeScale = 1f;// вернем нормальную скорость течения времени
					StartTime = Time.time;

					Player.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));// проверим возможность поворота игрока
					Player.Body.drag = PlayerData.Drag;
					Player.SetVelocity(PlayerData.DashSpeed, _dashDirection);// сделаем рывок
					Player.DashDirectionIndicator.gameObject.SetActive(false);// спрячем индикатор рывка
					}
				}
			else {
				Player.SetVelocity(PlayerData.DashSpeed, _dashDirection); // делаем рывок

				if (Time.time >= StartTime + PlayerData.DashTime) { // если закончилось время рывка
					Player.Body.drag = 0;
					IsAbilityDone = true;// выход из состояния способности
					_lastDashTime = Time.time;// запомним время последнего рывка
					}
				}
			}

		public override void Exit() {
			base.Exit();

			if (Player.CurrentVelocity.y > 0) // если поднимаемся вверх
				Player.SetYVelocity(Player.CurrentVelocity.y * PlayerData.DashEndYMultiplier);// ум. скорость подьема вверх
			}

		public bool CheckIfCanDash() => CanDash && Time.time >= _lastDashTime + PlayerData.DashCooldown;// можно ли делать рывок
		public void ResetCanDash() => CanDash = true;// разрешим рывок

		}
	}