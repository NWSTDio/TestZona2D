namespace GameMehanics.PlayerStateMachineMovement {
	public class WallClimbState : TouchingWallStates { // подьем по стене

		public WallClimbState(Player player, string nameState) : base(player, nameState) { }

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			Player.SetYVelocity(PlayerData.WallClimbSpeed);// подьем по стене

			if (InputY != 1) { // если перестали нажимать кнопку вврх
				StateMachine.ChangeState(Player.WallGrab);

				return;
				}
			}

		}
	}