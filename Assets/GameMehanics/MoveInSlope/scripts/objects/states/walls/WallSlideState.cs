namespace GameMehanics.MoveInSlope {
    public class WallSlideState : WallStates { // спуск с горы

        public WallSlideState(Player player, string nameState) : base(PlayerStates.WALL_SLIDE, player, nameState) { }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (IsExitingState)
                return;

            if (GrabButton) {// если захват стены 
                if (InputY == 0) { // нет движения по вертикали
                    StateMachine.ChangeState(PlayerStates.WALL_GRAB);

                    return;
                    }

                if (InputY == 1) { // если подьем
                    StateMachine.ChangeState(PlayerStates.WALL_CLIMB);

                    return;
                    }
                }

            Player.SetYVelocity(-PlayerData.WallSlideSpeed);
            }

        }
    }