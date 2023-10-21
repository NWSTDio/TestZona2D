using TMPro;
using UnityEngine;

namespace GameMehanics.MoveInSlope {
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    public class PlayerController : MonoBehaviour {

        [SerializeField] private Transform _groundCheck, _wallCheck;// координаты проверки земли и стены
        [SerializeField] private LayerMask _whatIsGround;// что есть земля
        [SerializeField] private PhysicsMaterial2D _noFriction, _fullFriction;// физ. материалы трения

        [SerializeField] private TextMeshProUGUI _velocityText;
        [SerializeField] private TextMeshProUGUI _movementXAxisText;
        [SerializeField] private TextMeshProUGUI _slopeSideAngleText;
        [SerializeField] private TextMeshProUGUI _isOnGroundText, _isOnWallText, _isOnSlopeText, _isOnAirText;
        [SerializeField] private TextMeshProUGUI _canWalkOnSlopeText, _canJumpText;
        [SerializeField] private TextMeshProUGUI _frictionText;
        [SerializeField] private TextMeshProUGUI _isJumpingText;
        [SerializeField] private TextMeshProUGUI _currentSpeedText;

        private readonly float _movementSpeed = 10f, _jumpForce = 30f, _sprintSpeed = 15f;// скорость движения, прыжка и бега
        private readonly float _groundCheckRadius = .38f, _slopeCheckDistance = .6f, _wallCheckRadius = .05f, _slopeDownCheckDistance = .1f;
        private readonly float _maxSlopeAngle = 50f;// макс. угол подьема/спуска
        private Rigidbody2D _body;
        private CapsuleCollider2D _collider;
        private Vector2 _slopeNormalPerp;// нормаль по которой будем двигатся если наклон
        private float _movementXAxis;// направление движения
        private float _slopeSideAngle;// угол наклона поверхности
        private bool _isOnGround, _isOnWall;// на земле? у стены?
        private bool _canJump;// могу ли прыгнуть
        private bool _sprint;// бег?

        public float CurrentSpeed {
            get {
                if (NotMove) // если стою
                    return 0;
                else {
                    if ((IsOnSlope && CanWalkOnSlope == false) || _isOnWall) // если на склоне и не могу двигатся или у стены
                        return 0;
                    }

                return _sprint ? _sprintSpeed : _movementSpeed;// если бег
                }
            } // текущая скорость

        public bool IsRightEyeDirection => transform.localScale.x > 0;// проверка направления взгляда
        public bool NotMove => _movementXAxis == 0;// стою?
        public bool IsOnSlope => _slopeSideAngle.Round() > 0 && _slopeSideAngle.Round() < 90f;// на склоне? 
        public bool CanWalkOnSlope => _slopeSideAngle <= _maxSlopeAngle;// могу ли идти по склону
        public bool IsJumping => _body.velocity.y.Round() > 0;// в прыжке?
        public bool IsOnAir => IsOnSlope == false && _body.velocity.y.Round() != 0;// в воздухе?

        #region MonoBehaviour
        private void Start() {
            _body = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
            }
        private void Update() => CheckInput();
        private void FixedUpdate() {
            CheckCollisions();
            CheckSlope();

            _canJump = _isOnGround || IsOnSlope;// можно ли прыгать?

            ApplyFriction();
            ApplyMovement();

            UpdateVisual();
            }
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
            Gizmos.DrawWireSphere(_wallCheck.position, _wallCheckRadius);
            }
        #endregion

        #region Updates
        private void CheckInput() {
            _movementXAxis = Input.GetAxisRaw("Horizontal");

            if (_movementXAxis == 1 && IsRightEyeDirection == false) // если в право идти а смотрим в лево
                Flip();
            else if (_movementXAxis == -1 && IsRightEyeDirection) // если в лево идти а смотрим в право
                Flip();

            if (Input.GetButtonDown("Jump"))
                Jump();

            _sprint = Input.GetKey(KeyCode.LeftShift);
            }
        private void CheckCollisions() {
            _isOnGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
            _isOnWall = Physics2D.OverlapCircle(_wallCheck.position, _wallCheckRadius, _whatIsGround);
            }
        private void CheckSlope() { // проверка есть ли под ногами наклонная поверхность
            Vector2 checkPos = transform.position - (Vector3)new Vector2(0.0f, _collider.size.y / 2);

            RaycastHit2D hit = Physics2D.Raycast(checkPos, -transform.up, _slopeDownCheckDistance, _whatIsGround);// проверка снизу

            if (hit == false) { // если с низу пусто
                RaycastHit2D slopeHitRight = Physics2D.Raycast(checkPos, transform.right, _slopeCheckDistance, _whatIsGround);// проверка с переди (не зависит от напр. взгляда)
                RaycastHit2D slopeHitLeft = Physics2D.Raycast(checkPos, -transform.right, _slopeCheckDistance, _whatIsGround);// проверка с зади (не зависит от напр. взгляда)

                Debug.DrawRay(checkPos, transform.right * _slopeCheckDistance, Color.blue);
                Debug.DrawRay(checkPos, -transform.right * _slopeCheckDistance, Color.blue);

                if (NotMove) { // если стою
                    if (slopeHitRight) // если с права чтото есть
                        hit = slopeHitRight;
                    else if (slopeHitLeft) // если с лева чтото есть
                        hit = slopeHitLeft;
                    }
                else {
                    if (IsRightEyeDirection) // если смотрю в право
                        hit = slopeHitRight ? slopeHitRight : slopeHitLeft;
                    else
                        hit = slopeHitLeft ? slopeHitLeft : slopeHitRight;
                    }
                }
            else
                Debug.DrawRay(checkPos, -transform.up * _slopeDownCheckDistance, Color.blue);

            if (hit) { // если поверхность обнаружена
                _slopeSideAngle = Vector2.Angle(hit.normal, Vector2.up);// угол наклона поверности

                _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;// перпендикуляр поверхности для движения по склонам

                Debug.DrawRay(hit.point, hit.normal, Color.yellow);
                }
            else
                _slopeSideAngle = 0;// иначе пусто
            }
        #endregion

        private void Jump() {
            if (IsOnAir && _canJump == false) // если в воздухе и не могу прыгнуть
                return;

            Debug.Log("Jump");

            _canJump = false;

            _body.velocity = new Vector2(_body.velocity.x, _jumpForce);// прыжок
            }
        private void Flip() => transform.localScale *= new Vector2(-1, 1);// поворот перса
        private void ApplyFriction() { // применение трения
            if (NotMove) { // если стою
                if (IsOnSlope && CanWalkOnSlope) { // если на наклонной поверности и могу по ней ходить
                    _body.sharedMaterial = _fullFriction;// макс. трение

                    return;
                    }
                }

            if (IsOnSlope && CanWalkOnSlope && _isOnWall) { // если на наклонной поверхности  и могу идти и уперся в стену
                _body.sharedMaterial = _fullFriction;// макс. трение

                return;
                }

            _body.sharedMaterial = _noFriction;// без трения
            }
        private void ApplyMovement() { // приминение движения
            if (IsOnSlope && CanWalkOnSlope && IsJumping == false) { // если на наклонной поверхности и могу идти и не прыгаю
                _body.velocity = new Vector2(CurrentSpeed * _slopeNormalPerp.x * -_movementXAxis, CurrentSpeed * _slopeNormalPerp.y * -_movementXAxis);// движемся по нормали

                return;
                }

            _body.velocity = new Vector2(CurrentSpeed * _movementXAxis, _body.velocity.y);// иначе обычное движение
            }

        private void UpdateVisual() { // обновление данных на экране
            _isOnGroundText.text = "IsOnGround: <color=green>" + _isOnGround + "</color>";
            _isJumpingText.text = "IsJumping: <color=green>" + IsJumping + "</color>";
            _isOnAirText.text = "IsOnAir: <color=green>" + IsOnAir + "</color>";
            _canWalkOnSlopeText.text = "CanWalkOnSlope: <color=green>" + CanWalkOnSlope + "</color>";
            _currentSpeedText.text = "CurrentSpeed: <color=green>" + CurrentSpeed + "</color>";
            _velocityText.text = "Velocity: X= <color=green>" + _body.velocity.x.Round() + "</color>,Y= <color=green>" + _body.velocity.y.Round() + "</color>";
            _movementXAxisText.text = "Movement XAxis: <color=green>" + _movementXAxis + "</color>";
            _slopeSideAngleText.text = "SlopeSide Angle: <color=green>" + _slopeSideAngle.Round() + "</color>";
            _isOnWallText.text = "IsOnWall: <color=green>" + _isOnWall + "</color>";
            _isOnSlopeText.text = "IsOnSlope: <color=green>" + IsOnSlope + "</color>";
            _canJumpText.text = "CanJump: <color=green>" + _canJump + "</color>";
            _frictionText.text = "Friction: <color=green>" + (_body.sharedMaterial == _fullFriction) + "</color>";
            }

        }
    }