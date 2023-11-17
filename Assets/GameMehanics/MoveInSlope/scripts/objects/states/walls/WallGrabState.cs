using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public class WallGrabState : WallStates { // захват стены

        private Vector2 _holdPosition;// позиция удержания игрока

        public WallGrabState(Player player, string nameState) : base(PlayerStates.WALL_GRAB, player, nameState) { }

        public override void Enter() {
            base.Enter();

            _holdPosition = Player.Position;

            HoldPosition();// удержим игрока

            MenuUI.Instance.UpdateKeysInfo("Удерживайте [Q] чтобы не упасть [W|S] для движения");
            }
        public override void LogicUpdate() {
            base.LogicUpdate();

            if (IsExitingState)
                return;

            HoldPosition();// удержим игрока

            if (InputY > 0) { // если подьем
                StateMachine.ChangeState(PlayerStates.WALL_CLIMB);

                return;
                }

            if (InputY < 0 || GrabButton == false) { // если спуск или отпустили клавишу захвата
                StateMachine.ChangeState(PlayerStates.WALL_SLIDE);

                return;
                }
            }

        private void HoldPosition() {
            Player.SetPosition(_holdPosition);

            Player.FreezeVelocity();
            }

        }
    }
