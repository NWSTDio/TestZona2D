namespace GameMehanics.MoveInSlope {
	public class JumpState : AbilityStates {

		private int _amountOfJumpsLeft;// количество разрешенных прыжков

		public bool CanJump => _amountOfJumpsLeft > 0;// можно ли прыгать

		public JumpState(Player player, string nameState) : base(PlayerStates.JUMP, player, nameState) {
			ResetAmountOfJumpsLeft();// вернем количество прыжков
			}

		public override void Enter() {
			base.Enter();

			IsExitAbility = true;// сообщим о звершении способности

			Player.InputHandler.UseJumpButton();// используем кнопку прыжка
			Player.SetYVelocity(PlayerData.JumpSpeed);// прыгнем

			DecreaseAmountOfJumpsLeft();// ум. количество разрешенных прыжков

			((FlyState)StateMachine.GetState(PlayerStates.FLY)).SetJumping();// сообщим что прыгнули

			StateMachine.ChangeState(PlayerStates.FLY);
			}

		public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.AmountOfJumps;
		public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;

		}
	}