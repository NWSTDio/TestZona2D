using UnityEngine;

namespace SimplePlatformer {
	[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
	public class Player : Entity {

		public enum States { IDLE, RUN, JUMP, FALL } // состояния анимаций

		[SerializeField] private SpriteRenderer _sprite;// изображение игрока
		[SerializeField] private Transform _foot;// координаты ног

		private readonly float _speed = 3f;// скорость движения
		private readonly float _jumpForse = 15f;// сила прыжка
		private Rigidbody2D _body;
		private Animator _animator;
		private int _lives = 15;// жизни
		private bool _isGrounded;// для проверки на земле ли игрок

		private void Awake() {
			_body = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();
			}

		private void Start() {
			GameUI.Instance.UpdateLives(_lives);// обновим счетсчик жизней
			}

		private void FixedUpdate() {
			Collider2D[] collider = Physics2D.OverlapCircleAll(_foot.position, 0.3f);// проверим колизии под ногами

			_isGrounded = collider.Length > 1;// установим есть ли чтото под ногами

			if (_isGrounded == false) // если нет
				SetAnimationState(States.JUMP);// анимация прижок
			}
		private void Update() {
			if (_isGrounded) // если на земле
				SetAnimationState(States.IDLE);// анимация ожидания

			if (Input.GetButton("Horizontal")) { // движение по горизонтали
				if (_isGrounded) // если на земле
					SetAnimationState(States.RUN);// включим анимацию движения

				Vector3 dir = transform.right * Input.GetAxis("Horizontal");// направление движения

				transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, _speed * Time.deltaTime);

				_sprite.flipX = dir.x < 0f;// отразим спрайт если направление сменилось
				}

			if (_isGrounded && Input.GetButtonDown("Jump")) // если был нажат прижок и мы на земле
				_body.AddForce(transform.up * _jumpForse, ForceMode2D.Impulse);
			}

		public void Damage() { // урон игроку
			_lives--;

			GameUI.Instance.UpdateLives(_lives);// обновим счетчик жизней
			}

		private void SetAnimationState(States state) => _animator.SetInteger("state", (int)state);
		}
	}