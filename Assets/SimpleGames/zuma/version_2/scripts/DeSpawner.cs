using UnityEngine;

namespace SimpleGames.Zuma.version_2 {
	public class DeSpawner : MonoBehaviour {

		private GameController _controller;

		private void Start() {
			_controller = GameController.Instance;
			}

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.TryGetComponent(out Ball ball))
				_controller.RemoveBall(ball);
			}

		}
	}