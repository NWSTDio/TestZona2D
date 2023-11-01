namespace GameMehanics.PlayerStateMachineMovement {
	public class RunState : GroundedStates { // бег

		public RunState(Player player, string nameState) : base(player, nameState) { }

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			Player.SetXVelocity(PlayerData.RunSpeed * InputX);// движение по горизонтали

			if (IsMove == false) { // если прекратили движение
				StateMachine.ChangeState(Player.Idle);

				return;
				}

			if (RunButton == false) { // если прекратили бег
				StateMachine.ChangeState(Player.Walk);

				return;
				}
			}

		}
	}