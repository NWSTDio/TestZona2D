namespace GameMehanics.MoveInSlope {
	public class WalkState : GroundedStates { // режим ходьбы

		public WalkState(Player player, string nameState) : base(PlayerStates.WALK, player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.ApplyFriction(PlayerData.NoFriction);// уберем трение
			}
		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState)
				return;

			if (IsMovingInput == false) { // если прекратили движение
				StateMachine.ChangeState(PlayerStates.IDLE);

				return;
				}

			if (RunButton) { // если начали бег
				StateMachine.ChangeState(PlayerStates.RUN);

				return;
				}

			Player.SetXVelocity((Player.IsCrouch ? PlayerData.CrouchSpeed : PlayerData.WalkSpeed) * Player.FacingDirection);
			}

		}
	}