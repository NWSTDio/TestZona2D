namespace GameMehanics.PlayerStateMachineMovement {
	public abstract class TouchingWallStates : PlayerState { // состояние касания стены

		protected bool IsGrounded, IsTouchingWall, IsTouchingLedge;// если касаюсь земли, стены или выступа
		protected int InputX, InputY;// движение
		protected bool GrabButton, JumpButton;// захват стены или прыжок

		public TouchingWallStates(Player player, string nameState) : base(player, nameState) { }

		public override void DoChecks() {
			base.DoChecks();

			IsGrounded = Player.CheckIfGrounded();
			IsTouchingWall = Player.CheckIfTouchingWall();
			IsTouchingLedge = Player.CheckIfTouchingLedge();

			if (IsTouchingWall && IsTouchingLedge == false) // касание стены и выступ 
				Player.LedgeClimb.SetDetectedPosition(Player.Position);// запомним позицию игрока
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			InputX = Player.InputHandler.Movement.x;
			InputY = Player.InputHandler.Movement.y;
			GrabButton = Player.InputHandler.GrabButton;
			JumpButton = Player.InputHandler.JumpButton;

			if (JumpButton) { // если прыжок
				Player.WallJump.DetermineWallJumpDirection(IsTouchingWall);// получим направление прыжка
				StateMachine.ChangeState(Player.WallJump);

				return;
				}

			if (IsGrounded && GrabButton == false) { // если на земле и не захватываем стену
				StateMachine.ChangeState(Player.Idle);

				return;
				}

			if (IsTouchingWall == false || (InputX != Player.FacingDirection && GrabButton == false)) { // не касаемся стены или в другую сторону движемся от направления игрок и не захватывание стены
				StateMachine.ChangeState(Player.InAir);

				return;
				}

			if (IsTouchingWall && IsTouchingLedge == false) { // касаемся стены и начался выступ
				StateMachine.ChangeState(Player.LedgeClimb);

				return;
				}
			}

		}
	}