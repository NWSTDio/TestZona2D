using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public class WallGrabState : TouchingWallStates { // захавт стены

		private Vector2 _holdPosition;

		public WallGrabState(Player player, string nameState) : base(player, nameState) { }

		public override void Enter() {
			base.Enter();

			_holdPosition = Player.Position;// позиция заморозки координат персонажа

			HoldPosition();// остановим персонажа
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState) // если произошел выход в супер состоянии
				return;

			HoldPosition();// остановим персонажа

			if (InputY > 0) { // если нажали вверх
				StateMachine.ChangeState(Player.WallClimb);

				return;
				}

			if (InputY < 0 || GrabButton == false) { // если нажали вниз или отпустили кнопку захвата стены
				StateMachine.ChangeState(Player.WallSlide);

				return;
				}
			}

		private void HoldPosition() {
			Player.transform.position = _holdPosition;

			Player.ChangeVelocity(0, 0);
			} // удержание игрока в начальной позиции

		}
	}