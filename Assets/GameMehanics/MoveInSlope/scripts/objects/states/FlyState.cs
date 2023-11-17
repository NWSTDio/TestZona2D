using UnityEngine;

namespace GameMehanics.MoveInSlope {
	public class FlyState : PlayerState<PlayerStates> { // в воздухе

		private bool _isOnGround, _isOnSlope, _isTouchWall, _isTouchLedge;// проверка на земле, на наклонной поверхности, касаюсь стены, уступа
		private int _inputX;// ввод по оси X
		private bool _isJumping;// прыгаем
		private bool _jumpButtonStop;// остановили прыжок
		private bool _grabButton;// захват

		public FlyState(Player player, string nameState) : base(PlayerStates.FLY, player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.ApplyFriction(PlayerData.NoFriction);// сбросим трение

			Player.FinishCrouch();// уберем режим приседания если есть

			MenuUI.Instance.ClearKeysInfo();
			}
		public override void LogicUpdate() {
			base.LogicUpdate();

			_inputX = Player.InputHandler.Movement.x;
			_jumpButtonStop = Player.InputHandler.JumpButtonStop;
			_grabButton = Player.InputHandler.GrabButton;

			CheckJumpMultiplier();// сократим высоту прыжка
			Player.CheckIfShouldFlip(_inputX);// проверка поворота

			if (_isOnGround && _isJumping == false) { // если на земле и не в прыжке
				StateMachine.ChangeState(_inputX == 0 ? PlayerStates.IDLE : PlayerStates.WALK);

				return;
				}

			if (_isOnGround == false && _isTouchWall && _isTouchLedge == false) { // если нет земли и касаемся стены и не касаемся уступа
				StateMachine.ChangeState(PlayerStates.LEDGE_CLIMB);// подьем на уступ

				return;
				}

			if (_isTouchWall && _grabButton && _isTouchLedge) { // если касаемся стены и захват и касаемся уступа
				StateMachine.ChangeState(PlayerStates.WALL_GRAB);// захват стены

				return;
				}

			if (_isTouchWall && _inputX == Player.FacingDirection && Player.IsFalling) { // если касаемся стены и смотрим на нее и падаем
				StateMachine.ChangeState(PlayerStates.WALL_SLIDE);// скользим по стене

				return;
				}

			Player.SetXVelocity(_inputX * PlayerData.AirSpeed);// движение по горизонтали
			}
		public override void PhysicsUpdate() {
			base.PhysicsUpdate();

			_isOnGround = Player.CheckIfOnGround();
			_isOnSlope = Player.CheckIfOnSlope();
			_isTouchWall = Player.CheckIfTouchWall();
			_isTouchLedge = Player.CheckIfTouchLedge();

			if (_isTouchWall && _isTouchLedge == false) // если касаемся стены и прекратили касатся уступа
				((LedgeClimbState)StateMachine.GetState(PlayerStates.LEDGE_CLIMB)).SetDetectedPosition(Player.Position);

			MenuUI.Instance.UpdateIsOnGround(_isOnGround);
			MenuUI.Instance.UpdateIsOnSlope(_isOnSlope);
			MenuUI.Instance.UpdateIsTouchWall(_isTouchWall);
			MenuUI.Instance.UpdateIsTouchLedge(_isTouchLedge);
			}

		public void SetJumping() => _isJumping = true;// сообшим что прыгнули

		private void CheckJumpMultiplier() {
			if (_isJumping) { // если прыгаю
				if (_jumpButtonStop) { // если отпустил кнопку прыжка
					Player.SetYVelocity(Player.CurrentVelocity.y * PlayerData.JumpHeightMultiplier);// уменьшим скорость прыжка

					_isJumping = false;// отменим прыжок
					}
				else if (Player.IsFalling) // если начали падать
					_isJumping = false;// отменим прыжок
				}
			}

		}
	}