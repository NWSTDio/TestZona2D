namespace GameMehanics.PlayerStateMachineMovement {
	public class CrouchWalkState : GroundedStates { // ходьба прысядки

		public CrouchWalkState(Player player, string nameState) : base(player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.SetColliderHeight(PlayerData.CrouchColliderHeight);// сменим размер колайдера
			}
		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			Player.SetXVelocity(PlayerData.CrouchSpeed * Player.FacingDirection);// движемся по горизонтали

			if (IsMove == false) { // если не движемся
				StateMachine.ChangeState(Player.CrouchIdle);

				return;
				}

			if (InputY != -1 && IsTouchingCeiling == false) { // если отпустили клавишу вниз и не мешает потолок
				StateMachine.ChangeState(Player.Walk);

				return;
				}
			}

		public override void Exit() {
			base.Exit();

			Player.SetColliderHeight(PlayerData.StandColliderHeight);// вернем обычную высоту колайдера
			}

		}
	}