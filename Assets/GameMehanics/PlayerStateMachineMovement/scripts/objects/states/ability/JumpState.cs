namespace GameMehanics.PlayerStateMachineMovement {
	public class JumpState : AbilityStates {

		private int _amountOfJumpsLeft;// количество разрешенных прыжков

		public bool CanJump => _amountOfJumpsLeft > 0;// можно ли прыгать

		public JumpState(Player player, string nameState) : base(player, nameState) {
			ResetAmountOfJumpsLeft();// вернем количество прыжков
			}

		public override void Enter() {
			base.Enter();

			Player.InputHandler.UseJumpButton();// используем кнопку прыжка
			Player.SetYVelocity(Player.PlayerData.JumpForce);// прыгнем
			IsAbilityDone = true;// сообщим о звершении способности

			DecreaseAmountOfJumpsLeft();// ум. количество разрешенных прыжков

			Player.InAir.SetJumping();// сообщим что прыгнули
			}

		public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.AmountOfJumps;
		public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;

		}
	}