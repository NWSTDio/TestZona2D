using System.Collections.Generic;
using UnityEngine;

namespace SimpleGames.Zuma.version_1 {
	public class Bullet : MonoBehaviour {

		public Vector3 moveTo;
		public int type = -1;

		[SerializeField] private GameObject circlePrefab;

		private readonly float _speed = 10f;
		private bool _colided;
		private static int counter = 0;

		private void Start() {
			moveTo = Vector3.Normalize(new Vector3(moveTo.x, moveTo.y));
			_colided = false;
			}

		private void Update() {
			transform.position += moveTo * _speed * Time.deltaTime;
			if (transform.position.y > 10 || transform.position.y < -10 || transform.position.x > 15 || transform.position.x < -15)
				Destroy(gameObject);
			}

		private void OnTriggerEnter2D(Collider2D collision) {
			if (_colided)
				return;

			_colided = true;

			GameObject tmpObj = collision.gameObject;

			Transform tmpTrans = tmpObj.transform;

			float distamceX = transform.position.x - tmpTrans.position.x;

			Vector3 tmpPos = tmpObj.transform.position;

			bool isLeft = false;

			if (distamceX <= -.2f) {
				isLeft = true;

				if (tmpObj.GetComponent<Ball>().next == null)
					tmpPos = new Vector3(tmpTrans.position.x - 1, tmpTrans.position.y, 0);
				}
			else
				tmpPos = new Vector3(tmpTrans.position.x + 1, tmpTrans.position.y, 0);

			GameObject ball = Instantiate(circlePrefab, tmpPos, Quaternion.identity) as GameObject;
			ball.GetComponent<Ball>().changeColor(type);
			ball.name = "circle_" + (++counter);

			if (isLeft) {
				if (tmpObj.GetComponent<Ball>().next == null)
					tmpObj.GetComponent<Ball>().next = ball.transform;
				else {
					moveObjects(tmpObj);

					ball.GetComponent<Ball>().next = tmpObj.GetComponent<Ball>().next;

					tmpObj.GetComponent<Ball>().next.gameObject.GetComponent<Ball>().prev = ball.transform;

					tmpObj.GetComponent<Ball>().next = ball.transform;
					}
				ball.GetComponent<Ball>().prev = tmpTrans;

				Debug.Log("Left Collision" + "___" + tmpObj.name);
				}
			else {
				if (tmpObj.GetComponent<Ball>().prev == null)
					tmpObj.GetComponent<Ball>().prev = ball.transform;
				else {
					moveObjects(tmpObj.GetComponent<Ball>().prev.gameObject);

					ball.GetComponent<Ball>().prev = tmpObj.GetComponent<Ball>().prev;

					tmpObj.GetComponent<Ball>().prev.gameObject.GetComponent<Ball>().next = ball.transform;

					tmpObj.GetComponent<Ball>().prev = ball.transform;
					}
				ball.GetComponent<Ball>().next = tmpTrans;

				Debug.Log("Right Collision");
				}
			//checkColors(ball);
			Destroy(gameObject);
			}
		private void moveObjects(GameObject obj) {
			GameObject tmp = obj;

			int counter = 0;

			while (tmp != null) {
				Debug.Log("move: " + tmp.name);

				tmp.transform.position += new Vector3(1, 0, 0);

				if (tmp.GetComponent<Ball>().prev == null)
					break;

				tmp = tmp.GetComponent<Ball>().prev.gameObject;

				if (counter >= 100)
					break;

				counter++;
				}

			Debug.Log(counter);
			}
		private void CheckColors(GameObject obj) {
			int type = obj.GetComponent<Ball>().type;

			var tmpList = new List<Transform> {
				obj.transform
				};

			Transform prev = obj.GetComponent<Ball>().prev, next = obj.GetComponent<Ball>().next;

			int counter = 0;

			while (next) {
				if (next.GetComponent<Ball>().type != type)
					break;

				tmpList.Add(next.transform);

				next = next.gameObject.GetComponent<Ball>().next;

				if (counter >= 100)
					break;

				counter++;
				}

			counter = 0;

			while (prev) {
				if (prev.GetComponent<Ball>().type != type)
					break;

				tmpList.Add(prev.transform);

				prev = prev.gameObject.GetComponent<Ball>().prev;

				if (counter >= 100)
					break;

				counter++;
				}
			if (tmpList.Count >= 3) {
				if (next == null)
					prev.GetComponent<Ball>().next = null;
				else
					prev.GetComponent<Ball>().next = next;

				if (prev == null)
					next.GetComponent<Ball>().prev = null;
				else
					next.GetComponent<Ball>().prev = next;

				foreach (Transform t in tmpList)
					Destroy(t.gameObject);
				}
			}
		}
	}