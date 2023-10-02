using UnityEngine;

namespace NWSTDio.PathGeneratorExample {
	public class Despawner : MonoBehaviour {

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.CompareTag("Ball") == false)
				return;

			Destroy(collision.gameObject);
			}

		}
	}