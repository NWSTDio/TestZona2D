using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public class TestState : PlayerState { // тестовое состояние

		public TestState(Player player, string nameState) : base(player, nameState) { }

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (Input.GetKeyDown(KeyCode.E)) {
				StateMachine.ChangeState(Player.Idle);

				return;
				}
			}

		}
	}