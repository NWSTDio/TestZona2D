namespace GameMehanics.MoveInSlope {
	public abstract class AbilityStates : PlayerState<PlayerStates> { // способность

		protected bool IsExitAbility;// выход из способности

		public AbilityStates(PlayerStates id, Player player, string nameState) : base(id, player, nameState) { }

		public override void Enter() {
			base.Enter();

			IsExitAbility = false;
			}

		}
	}