namespace GameMehanics.PlayerStateMachineMovement {
	public class WallSlideState : TouchingWallStates { // скольжение по стене

		public WallSlideState(Player player, string nameState) : base(player, nameState) { }

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			Player.SetYVelocity(-PlayerData.WallSlideSpeed);// скользим

			if (GrabButton && InputY == 0) { // если нажата кнопка захвата стены и нет нажатий движения по вертикали
				StateMachine.ChangeState(Player.WallGrab);

				return;
				}
			}

		}
	}