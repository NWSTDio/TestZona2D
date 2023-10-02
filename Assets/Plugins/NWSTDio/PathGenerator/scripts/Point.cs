using UnityEngine;

namespace NWSTDio {
	public class Point : MonoBehaviour {

		[SerializeField] private Transform _spine1, _spine2, _end;
		[SerializeField] private Point _prev, _next;
		[SerializeField] private bool _isBezier;

		private bool _isRunning;

		public bool IsBezier => _isBezier;

		public Vector3 Position => transform.position;

		private void Start() {
			_isRunning = true;
			}

		private void OnDrawGizmos() {
			if (_isRunning) {
				if (_next) {
					Gizmos.color = Color.cyan;
					Gizmos.DrawLine(transform.position, _next.Position);
					}
				}
			else {
				Gizmos.color = _isBezier ? Color.red : Color.white;

				if (_end)
					Gizmos.DrawLine(transform.position, _end.position);

				if (_isBezier) {
					Gizmos.color = Color.green;

					Gizmos.DrawLine(transform.position, _spine1.position);
					Gizmos.DrawLine(_end.position, _spine2.position);

					Gizmos.color = Color.magenta;

					Vector3 prev = transform.position;

					for (float i = 0, step = .05f, max_i = 1 + step; i <= max_i; i += step) {
						Vector3 point = BezierUtills.GetPoint(transform.position, _spine1.position, _spine2.position, _end.position, i);

						Gizmos.DrawLine(prev, point);

						prev = point;
						}
					}
				}
			}
		public Point GetNextPoint() => _next;
		public Point GetPrevPoint() => _prev;

		public void SetNextPoint(Point p) => _next = p;
		public void SetPrevPoint(Point p) => _prev = p;

		public Vector3 GetBezierPosition(float step) => BezierUtills.GetPoint(transform.position, _spine1.position, _spine2.position, _end.position, step);

		}
	}