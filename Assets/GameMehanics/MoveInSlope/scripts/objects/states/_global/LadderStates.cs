namespace GameMehanics.MoveInSlope {
    public abstract class LadderStates : PlayerState<PlayerStates> { // состояние на лестнице

        protected int InputX, InputY;// ввод по оси X и Y
        protected bool JumpButton;// кнокпа прыжка
        protected bool IsOnGround;// проверка на земле

        protected bool IsMovingInput => InputX != 0;// движусь ли по оси X

        public LadderStates(PlayerStates id, Player player, string nameState) : base(id, player, nameState) { }

        public override void LogicUpdate() {
            base.LogicUpdate();

            InputX = Player.InputHandler.Movement.x;
            InputY = Player.InputHandler.Movement.y;
            JumpButton = Player.InputHandler.JumpButton;

            if (IsMovingInput)
                Player.CheckIfShouldFlip(InputX);// поворот игрока
            }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            IsOnGround = Player.CheckIfOnGround();

            MenuUI.Instance.UpdateIsOnGround(IsOnGround);
            }

        }
    }