using UnityEngine;

namespace GameMehanics.MoveInSlope {
	public class IdleState : GroundedStates { // режим стою

		public IdleState(Player player, string nameState) : base(PlayerStates.IDLE, player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.FreezeVelocity();// заморозим скорость
			Player.ApplyFriction(PlayerData.FullFriction);// установим трение
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState)
				return;

			if (IsMovingInput) { // если движемся
				StateMachine.ChangeState(RunButton ? PlayerStates.RUN : PlayerStates.WALK);

				return;
				}

			if (Player.IsLadder && Player.IsCrouch == false) // если у лестницы и не приседаю
				MenuUI.Instance.UpdateKeysInfo("Нажмите [Q] для взаимодействия");
			else if (IsTouchWall && Player.IsLadder == false) // если у сетны и нет рядом лестницы
				MenuUI.Instance.UpdateKeysInfo("Нажмите [Q] чтобы поднятся по стене");
			else
				MenuUI.Instance.ClearKeysInfo();

			if (Player.IsLadder && GrabButton && Player.IsCrouch == false) { // если у лестницы и захват и не приседаю
				StateMachine.ChangeState(PlayerStates.LADDER_IDLE);// режим перемещение по лестнице

				return;
				}
			}

		public override void Exit() {
			base.Exit();

			MenuUI.Instance.ClearKeysInfo();
			}

		}
	}