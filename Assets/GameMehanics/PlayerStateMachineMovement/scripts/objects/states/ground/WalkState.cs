namespace GameMehanics.PlayerStateMachineMovement {
	public class WalkState : GroundedStates { // ходьба

		public WalkState(Player player, string nameState) : base(player, nameState) { }

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			Player.SetXVelocity(PlayerData.WalkSpeed * InputX);// движемся по горизонтали

			if (IsMove == false) { // прекратили движение
				StateMachine.ChangeState(Player.Idle);

				return;
				}

			if (RunButton) { // начали бег
				StateMachine.ChangeState(Player.Run);

				return;
				}

			if (InputY == -1) { // нажали кнопку вниз
				StateMachine.ChangeState(Player.CrouchWalk);

				return;
				}
			}

		}
	}