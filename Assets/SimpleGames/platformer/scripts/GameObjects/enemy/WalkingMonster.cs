using UnityEngine;

namespace SimpleGames.Platformer {
	public class WalkingMonster : Entity { // подвижный монстр

		[SerializeField] private SpriteRenderer _sprite;
		private Vector3 _direction;// направление движения
		private Animator _animator;

		private void Awake() {
			_animator = GetComponent<Animator>();

			_animator.SetInteger("state", 1);// обновим состояние анимации

			_sprite.flipX = true;// повернем спрайт

			_direction = transform.right;// укажем направление движения
			}
		private void Update() {
			var check = new Vector3() {
				x = transform.position.x + (transform.localScale.x * _direction.x),
				y = transform.position.y,
				z = transform.position.z
				};// координаты провреки столкновений со стенами

			Collider2D[] colliders = Physics2D.OverlapCircleAll(check, 0.1f);// поверим есть ли обьекты столкновений

			if (colliders.Length > 0) { // если есть обьект перед глазами
				_direction *= -1f;// меняем направление движения

				_sprite.flipX = _direction.x > 0.0f;// отразим спрайт
				}

			transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, Time.deltaTime);
			}
		private void OnCollisionEnter2D(Collision2D collision) {
			if (collision.gameObject.TryGetComponent(out Player player))
				player.Damage();// нанесем урон игроку
			}
		}
	}