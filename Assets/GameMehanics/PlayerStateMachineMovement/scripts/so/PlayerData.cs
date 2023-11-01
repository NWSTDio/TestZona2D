using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	[CreateAssetMenu(fileName = "_data", menuName = "GameMehanics/PlayerStateMachineMovement/PlayerData")]
	public class PlayerData : ScriptableObject {

		[Header("Move State")]
		[SerializeField] private float _walkSpeed = 10f;// ходьба
		[SerializeField] private float _runSpeed = 15f;// бег
		[SerializeField] private float _airSpeed = 5f;// передвижение в воздухе

		[Header("Jump State")]
		[SerializeField] private float _jumpForce = 20f;// прыжок
		[SerializeField] private int _amountOfJumps = 1;// количество прыжков

		[Header("Wall Jump State")]
		[SerializeField] private Vector2 _wallJumpAngle = new(1f, 2f);// угол прыжка от стены
		[SerializeField] private float _wallJumpForce = 20f;// сила прыжка от стены
		[SerializeField] private float _wallJumpTime = .4f;// время прыжка от стены

		[Header("In Air State")]
		[SerializeField] private float _jumpHeightMultiplier = .5f;// множитель высоты прыжка
		[SerializeField] private float _coyoteTime = .2f;// время ожидания прыжка

		[Header("Wall Slide State")]
		[SerializeField] private float _wallSlideSpeed = 3f;// скорость сползания по стене

		[Header("Wall Climb State")]
		[SerializeField] private float _wallClimbSpeed = 3f;// скорость подьема по стене

		[Header("Ledge Climb State")]
		[SerializeField] private Vector2 _startOffset;// смещение положения персонажа в позиции висения на склоне
		[SerializeField] private Vector2 _stopOffset;// смещение положения персонажа в позиции конечного взбирания на гору

		[Header("Dash State")]
		[SerializeField] private float _dashCooldown = .5f;// кулдаун рывка
		[SerializeField] private float _maxHoldTime = 1f;// время работы рывка
		[SerializeField] private float _holdTimeScale = .25f;// коефициент времени при рывке
		[SerializeField] private float _dashTime = .2f;// время рывка
		[SerializeField] private float _dashSpeed = 30f;// скорость рывка
		[SerializeField] private float _drag = 10f;
		[SerializeField] private float _dashEndYMultiplier = .2f;// уменьшение скорости еслы рывок вверх

		[Header("Crouch State")]
		[SerializeField] private float _crouchSpeed = 5f;// скорость движения когда присел
		[SerializeField] private float _croucColliderHeight = 1f;// высота колайдера при присидании
		[SerializeField] private float _standColliderHeight = 2f;// высота колайдера в обычном состоянии

		[Header("Check Variables")]
		[SerializeField] private LayerMask _whatIsGround;// что есть земля
		[SerializeField] private float _groundCheckRadius = .3f;// радиус проверки земли
		[SerializeField] private float _wallCheckDistance = .5f;// расстояние до стены

		public float WalkSpeed => _walkSpeed;
		public float RunSpeed => _runSpeed;
		public float AirSpeed => _airSpeed;

		public float JumpForce => _jumpForce;
		public int AmountOfJumps => _amountOfJumps;

		public Vector2 WallJumpAngle => _wallJumpAngle;
		public float WallJumpSpeed => _wallJumpForce;
		public float WallJumpTime => _wallJumpTime;

		public float JumpHeightMultiplier => _jumpHeightMultiplier;
		public float CoyoteTime => _coyoteTime;

		public float WallSlideSpeed => _wallSlideSpeed;

		public float WallClimbSpeed => _wallClimbSpeed;

		public Vector2 StartOffset => _startOffset;
		public Vector2 StopOffset => _stopOffset;

		public float DashCooldown => _dashCooldown;
		public float MaxHoldTime => _maxHoldTime;
		public float HoldTimeScale => _holdTimeScale;
		public float DashTime => _dashTime;
		public float DashSpeed => _dashSpeed;
		public float Drag => _drag;
		public float DashEndYMultiplier => _dashEndYMultiplier;

		public float CrouchSpeed => _crouchSpeed;
		public float CrouchColliderHeight => _croucColliderHeight;
		public float StandColliderHeight => _standColliderHeight;

		public LayerMask WhatIsGround => _whatIsGround;
		public float GroundCheckRadius => _groundCheckRadius;
		public float WallCheckDistance => _wallCheckDistance;

		}
	}