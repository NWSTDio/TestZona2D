using UnityEngine;

namespace ExampleWorkEditor.TestAnimations.version_2 {
	public class Explosion : MonoBehaviour {

		public void Show(Vector3 position) {
			gameObject.SetActive(true);

			transform.position = position;
			}

		public void EndOfAnimation() => gameObject.SetActive(false);

		}
	}