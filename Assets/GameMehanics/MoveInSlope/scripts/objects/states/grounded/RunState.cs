namespace GameMehanics.MoveInSlope {
	public class RunState : GroundedStates { // режим бега

		public RunState(Player player, string nameState) : base(PlayerStates.RUN, player, nameState) { }

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

			if (RunButton == false) { // если прекратили бег
				StateMachine.ChangeState(PlayerStates.WALK);

				return;
				}

			Player.SetXVelocity((Player.IsCrouch ? PlayerData.CrouchSpeed : PlayerData.RunSpeed) * Player.FacingDirection);
			}

		}
	}