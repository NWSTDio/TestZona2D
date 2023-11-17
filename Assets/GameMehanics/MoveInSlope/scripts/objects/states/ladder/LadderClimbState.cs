using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public class LadderClimbState : LadderStates { // подьем по лестнице

        public LadderClimbState(Player player, string nameState) : base(PlayerStates.LADDER_CLIMB, player, nameState) { }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (Player.IsLadder == false) { // если нет лестницы
                Player.FinishLadderMode();// отключим режим движения по лестницы

                if (IsMovingInput) { // если в движении
                    StateMachine.ChangeState(PlayerStates.WALK);// ходьба

                    return;
                    }

                StateMachine.ChangeState(PlayerStates.IDLE);// стою

                return;
                }

            if (InputY == 0) { // не движемся по оси Y
                StateMachine.ChangeState(PlayerStates.LADDER_IDLE);

                return;
                }

            if (InputY < 0) { // спуск
                StateMachine.ChangeState(PlayerStates.LADDER_SLIDE);

                return;
                }

            Player.SetYVelocity(PlayerData.CrouchSpeed);
            }

        }
    }