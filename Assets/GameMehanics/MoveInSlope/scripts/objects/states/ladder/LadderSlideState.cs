using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public class LadderSlideState : LadderStates { // спуск с лестницы

        public LadderSlideState(Player player, string nameState) : base(PlayerStates.LADDER_SLIDE, player, nameState) { }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (InputY == 0) { // если прекратили спуск
                StateMachine.ChangeState(PlayerStates.LADDER_IDLE);

                return;
                }

            if (InputY > 0) { // если начали подьем
                StateMachine.ChangeState(PlayerStates.LADDER_CLIMB);

                return;
                }

            if (IsOnGround) { // если под ногами заемля
                Player.FinishLadderMode();// отключим режим движения 

                StateMachine.ChangeState(PlayerStates.IDLE);// стою
                MenuUI.Instance.UpdateKeysInfo("Нажмите [Q] для взаимодействия");

                return;
                }

            Player.SetYVelocity(-PlayerData.CrouchSpeed);
            }
        }
    }