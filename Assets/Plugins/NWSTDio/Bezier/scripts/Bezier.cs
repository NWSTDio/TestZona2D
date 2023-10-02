using System.Collections.Generic;
using UnityEngine;

namespace NWSTDio {
	public class Bezier : MonoBehaviour {

		[SerializeField] private Transform _begin, _spine1, _spine2, _end;
		private readonly float _maxLength = 1f;

		private void OnDrawGizmos() {
			Gizmos.color = Color.red;

			Gizmos.DrawLine(_begin.position, _spine1.position);
			Gizmos.DrawLine(_spine2.position, _end.position);

			Gizmos.color = Color.magenta;

			if (TryGetPoints(2, .05f, out List<Vector3> points)) {
				for (int i = 0, max_i = points.Count - 1; i < max_i; i++)
					Gizmos.DrawLine(points[i], points[i + 1]);

				points.Clear();
				}
			}

		public bool TryGetPoints(float minPoints, float step, out List<Vector3> points) {
			points = GetPoints(step);

			return points.Count >= minPoints;
			}

		public List<Vector3> GetPoints(float step) {
			List<Vector3> points = new();

			for (float i = 0, max_i = _maxLength + step; i <= max_i; i += step)
				points.Add(GetPosition(i));

			return points;
			}

		public Vector3 GetPosition(float len) => BezierUtills.GetPoint(_begin.position, _spine1.position, _spine2.position, _end.position, len);
		public Vector3 GetRotation(float len) => BezierUtills.GetRotation(_begin.position, _spine1.position, _spine2.position, _end.position, len);

		}
	}