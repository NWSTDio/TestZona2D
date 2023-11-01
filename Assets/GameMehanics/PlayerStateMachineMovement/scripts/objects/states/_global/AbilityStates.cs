namespace GameMehanics.PlayerStateMachineMovement {
	public abstract class AbilityStates : PlayerState { // состояние умений игрока

		protected bool IsAbilityDone;// для проверки выполнилось ли умение

		private bool _isGrounded;// касаемся ли земли

		protected AbilityStates(Player player, string nameState) : base(player, nameState) { }

		public override void DoChecks() {
			base.DoChecks();

			_isGrounded = Player.CheckIfGrounded();// проверка касаемся ли земли
			}

		public override void Enter() {
			base.Enter();

			IsAbilityDone = false;// умение не выполнено
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsAbilityDone) { // если умение выполнено
				StateMachine.ChangeState(_isGrounded && Player.CurrentVelocity.y < .01f ? Player.Idle : Player.InAir);

				return;
				}
			}

		}
	}