using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public class LadderWalkState : LadderStates { // ходьба по горизонтали на лестнице]

        public LadderWalkState(Player player, string nameState) : base(PlayerStates.LADDER_WALK, player, nameState) { }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (Player.IsLadder == false) { // если лестница закончилась
                Player.FinishLadderMode();// отключим режим перемещения по лестнице

                if (Player.IsFalling) { // если игрок падает
                    StateMachine.ChangeState(PlayerStates.FLY);// ввоздухе

                    return;
                    }

                StateMachine.ChangeState(IsMovingInput ? PlayerStates.WALK : PlayerStates.IDLE);

                return;
                }

            Player.SetXVelocity(PlayerData.CrouchSpeed * Player.FacingDirection);
            }

        }
    }