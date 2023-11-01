namespace GameMehanics.PlayerStateMachineMovement {
	public class LandState : GroundedStates { // состояние касания земли после полета
		public LandState(Player player, string nameState) : base(player, nameState) { }

		public override void Enter() {
			base.Enter();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			StateMachine.ChangeState(Player.Idle);

			// доработать если будут анимации, эта анимация должна запускать код
			}
		}
	}