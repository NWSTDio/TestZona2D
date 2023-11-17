using UnityEngine;

namespace GameMehanics.MoveInSlope {
	public abstract class GroundedStates : PlayerState<PlayerStates> { // на земле

		protected int InputX, InputY;// ввод по оси X и Y
		protected bool RunButton, JumpButton, GrabButton;// кнокпи бега, прыжка, захвата стены
		protected bool IsOnGround, IsOnSlope, IsTouchWall, IsTouchLedge, IsTouchCeiling;// проверка на земле, на наклонной поверхности, касаюсь стены, уступа или потолка

		private JumpState _jump;// состояние прыжка

		protected bool IsMovingInput => InputX != 0;// движусь ли по оси X

		protected GroundedStates(PlayerStates id, Player player, string nameState) : base(id, player, nameState) { }

		public override void Enter() {
			base.Enter();

			_jump ??= (JumpState)StateMachine.GetState(PlayerStates.JUMP);// если прыжок был ране равен null то проинициализируем его

			_jump.ResetAmountOfJumpsLeft();// обновим количество прыжков
			}

		public override void LogicUpdate() {
			base.LogicUpdate();

			InputX = Player.InputHandler.Movement.x;
			InputY = Player.InputHandler.Movement.y;
			RunButton = Player.InputHandler.RunButton;
			JumpButton = Player.InputHandler.JumpButton;
			GrabButton = Player.InputHandler.GrabButton;

			if (IsMovingInput)
				Player.CheckIfShouldFlip(InputX);// поворот игрока

			if (InputY == -1 && Player.IsCrouch == false)
				Player.StartCrouch();// нач. прыседание
			else if (InputY != -1 && Player.IsCrouch && IsTouchCeiling == false)
				Player.FinishCrouch();// законч. прыседание

			if (IsOnGround == false) { // если под ногами нет земли
				StateMachine.ChangeState(PlayerStates.FLY);// в воздухе

				return;
				}

			if (JumpButton && _jump.CanJump && Player.IsCrouch == false) { // если прыжок и могу прыгнуть и не приседаю
				StateMachine.ChangeState(PlayerStates.JUMP);// прыжок

				return;
				}
			if (IsTouchWall && GrabButton && IsTouchLedge && Player.IsCrouch == false && Player.IsLadder == false) { // если касаюсь стены, захватываю стену и касаюсь стены но как уступа и не прыседаю и нет рядом лестницы
				StateMachine.ChangeState(PlayerStates.WALL_GRAB);// захват стены

				return;
				}

			if (IsOnSlope) { // если на наклонной поверхности
				if (IsMovingInput) { // если в движении
					StateMachine.ChangeState(RunButton ? PlayerStates.SLOPE_RUN : PlayerStates.SLOPE_WALK);// наклонные поверхности

					return;
					}

				StateMachine.ChangeState(PlayerStates.SLOPE_IDLE);// наклонная поверхность но стою

				return;
				}
			}

		public override void PhysicsUpdate() {
			base.PhysicsUpdate();

			IsOnGround = Player.CheckIfOnGround();
			IsOnSlope = Player.CheckIfOnSlope();
			IsTouchWall = Player.CheckIfTouchWall();
			IsTouchLedge = Player.CheckIfTouchLedge();
			IsTouchCeiling = Player.CheckForCeiling();

			MenuUI.Instance.UpdateIsOnGround(IsOnGround);
			MenuUI.Instance.UpdateIsOnSlope(IsOnSlope);
			MenuUI.Instance.UpdateIsTouchWall(IsTouchWall);
			MenuUI.Instance.UpdateIsTouchLedge(IsTouchLedge);
			MenuUI.Instance.UpdateIsTouchCeiling(IsTouchCeiling);
			}

		}
	}