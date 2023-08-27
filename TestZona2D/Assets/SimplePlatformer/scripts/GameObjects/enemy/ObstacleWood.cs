using UnityEngine;

namespace SimplePlatformer {
	public class ObstacleWood : MonoBehaviour { // деревянный пенек

		private void OnCollisionEnter2D(Collision2D collision) {
			if (collision.gameObject.TryGetComponent(out Player player))
				player.Damage();// нанесем урон игроку
			}

		}
	}