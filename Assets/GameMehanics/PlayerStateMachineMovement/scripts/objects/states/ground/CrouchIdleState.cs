namespace GameMehanics.PlayerStateMachineMovement {
	public class CrouchIdleState : GroundedStates { // прысели

		public CrouchIdleState(Player player, string nameState) : base(player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.ChangeVelocity(0, 0);// запретим движение
			Player.SetColliderHeight(PlayerData.CrouchColliderHeight);// сменим размер колайдера
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			if (IsMove) { // если движемся
				StateMachine.ChangeState(Player.CrouchWalk);

				return;
				}

			if (InputY != -1 && IsTouchingCeiling == false) { // если отпустили кнопку вниз и нет над головой потолка
				StateMachine.ChangeState(Player.Idle);

				return;
				}
			}

		public override void Exit() {
			base.Exit();

			Player.SetColliderHeight(PlayerData.StandColliderHeight);// вернем обычный размер колайдера
			}

		}
	}