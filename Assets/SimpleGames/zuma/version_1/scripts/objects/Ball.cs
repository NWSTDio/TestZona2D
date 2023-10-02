using UnityEngine;

namespace SimpleGames.Zuma.version_1 {
	public class Ball : MonoBehaviour {

		[SerializeField] public Color[] colors;
		public Transform prev, next;
		public int type = -1;
		private float speed = .5f;

		private void Start() {
			if (type == -1)
				changeColor(Random.Range(0, colors.Length));
			}

		private void Update() {
			transform.position += speed * Time.deltaTime * new Vector3(-1, 0, 0);
			}

		public void changeColor(int type) {
			GetComponent<SpriteRenderer>().color = colors[type];
			}
		}
	}