using UnityEngine;
using NWSTDio;

namespace SimpleGames.Zuma.version_2 {
	[RequireComponent(typeof(SpriteRenderer))]
	public class Ball : MonoBehaviour {

		private Point _destination;
		private Ball _prev, _next;

		public Ball NextBall => _next;
		public Ball PrevBall => _prev;
		public bool IsEndOfPath => _destination == null;
		public bool IsDestination => transform.position == _destination.Position;

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.TryGetComponent(out MovedBall ball) && ball.IsLocked == false) {
				ball.Lock();

				Debug.Log("Trigger");
				}
			}

		public void Move(float deltaTime, float speed) {
			if (IsEndOfPath)
				return;

			float ost = 0;

			if (NextBall != null) {
				float distance = Vector3.Distance(_next.transform.position, transform.position);

				if (distance >= 1f)
					ost = (distance - 1f) * 2;
				}

			transform.position = Vector3.MoveTowards(transform.position, _destination.transform.position, (speed + ost) * deltaTime);
			}
		public void SetDestination(Point destination) => _destination = destination;
		public void NextDestination() => _destination = _destination.GetNextPoint();
		public void PrevDestination() => _destination = _destination.GetPrevPoint();
		public void ChangeNextBall(Ball ball) => _next = ball;
		public void ChangePrevBall(Ball ball) => _prev = ball;
		}
	}