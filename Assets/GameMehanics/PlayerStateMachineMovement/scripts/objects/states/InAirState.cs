using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public class InAirState : PlayerState { // в воздухе

		private float _startWallJumpCoyoteTime;
		private int _inputX;// ввод по оси X
		private bool _jumpButton, _jumpButtonStop, _grabButton, _dashButton;// кнопки прыжка, остановки прыжка, захвата стены и рывка
		private bool _isGrounded;// касаемся земли
		private bool _isTouchingWall, _isTouchingWallBack, _isTouchingLedge;// касаемся стены спереди и сзади касаемся ли уступа
		private bool _coyoteTime, _wallJumpCoyoteTime;
		private bool _isJumping;// прыгаем
		private bool _oldIsTouchingWall, _oldIsTouchingWallBack;

		public InAirState(Player player, string nameState) : base(player, nameState) { }

		public override void DoChecks() {
			base.DoChecks();

			_oldIsTouchingWall = _isTouchingWall;
			_oldIsTouchingWallBack = _isTouchingWallBack;

			_isGrounded = Player.CheckIfGrounded();
			_isTouchingWall = Player.CheckIfTouchingWall();
			_isTouchingWallBack = Player.CheckIfTouchingWallBack();
			_isTouchingLedge = Player.CheckIfTouchingLedge();

			if (_isTouchingWall && _isTouchingLedge == false) // если касаемся стены и не касаемся уступа
				Player.LedgeClimb.SetDetectedPosition(Player.Position);// запомним позицию игрока

			if (_wallJumpCoyoteTime == false && _isTouchingWall == false && _isTouchingWallBack == false && (_oldIsTouchingWall || _oldIsTouchingWallBack))
				StartWallJumpCoyoteTime();
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			CheckCoyoteTime();
			CheckWallJumpCoyoteTime();

			_inputX = Player.InputHandler.Movement.x;
			_jumpButton = Player.InputHandler.JumpButton;
			_jumpButtonStop = Player.InputHandler.JumpButtonStop;
			_grabButton = Player.InputHandler.GrabButton;
			_dashButton = Player.InputHandler.DashButton;

			CheckJumpMultiplier();// гашение скорости прыжка, если не до конца нажал прыжок

			if (_isGrounded && Player.CurrentVelocity.y < .01f) { // если касаемся земли и падаем
				StateMachine.ChangeState(Player.Land);

				return;
				}

			if (_isTouchingWall && _isTouchingLedge == false && _isGrounded == false) { // касаемся стены и не касаемся уступа и не касаемся земли
				StateMachine.ChangeState(Player.LedgeClimb);

				return;
				}

			if (_jumpButton && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime)) { // прыжок и касание стены
				StopWallJumpCoyoteTime();

				_isTouchingWall = Player.CheckIfTouchingWall();

				Player.WallJump.DetermineWallJumpDirection(_isTouchingWall);
				StateMachine.ChangeState(Player.WallJump);

				return;
				}

			if (_jumpButton && Player.Jump.CanJump) { // прыжок и могу прыгнуть
				StateMachine.ChangeState(Player.Jump);

				return;
				}

			if (_isTouchingWall && _grabButton && _isTouchingLedge) { // касаюсь стены и нажал захват стены и касаюсь уступа
				StateMachine.ChangeState(Player.WallGrab);

				return;
				}

			if (_isTouchingWall && _inputX == Player.FacingDirection && Player.CurrentVelocity.y <= 0) { // касаюсь стены и перемещаюсь в сторону взгляда и падаю
				StateMachine.ChangeState(Player.WallSlide);

				return;
				}

			if (_dashButton && Player.Dash.CheckIfCanDash()) { // рывок и могу ли зделать рывок
				StateMachine.ChangeState(Player.Dash);

				return;
				}

			Player.CheckIfShouldFlip(_inputX);// проверка поворота
			Player.SetXVelocity(_inputX * PlayerData.AirSpeed);// движение по горизонтали
			}
		public override void Exit() {
			base.Exit();

			_oldIsTouchingWall = false;
			_oldIsTouchingWallBack = false;
			_isTouchingWall = false;
			_isTouchingWallBack = false;
			}

		public void SetJumping() => _isJumping = true;
		public void StartCoyoteTime() {
			_coyoteTime = true;

			_startWallJumpCoyoteTime = Time.time;
			}
		public void StartWallJumpCoyoteTime() => _wallJumpCoyoteTime = true;
		public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;

		private void CheckCoyoteTime() {
			if (_coyoteTime && Time.time > StartTime + PlayerData.CoyoteTime) {
				_coyoteTime = false;

				Player.Jump.DecreaseAmountOfJumpsLeft();
				}
			}
		private void CheckWallJumpCoyoteTime() {
			if (_wallJumpCoyoteTime && Time.time > _startWallJumpCoyoteTime + PlayerData.CoyoteTime)
				StopWallJumpCoyoteTime();
			}
		private void CheckJumpMultiplier() {
			if (_isJumping) { // если прыгаю
				if (_jumpButtonStop) { // если отпустил кнопку прыжка
					Player.SetYVelocity(Player.CurrentVelocity.y * PlayerData.JumpHeightMultiplier);// уменьшим скорость прыжка

					_isJumping = false;// отменим прыжок
					}
				else if (Player.CurrentVelocity.y <= 0) // если начали падать
					_isJumping = false;// отменим прыжок
				}
			}

		}
	}