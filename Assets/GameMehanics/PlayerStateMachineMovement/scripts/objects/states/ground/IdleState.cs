namespace GameMehanics.PlayerStateMachineMovement {
	public class IdleState : GroundedStates { // сотсояние бездействия

		public IdleState(Player player, string nameState) : base(player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.SetXVelocity(0);// стоим по горизонтали
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			if (IsMove) { // если начали движение
				StateMachine.ChangeState(RunButton ? Player.Run : Player.Walk);

				return;
				}

			if (InputY == -1) { // если нажали кнопку вниз
				StateMachine.ChangeState(Player.CrouchIdle);

				return;
				}
			}

		}
	}