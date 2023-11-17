using UnityEngine;

namespace GameMehanics.MoveInSlope {
	[RequireComponent(typeof(PlayerInputHandler), typeof(Rigidbody2D))]
	public class Player : MonoBehaviour {

		public PlayerStateMachine StateMachine { get; private set; } // состояния игрока

		public PlayerInputHandler InputHandler { get; private set; } // пользовательский ввод
		public Vector2 CurrentVelocity { get; private set; } // текущая скорость игрока
		public int FacingDirection { get; private set; } = 1;// направление взгляда
		public bool IsCrouch { get; private set; } // прыседает ли?

		[SerializeField] private PlayerData _data;// данные игрока
		[SerializeField] private Transform _groundCheck, _wallCheck, _ledgeCheck, _ceilingCheck;// точки для проверки

		private Rigidbody2D _body;
		private CapsuleCollider2D _collider;
		private MenuUI _menu;
		private JumpState _jump;// состояние прыжка
		private Vector3 _startPos;// позиция спавна
		private float _gravityScale;// сила гравитации
		public Ladder Ladder { get; private set; } // лестница

		public PlayerData PlayerData => _data;
		public Vector3 Position => transform.position;
		public bool IsFalling => CurrentVelocity.y.Round() <= 0;// падаю ли
		public bool IsLadder => Ladder != null;// есть ли лестница

		#region MonoBehaviour
		private void Awake() {
			StateMachine = new PlayerStateMachine();

			StateMachine.AddState(new IdleState(this, "idle"));// стою
			StateMachine.AddState(new WalkState(this, "walk"));// иду
			StateMachine.AddState(new RunState(this, "run"));// бегу

			StateMachine.AddState(new JumpState(this, "jump"));// прыгаю

			StateMachine.AddState(new FlyState(this, "fly"));// в воздухе

			StateMachine.AddState(new LandState(this, "land"));// приземляюсь

			StateMachine.AddState(new SlopeIdleState(this, "slope_idle"));// стою на наклонной поверхности
			StateMachine.AddState(new SlopeWalkState(this, "slope_walk"));// иду по наклонной поверхности
			StateMachine.AddState(new SlopeRunState(this, "slope_run"));// бегу по наклонной поверхности

			StateMachine.AddState(new WallGrabState(this, "wall_grab"));// захват стены
			StateMachine.AddState(new WallClimbState(this, "wall_climb"));// движение вверх по стене
			StateMachine.AddState(new WallSlideState(this, "wall_slide"));// скольжение по стене

			StateMachine.AddState(new LedgeClimbState(this, "ledge_climb"));// карабкание вверх

			StateMachine.AddState(new LadderIdleState(this, "ladder_idle"));// стою на лестнице
			StateMachine.AddState(new LadderWalkState(this, "ladder_walk"));// иду в бок по лестнице
			StateMachine.AddState(new LadderClimbState(this, "ladder_climb"));// подымаюсь по лестнице
			StateMachine.AddState(new LadderSlideState(this, "ladder_slide"));// спускаюсь с лестницы

			InputHandler = GetComponent<PlayerInputHandler>();
			_body = GetComponent<Rigidbody2D>();
			_collider = GetComponent<CapsuleCollider2D>();
			_startPos = Position;
			_gravityScale = _body.gravityScale;
			}
		private void Start() {
			_menu = MenuUI.Instance;

			StateMachine.ChangeState(PlayerStates.IDLE);
			_jump = (JumpState)StateMachine.GetState(PlayerStates.JUMP);

			CurrentVelocity = new Vector2();

			_menu.UpdateGravityScale(_body.gravityScale);
			_menu.UpdateIsTrigger(_collider.isTrigger);
			_menu.UpdateIsLadder(IsLadder);
			}
		private void Update() {
			if (Input.GetKey(KeyCode.R)) {
				RestartGame();

				return;
				}

			CurrentVelocity = _body.velocity;

			StateMachine.LogicUpdate();

			_menu.UpdateCanJump(_jump.CanJump);
			_menu.UpdatePosition(Position);
			}
		private void FixedUpdate() => StateMachine.PhysicsUpdate();
		private void OnTriggerEnter2D(Collider2D collider) {
			if (collider.gameObject.TryGetComponent(out Ladder ladder)) {
				Ladder = ladder;

				_menu.UpdateIsLadder(IsLadder);
				} // если лестница, то запомним ее
			}
		private void OnTriggerExit2D(Collider2D collider) {
			if (collider.gameObject.TryGetComponent<Ladder>(out _)) {
				Ladder.Show();// покажем колайдер верхушки лестницы
				Ladder = null;

				_menu.UpdateIsLadder(IsLadder);
				} // если лестницы нет, то забудем ее
			}
		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(_groundCheck.position, _data.GroundCheckRadius);
			Gizmos.DrawWireSphere(_ceilingCheck.position, _data.GroundCheckRadius);
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + (transform.right * _data.WallCheckDistance));
			Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + (transform.right * _data.WallCheckDistance));

			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(_groundCheck.position, _groundCheck.position + (-transform.up * _data.SlopeDownCheckDistance));
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(_groundCheck.position, _groundCheck.position + (transform.right * _data.SlopeCheckDistance));
			Gizmos.DrawLine(_groundCheck.position, _groundCheck.position + (-transform.right * _data.SlopeCheckDistance));
			}
		#endregion

		#region Set Functions
		public void SetXPosition(float x) => SetPosition(x, Position.y);
		public void SetYPosition(float y) => SetPosition(Position.x, y);
		public void SetPosition(float x, float y) => transform.position = new Vector2(x, y);
		public void SetPosition(Vector3 position) => transform.position = position;
		public void SetXVelocity(float velocity) => ChangeVelocity(velocity, CurrentVelocity.y);
		public void SetYVelocity(float velocity) => ChangeVelocity(CurrentVelocity.x, velocity);
		public void ChangeVelocity(float xVelocity, float yVelocity) {
			var velocity = new Vector2(xVelocity, yVelocity);

			_body.velocity = velocity;

			CurrentVelocity = velocity;

			_menu.UpdateVelocity(xVelocity, yVelocity);
			}
		public void FreezeVelocity() => ChangeVelocity(0, 0);
		public void ApplyFriction(PhysicsMaterial2D material) {
			_body.sharedMaterial = material;

			_menu.UpdateFriction(material.name);
			} // установка материала для игрока, чтобы включить/выключить трение
		#endregion

		#region Check Functions
		public bool CheckIfOnGround() => Physics2D.OverlapCircle(_groundCheck.position, PlayerData.GroundCheckRadius, PlayerData.WhatIsGround);// проверка земли
		public bool CheckForCeiling() => Physics2D.OverlapCircle(_ceilingCheck.position, PlayerData.GroundCheckRadius, PlayerData.WhatIsGround);// проверка потолка
		public bool CheckIfTouchWall() => Physics2D.Raycast(_wallCheck.position, transform.right, _data.WallCheckDistance, _data.WhatIsGround);// касаюсь ли стены
		public bool CheckIfTouchLedge() => Physics2D.Raycast(_ledgeCheck.position, transform.right, _data.WallCheckDistance, _data.WhatIsGround);// касаюсь ли стены
		public bool CheckIfOnSlope() {
			RaycastHit2D hit = GetHitSlope();// получим наклонную поверхность

			if (hit) {
				float angle = Vector2.Angle(hit.normal, Vector2.up);// угол наклона поверности

				if (angle.Round() > 0 && angle.Round() < 90f) // если он допустим
					return true;
				}

			return false;
			} // проверка стою ли я на наклонной поверхности
		public RaycastHit2D CheckDownHitSlope() => Physics2D.Raycast(_groundCheck.position, -transform.up, _data.SlopeDownCheckDistance, _data.WhatIsGround);// проверка наклонной поверхности вниз
		public RaycastHit2D CheckForwardHitSlope() => Physics2D.Raycast(_groundCheck.position, transform.right, _data.SlopeCheckDistance, _data.WhatIsGround);// проверка наклоной поверхности вправо
		public RaycastHit2D CheckBackHitSlope() => Physics2D.Raycast(_groundCheck.position, -transform.right, _data.SlopeCheckDistance, _data.WhatIsGround);// проверка наклоной поверхности влево
		public void CheckIfShouldFlip(int xInput) {
			if (xInput != 0 && xInput != FacingDirection) // если движемся, и направление передвижение отличается от направления взгляда
				Flip();
			} // проверка нужно ли флипать игрока
		#endregion

		public Vector2 DetermineCornerPosition() {
			RaycastHit2D xHit = Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _data.WallCheckDistance, _data.WhatIsGround);// бросим луч вперед

			var pos = new Vector2((xHit.distance + .015f) * FacingDirection, 0f);// позиция угла по X но со сдвигом, чтобы не было ошибок

			RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.position + (Vector3)pos, -transform.up, _ledgeCheck.position.y - _wallCheck.position.y + .015f, _data.WhatIsGround);// бросим луч вниз

			return new Vector2(_wallCheck.position.x + xHit.distance * FacingDirection, _ledgeCheck.position.y - yHit.distance);// позиция угла
			} // вычисление угла склона
		public RaycastHit2D GetHitSlope() {
			RaycastHit2D hit = CheckDownHitSlope();// вниз (т.к. она самая короткая)

			if (hit == false) {
				hit = CheckForwardHitSlope();// вперед

				if (hit == false)
					hit = CheckBackHitSlope();// назад
				}

			return hit;
			} // получение выгодной наклонной поверхности

		public void StartCrouch() {
			if (IsCrouch) // если уже приседаем
				return;

			IsCrouch = true;

			transform.localScale = new Vector3() {
				x = transform.localScale.x,
				y = .5f,
				z = transform.localScale.z
				};// уменьшим скейл игрока

			SetYPosition(Position.y - .5f);// опустим ниже

			MenuUI.Instance.UpdateBool("IsCrouch", IsCrouch);
			} // старт режима приседания
		public void FinishCrouch() {
			if (IsCrouch == false) // если режим приседания отключен
				return;

			IsCrouch = false;

			transform.localScale = new Vector3() {
				x = transform.localScale.x,
				y = 1f,
				z = transform.localScale.z
				};// вернем скейл игрока

			SetYPosition(Position.y + .5f);// поднимем выше игрока

			MenuUI.Instance.UpdateBool("IsCrouch", IsCrouch);
			} // отключение режима приседания

		public void StartLadderMode() {
			_body.gravityScale = 0;// уберем гравитацию
			_collider.isTrigger = true;// укажем что игрок теперь тригер

			Ladder.Hide();// спрячем колайдер вершини лестницы

			_menu.UpdateGravityScale(_body.gravityScale);
			_menu.UpdateIsTrigger(_collider.isTrigger);
			_menu.UpdateIsLadder(IsLadder);
			} // старт режима подьма/спуска с лестницы
		public void FinishLadderMode() {
			_body.gravityScale = _gravityScale;// вернем гравитацию
			_collider.isTrigger = false;// укажем что игрок физическое тело

			_menu.UpdateGravityScale(_body.gravityScale);
			_menu.UpdateIsTrigger(_collider.isTrigger);
			_menu.UpdateIsLadder(IsLadder);
			} // отключение режима подьма/спуска с лестницы

		private void Flip() {
			FacingDirection *= -1;

			_menu.UpdateFacingDirection(FacingDirection);

			transform.Rotate(0, 180f, 0);
			} // отражение направления взгляда игрока
		private void RestartGame() {
			SetPosition(_startPos);
			FinishCrouch();
			} // рестарт игры

		}
	}