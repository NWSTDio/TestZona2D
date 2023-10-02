using UnityEngine;

namespace NWSTDio {
	public class PathGenerator : MonoBehaviour {

		[SerializeField] private Point _pointPrefab;
		[SerializeField, Range(.05f, .5f)] private float step = .05f;

		public void Awake() {
			Point[] all = GetComponentsInChildren<Point>();

			Point current = all[0], n;

			foreach (Point p in all) {
				if (current != p) {
					current.SetNextPoint(p);

					p.SetPrevPoint(current);

					current = p;
					}

				if (p.IsBezier) {
					n = p;

					for (float i = step; i < 1f; i += step) {
						Point point = Instantiate(_pointPrefab, p.GetBezierPosition(i), Quaternion.identity, p.transform);
						point.name = "bezier_" + i;

						n.SetNextPoint(point);
						point.SetPrevPoint(n);

						n = point;
						}

					current = n;
					}
				}
			}

		}
	}