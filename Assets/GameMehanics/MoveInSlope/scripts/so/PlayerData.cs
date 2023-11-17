using UnityEngine;

namespace GameMehanics.MoveInSlope {
	[CreateAssetMenu(fileName = "_data", menuName = "GameMehanics/MoveInSlope/PlayerData")]
	public class PlayerData : ScriptableObject {

		// скорости движения
		[SerializeField] private float _walkSpeed = 10f;// ходьба
		[SerializeField] private float _jumpSpeed = 30f;// прыжок
		[SerializeField] private float _runSpeed = 15f;// бег
		[SerializeField] private float _airSpeed = 5f;// передвижение в воздухе
		[SerializeField] private float _wallSlideSpeed = 5f;// подьем на стену
		[SerializeField] private float _wallClimbSpeed = 5f;// спуск со стены
		[SerializeField] private float _crouchSpeed = 5f;// приседание

		[SerializeField] private int _amountOfJumps = 1;// количество прыжков

		[SerializeField] private float _groundCheckRadius = .38f;// радиус проверки земли
		[SerializeField] private float _slopeCheckDistance = .6f;// дистанция проверки наклонной поверхности
		[SerializeField] private float _wallCheckDistance = .45f;// дистанция проверки стены
		[SerializeField] private float _slopeDownCheckDistance = .1f;// дистанция проверки наклонной поверхности под ногами

		[SerializeField] private float _maxSlopeAngle = 50f;// максимально допустимый угол подьема по наклонной поверхности

		[SerializeField] private float _jumpHeightMultiplier = .5f;// множитель высоты прыжка

		[SerializeField] private LayerMask _whatIsGround;// что есть земля
		[SerializeField] private PhysicsMaterial2D _noFriction;// отсутствие трения
		[SerializeField] private PhysicsMaterial2D _fullFriction;// трение

		[SerializeField] private Vector2 _startOffset, _finishOffset;// смещение игрока при подьеме на уступ


		public float WalkSpeed => _walkSpeed;
		public float JumpSpeed => _jumpSpeed;
		public float RunSpeed => _runSpeed;

		public float WallSlideSpeed => _wallSlideSpeed;
		public float WallClimbSpeed => _wallClimbSpeed;

		public int AmountOfJumps => _amountOfJumps;

		public float GroundCheckRadius => _groundCheckRadius;
		public float SlopeCheckDistance => _slopeCheckDistance;
		public float WallCheckDistance => _wallCheckDistance;
		public float SlopeDownCheckDistance => _slopeDownCheckDistance;

		public float MaxSlopeAngle => _maxSlopeAngle;
		public float JumpHeightMultiplier => _jumpHeightMultiplier;

		public float AirSpeed => _airSpeed;

		public LayerMask WhatIsGround => _whatIsGround;
		public PhysicsMaterial2D NoFriction => _noFriction;
		public PhysicsMaterial2D FullFriction => _fullFriction;

		public Vector2 StartOffset => _startOffset;
		public Vector2 FinishOffset => _finishOffset;

		public float CrouchSpeed => _crouchSpeed;

		}
	}