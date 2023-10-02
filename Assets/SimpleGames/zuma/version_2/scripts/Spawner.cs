using UnityEngine;

namespace SimpleGames.Zuma.version_2 {
	public class Spawner : MonoBehaviour {

		private GameController _controller;

		private void Start() {
			_controller = GameController.Instance;
			}

		private void OnTriggerExit2D(Collider2D collision) {
			if (collision.TryGetComponent(out Ball ball) && _controller.IsCanSpawnBall) {
				if (_controller.IsTail(ball) && _controller.IsForward)
					_controller.SpawnBall();
				}
			}

		}
	}