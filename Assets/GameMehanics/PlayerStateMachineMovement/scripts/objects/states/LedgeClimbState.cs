using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public class LedgeClimbState : PlayerState { // взбирание на уступ

		private Vector2 _detectedPosition;
		private Vector2 _cornerPosition;
		private Vector2 _startPosition, _stopPosition;

		private bool _isHanging, _isClimbing;
		private int _inputX, _inputY;
		private bool _jumpButton;
		private bool _isTouchingCeiling;
		private float _timer;

		public LedgeClimbState(Player player, string nameState) : base(player, nameState) { }

		public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;

		public override void Enter() {
			base.Enter();

			Player.ChangeVelocity(0, 0);// заморозим движение
			Player.transform.position = _detectedPosition;// установим начальную позицию игрока

			_cornerPosition = Player.DetermineCornerPosition();// позиция угла уступа

			_startPosition.Set(_cornerPosition.x - (Player.FacingDirection * PlayerData.StartOffset.x), _cornerPosition.y - PlayerData.StartOffset.y);
			_stopPosition.Set(_cornerPosition.x + (Player.FacingDirection * PlayerData.StopOffset.x), _cornerPosition.y + PlayerData.StopOffset.y);

			Player.transform.position = _startPosition;// установим игрока на стартовой позиции

			_timer = .5f;// время подьема
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (_timer > 0) // если рано подыматся
				_timer -= Time.deltaTime;
			else {
				if (_isHanging == false) // если еще не установили режим висения на уступе
					_isHanging = true;
				else {
					if (_isClimbing) { // если уже нажали подьем на уступ
						StateMachine.ChangeState(_isTouchingCeiling ? Player.CrouchIdle : Player.Idle);

						return;
						}
					}
				}

			_inputX = Player.InputHandler.Movement.x;
			_inputY = Player.InputHandler.Movement.y;
			_jumpButton = Player.InputHandler.JumpButton;

			Player.ChangeVelocity(0, 0);// запретим движение
			Player.transform.position = _startPosition;

			if (_inputX == Player.FacingDirection && _isHanging && _isClimbing == false) { // если движемся в направлении взгляда и держимся за уступ и еще не начали подьем
				CheckForSpace();// проверим можно ли пролезть на уступ

				_isClimbing = true;// начнем подьем
				_timer = .3f;// установим время подьема

				return;
				}

			if (_inputY == -1 && _isHanging && _isClimbing == false) { // если нажали вниз и держались за уступ и не начали подьем
				StateMachine.ChangeState(Player.InAir);

				return;
				}

			if (_jumpButton && _isClimbing == false) { // если нажали прыжок и не начали подьем
				Player.WallJump.DetermineWallJumpDirection(true);// получим направление прыжка
				StateMachine.ChangeState(Player.WallJump);

				return;
				}
			}

		public override void Exit() {
			base.Exit();

			_isHanging = false;

			if (_isClimbing) { // если был режим подьема
				_isClimbing = false;

				Player.transform.position = _stopPosition;// переместим в конечную позиию
				}
			}

		private void CheckForSpace() => _isTouchingCeiling = Physics2D.Raycast(_cornerPosition + (Vector2.up * .015f) + (Vector2.right * Player.FacingDirection * .015f), Vector2.up, PlayerData.StandColliderHeight, PlayerData.WhatIsGround);

		}
	}