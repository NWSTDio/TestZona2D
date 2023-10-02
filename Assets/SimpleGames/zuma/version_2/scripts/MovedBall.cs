using UnityEngine;

namespace SimpleGames.Zuma.version_2 {
	public class MovedBall : MonoBehaviour {

		private readonly float _speed = 2f;
		private bool _isLock = false;

		public bool IsLocked => _isLock;

		private void Update() {
			transform.Translate(_speed * Time.deltaTime * Vector2.right);
			}

		public void Lock() => _isLock = true;
		}
	}