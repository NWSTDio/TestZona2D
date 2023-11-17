namespace GameMehanics.MoveInSlope {
	public class LandState : GroundedStates { // режим приземления

		public LandState(Player player, string nameState) : base(PlayerStates.LAND, player, nameState) { }

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState)
				return;

			if (IsMovingInput) { // если движусь
				StateMachine.ChangeState(PlayerStates.WALK); // движение

				return;
				}
			}

		}
	}