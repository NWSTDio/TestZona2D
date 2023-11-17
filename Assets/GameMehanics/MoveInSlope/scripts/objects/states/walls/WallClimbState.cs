namespace GameMehanics.MoveInSlope {
    public class WallClimbState : WallStates { // подьем на гору

        public WallClimbState(Player player, string nameState) : base(PlayerStates.WALL_CLIMB, player, nameState) { }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (IsExitingState)
                return;

            if (GrabButton == false) { // если прекратили захват
                StateMachine.ChangeState(PlayerStates.WALL_SLIDE);

                return;
                }

            if (InputY != 1) { // если прекратили подьем
                StateMachine.ChangeState(PlayerStates.WALL_GRAB);// захват стены

                return;
                }

            Player.SetYVelocity(PlayerData.WallClimbSpeed);
            }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (IsTouchWall && IsTouchLedge == false) // если касаемся стены и не касаемся уступа
                ((LedgeClimbState)StateMachine.GetState(PlayerStates.LEDGE_CLIMB)).SetDetectedPosition(Player.Position);
            }

        }
    }