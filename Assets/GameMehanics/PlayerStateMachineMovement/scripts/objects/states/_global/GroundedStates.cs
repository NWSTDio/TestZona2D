namespace GameMehanics.PlayerStateMachineMovement {
	public abstract class GroundedStates : PlayerState { // состояние нахождения на земле

		protected int InputX, InputY;// ввод данных по осям X и Y
		protected bool RunButton;// кнопка бега
		protected bool IsTouchingCeiling;// проверка касания потолка

		private bool _jumpButton, _grabButton, _dashButton;// нажатие кнопок прыжка, захвата стены и рывка
		private bool _isGrounded, _isTouchingWall, _isTouchingLedge;// проерка касаний земли, стены и выступа

		protected bool IsMove => InputX != 0;// движется ли игрок

		protected GroundedStates(Player player, string nameState) : base(player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.Jump.ResetAmountOfJumpsLeft();// обновим количество прыжков
			Player.Dash.ResetCanDash();// сбросим возможность рывка
			}
		public override void DoChecks() {
			base.DoChecks();

			_isGrounded = Player.CheckIfGrounded();// касаемся ли земли
			_isTouchingWall = Player.CheckIfTouchingWall();// касаемся ли стены
			_isTouchingLedge = Player.CheckIfTouchingLedge();// касаемся ли выступа

			IsTouchingCeiling = Player.CheckForCeiling();// касаемся ли потолка
			}
		public override void LogicUpdate() {
			base.LogicUpdate();

			InputX = Player.InputHandler.Movement.x;
			InputY = Player.InputHandler.Movement.y;
			RunButton = Player.InputHandler.RunButton;

			_jumpButton = Player.InputHandler.JumpButton;
			_grabButton = Player.InputHandler.GrabButton;
			_dashButton = Player.InputHandler.DashButton;

			if (_jumpButton && Player.Jump.CanJump) { // если прыжок и можно прыгать
				StateMachine.ChangeState(Player.Jump);

				return;
				}

			if (_isGrounded == false) { // если не касаемся земли
				Player.InAir.StartCoyoteTime();
				StateMachine.ChangeState(Player.InAir);

				return;
				}

			if (_isTouchingWall && _grabButton && _isTouchingLedge) { // если касаемся стены и захват стены и не начался выступ
				StateMachine.ChangeState(Player.WallGrab);

				return;
				}

			if (_dashButton && Player.Dash.CheckIfCanDash() && IsTouchingCeiling == false) { // если рывок и можно рывок и не касаемся потолка
				StateMachine.ChangeState(Player.Dash);

				return;
				}

			if (IsMove) // если в движении
				Player.CheckIfShouldFlip(InputX);// проверим возможность поворота игрока
			}

		}
	}