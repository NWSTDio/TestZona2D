using UnityEngine;

namespace GameMehanics.MoveInSlope {
	public abstract class SlopesStates : PlayerState<PlayerStates> { // наклонные поверхности

		protected RaycastHit2D SlopeHit;// поверхность
		protected Vector2 SlopeNormal;// нормаль поверхности (для движения)
		protected float SlopeAngle;// угол поверхности
		protected int InputX, InputY;// ввод по оси X и Y
		protected bool RunButton, JumpButton, GrabButton;// кнокпи бега, прыжка, захвата стены
		protected bool IsOnGround, IsOnSlope, IsTouchWall, IsTouchLedge;// проверка на земле, на наклонной поверхности, касаюсь стены, уступа

		private JumpState _jump;// состояние прыжка

		protected bool IsMove => InputX != 0;// движусь ли по оси X
		protected bool CanWalkOnSlope => SlopeAngle <= PlayerData.MaxSlopeAngle;// могу ли двигатся по наклонной поверхности

		protected SlopesStates(PlayerStates id, Player player, string nameState) : base(id, player, nameState) { }

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

			if (IsMove)
				Player.CheckIfShouldFlip(InputX);// поворот игрока

			if (InputY == -1 && Player.IsCrouch == false)
				Player.StartCrouch();// нач. прыседание
			else if (InputY != -1 && Player.IsCrouch)
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

			if (IsOnSlope == false) { // если ровная поверхность
				if (IsMove) { // если движемся
					StateMachine.ChangeState(RunButton ? PlayerStates.RUN : PlayerStates.WALK);

					return;
					}

				StateMachine.ChangeState(PlayerStates.IDLE);// стою

				return;
				}
			}
		public override void PhysicsUpdate() {
			base.PhysicsUpdate();

			IsOnGround = Player.CheckIfOnGround();
			IsOnSlope = Player.CheckIfOnSlope();
			IsTouchWall = Player.CheckIfTouchWall();
			IsTouchLedge = Player.CheckIfTouchLedge();

			if (IsOnSlope) { // если наклонная поверхность есть
				SlopeHit = Player.GetHitSlope();// получим поверхность
				SlopeAngle = Vector2.Angle(SlopeHit.normal, Vector2.up);// получим угол поверхности
				SlopeNormal = Vector2.Perpendicular(SlopeHit.normal).normalized;// получим перпендикуляр поверхности

				MenuUI.Instance.UpdateSlopeAngle(SlopeAngle);
				}
			else
				MenuUI.Instance.UpdateSlopeAngle(0);

			MenuUI.Instance.UpdateIsOnGround(IsOnGround);
			MenuUI.Instance.UpdateIsOnSlope(IsOnSlope);
			MenuUI.Instance.UpdateIsTouchWall(IsTouchWall);
			MenuUI.Instance.UpdateIsTouchLedge(IsTouchLedge);
			}

		}
	}