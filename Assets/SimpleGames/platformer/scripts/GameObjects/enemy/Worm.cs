using UnityEngine;

namespace SimpleGames.Platformer {
	public class Worm : Entity { // червяк

		private int _lives = 3;// жизни

		private void OnCollisionEnter2D(Collision2D collision) {
			if (collision.gameObject.TryGetComponent(out Player player)) { // если игрок
				player.Damage();// нанесем игроку урон

				Damage();// нанесем червяку урон
				}
			}

		private void Damage() { // урон по червяку
			_lives--;

			if (_lives < 1) // если сдох
				Die();
			}
		}
	}