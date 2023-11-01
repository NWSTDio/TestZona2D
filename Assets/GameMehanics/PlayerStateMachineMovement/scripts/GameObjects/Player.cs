using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public class Player : MonoBehaviour {

		#region StateMachine
		public PlayerStateMachine StateMachine { get; private set; } // машина состояний

		public IdleState Idle { get; private set; } // ожидание
		public WalkState Walk { get; private set; } // ходьба
		public RunState Run { get; private set; } // бег
		public LandState Land { get; private set; } // приземление
		public CrouchIdleState CrouchIdle { get; private set; } // ожидать на присядки
		public CrouchWalkState CrouchWalk { get; private set; } // идти на присядки

		public JumpState Jump { get; private set; } // прыжок
		public WallJumpState WallJump { get; private set; } // прыжок от стены
		public DashState Dash { get; private set; } // рывок

		public WallClimbState WallClimb { get; private set; } // висеть на вершине
		public WallGrabState WallGrab { get; private set; } // схватится за стену
		public WallSlideState WallSlide { get; private set; } // скользить по стене

		public InAirState InAir { get; private set; } // в воздухе
		public LedgeClimbState LedgeClimb { get; private set; } // вскарабкивание на вершину
		public TestState Test { get; private set; } // тестовое состояние
		#endregion

		public PlayerInputHandler InputHandler { get; private set; } // система ввода
		public Vector2 CurrentVelocity { get; private set; } // текущая скорость игрока
		public int FacingDirection { get; private set; } = 1;// направление взгляда

		[SerializeField] private PlayerData _data;// данные игрока
		[SerializeField] private Transform _groundCheck, _wallCheck, _ledgeCheck, _ceilingCheck;// точки проверки состояний
		[SerializeField] private Transform _dashDirectionIndicator;// индикатор рывка

		private Rigidbody2D _body;
		private CapsuleCollider2D _collider;

		public PlayerData PlayerData => _data;// данные игрока
		public Rigidbody2D Body => _body;
		public Transform DashDirectionIndicator => _dashDirectionIndicator;
		public Vector2 Position => transform.position;

		#region MonoBehaviour
		private void Awake() {
			StateMachine = new PlayerStateMachine();

			Idle = new IdleState(this, "idle");
			Walk = new WalkState(this, "walk");
			Run = new RunState(this, "run");
			Land = new LandState(this, "land");
			CrouchIdle = new CrouchIdleState(this, "crouch_idle");
			CrouchWalk = new CrouchWalkState(this, "crouch_walk");

			Jump = new JumpState(this, "jump");
			WallJump = new WallJumpState(this, "wall_jump");
			Dash = new DashState(this, "dash");

			WallClimb = new WallClimbState(this, "wall_climb");
			WallGrab = new WallGrabState(this, "wall_grab");
			WallSlide = new WallSlideState(this, "wall_slide");

			InAir = new InAirState(this, "inAir");
			LedgeClimb = new LedgeClimbState(this, "ledge_climb");
			Test = new TestState(this, "test");

			InputHandler = GetComponent<PlayerInputHandler>();
			_body = GetComponent<Rigidbody2D>();
			_collider = GetComponent<CapsuleCollider2D>();
			}
		private void Start() {
			StateMachine.Initialize(Idle);

			CurrentVelocity = new Vector2();
			}
		private void Update() {
			CurrentVelocity = _body.velocity;

			StateMachine.LogicUpdate();
			}
		private void FixedUpdate() => StateMachine.PhysicsUpdate();
		private void OnDrawGizmos() {
			Gizmos.color = Color.red;

			Gizmos.DrawWireSphere(_groundCheck.position, PlayerData.GroundCheckRadius);
			Gizmos.DrawWireSphere(_ceilingCheck.position, PlayerData.GroundCheckRadius);

			Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + transform.right * PlayerData.WallCheckDistance);
			Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + transform.right * PlayerData.WallCheckDistance);
			}
		#endregion

		#region Set Functions
		public void SetXVelocity(float velocity) => ChangeVelocity(velocity, CurrentVelocity.y);
		public void SetYVelocity(float velocity) => ChangeVelocity(CurrentVelocity.x, velocity);
		public void ChangeVelocity(float xVelocity, float yVelocity) {
			var velocity = new Vector2(xVelocity, yVelocity);

			_body.velocity = velocity;

			CurrentVelocity = velocity;
			}
		public void ChangeVelocity(Vector2 velocity) {
			_body.velocity = velocity;

			CurrentVelocity = velocity;
			}
		public void SetVelocity(float velocity, Vector2 angle, int direction) {
			angle.Normalize();

			ChangeVelocity(angle.x * velocity * direction, angle.y * velocity);
			}
		public void SetVelocity(float velocity, Vector2 direction) => ChangeVelocity(direction * velocity);
		#endregion

		#region Check Functions
		public bool CheckForCeiling() => Physics2D.OverlapCircle(_ceilingCheck.position, PlayerData.GroundCheckRadius, PlayerData.WhatIsGround);// проверка потолка
		public bool CheckIfGrounded() => Physics2D.OverlapCircle(_groundCheck.position, PlayerData.GroundCheckRadius, PlayerData.WhatIsGround);// проверка земли
		public bool CheckIfTouchingWall() => Physics2D.Raycast(_wallCheck.position, transform.right, PlayerData.WallCheckDistance, PlayerData.WhatIsGround);// проверка стены перед игроком
		public bool CheckIfTouchingWallBack() => Physics2D.Raycast(_wallCheck.position, -transform.right, PlayerData.WallCheckDistance, PlayerData.WhatIsGround);// проверка стены сзади игрока
		public bool CheckIfTouchingLedge() => Physics2D.Raycast(_ledgeCheck.position, transform.right, PlayerData.WallCheckDistance, PlayerData.WhatIsGround);// проверка есть ли край склона
		public void CheckIfShouldFlip(int xInput) {
			if (xInput != 0 && xInput != FacingDirection) // если движемся, и направление передвижение отличается от направления взгляда
				Flip();
			} // проверка нужно ли флипать игрока
		#endregion

		public Vector2 DetermineCornerPosition() {
			RaycastHit2D xHit = Physics2D.Raycast(_wallCheck.position, transform.right, PlayerData.WallCheckDistance, PlayerData.WhatIsGround);

			var pos = new Vector2((xHit.distance + .015f) * FacingDirection, 0f);

			RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.position + (Vector3)pos, Vector2.down, _ledgeCheck.position.y - _wallCheck.position.y + .015f, PlayerData.WhatIsGround);

			return new Vector2(_wallCheck.position.x + (xHit.distance * FacingDirection), _ledgeCheck.position.y - yHit.distance);
			} // получение координат перемещения игрока когда он взобрался на гору
		public void SetColliderHeight(float height) {
			Vector2 center = _collider.offset;

			var size = new Vector2(_collider.size.x, height);

			center.y += (height - _collider.size.y) / 2;

			_collider.size = size;
			_collider.offset = center;
			} // изменения колайдера игрока

		private void Flip() {
			FacingDirection *= -1;

			transform.Rotate(0, 180f, 0);
			} // отражение направления взгляда игрока

		}
	}