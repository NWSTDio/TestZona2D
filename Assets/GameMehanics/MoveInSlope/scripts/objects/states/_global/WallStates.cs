using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public abstract class WallStates : PlayerState<PlayerStates> {

        protected int InputX, InputY;// ввод по оси X и Y
        protected bool IsOnGround, IsTouchWall, IsTouchLedge;// проверка на земле, касаюсь стены, уступа
        protected bool GrabButton;// захват стены

        public WallStates(PlayerStates id, Player player, string nameState) : base(id, player, nameState) { }

        public override void Enter() {
            base.Enter();

            Player.ApplyFriction(PlayerData.NoFriction);// уберем трение
            }

        public override void LogicUpdate() {
            base.LogicUpdate();

            InputX = Player.InputHandler.Movement.x;
            InputY = Player.InputHandler.Movement.y;
            GrabButton = Player.InputHandler.GrabButton;

            if (IsOnGround && GrabButton == false) { // если на земле и нет захвата стены
                StateMachine.ChangeState(PlayerStates.IDLE);// стою

                return;
                }

            if (IsTouchWall == false || (InputX != Player.FacingDirection && GrabButton == false)) { // если не касаюсь стены или направление движение отличимое от направления взглада и нет захвата стены
                StateMachine.ChangeState(PlayerStates.FLY);// в воздухе

                return;
                }

            if (IsTouchLedge == false) { // если перестал касатся уступа
                StateMachine.ChangeState(PlayerStates.LEDGE_CLIMB);// подьем на уступ

                return;
                }
            }
        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            IsOnGround = Player.CheckIfOnGround();
            IsTouchWall = Player.CheckIfTouchWall();
            IsTouchLedge = Player.CheckIfTouchLedge();

            MenuUI.Instance.UpdateIsOnGround(IsOnGround);
            MenuUI.Instance.UpdateIsTouchWall(IsTouchWall);
            MenuUI.Instance.UpdateIsTouchLedge(IsTouchLedge);
            }

        }
    }
