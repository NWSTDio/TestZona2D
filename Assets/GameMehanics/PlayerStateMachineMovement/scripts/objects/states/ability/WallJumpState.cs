using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public class WallJumpState : AbilityStates { // прыжок от стены

		private int _wallJumpDirection;// направление взгляда при прыжке

		public WallJumpState(Player player, string nameState) : base(player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.InputHandler.UseJumpButton();// сообщим о использовании кнопки прыжка
			Player.Jump.ResetAmountOfJumpsLeft();
			Player.SetVelocity(PlayerData.WallJumpSpeed, PlayerData.WallJumpAngle, _wallJumpDirection);
			Player.CheckIfShouldFlip(_wallJumpDirection);// проверим поворот персонажа
			Player.Jump.DecreaseAmountOfJumpsLeft();
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (Time.time >= StartTime + PlayerData.WallJumpTime) // если время прыжка закончилось
				IsAbilityDone = true;// сообщим об окончании способности
			}

		public void DetermineWallJumpDirection(bool isTouchingWall) => _wallJumpDirection = isTouchingWall ? -Player.FacingDirection : Player.FacingDirection;

		}
	}