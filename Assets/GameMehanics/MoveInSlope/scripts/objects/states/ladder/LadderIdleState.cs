using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public class LadderIdleState : LadderStates { // стоим на лестнице

        public LadderIdleState(Player player, string nameState) : base(PlayerStates.LADDER_IDLE, player, nameState) { }

        public override void Enter() {
            base.Enter();

            Player.FreezeVelocity();// заморозим скорость
            Player.StartLadderMode();// старт режима лестницы

            if (Player.Ladder.FreezeXPosition) // если нельзя двигатся по оси X на лестнице
                Player.SetXPosition(Player.Ladder.transform.position.x);

            MenuUI.Instance.UpdateKeysInfo("Нажмите [Space] чтобы быстро спустится \n [W | S] для движения по вертикали" + (Player.Ladder.FreezeXPosition ? "" : " \n [A | D] для движение по горизонтали"));
            }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (InputY > 0) { // подьем
                StateMachine.ChangeState(PlayerStates.LADDER_CLIMB);

                return;
                }

            if (InputY < 0) { // спуск
                StateMachine.ChangeState(PlayerStates.LADDER_SLIDE);

                return;
                }

            if (IsMovingInput && Player.Ladder.FreezeXPosition == false) { // если есть движение и можно двигатся по оси X на лестнице
                StateMachine.ChangeState(PlayerStates.LADDER_WALK);// идем по горизонтали

                return;
                }

            if (JumpButton) { // если прижок
                Player.FinishLadderMode();// выключим режим лестницы
                Player.InputHandler.UseJumpButton();// сообщим что использовали прыжок

                StateMachine.ChangeState(PlayerStates.IDLE);// стою

                MenuUI.Instance.UpdateKeysInfo("Нажмите [Q] для взаимодействия");

                return;
                }
            }

        }
    }