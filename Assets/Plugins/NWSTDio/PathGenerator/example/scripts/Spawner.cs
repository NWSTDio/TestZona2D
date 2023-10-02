using UnityEngine;

namespace NWSTDio.PathGeneratorExample {
	public class Spawner : MonoBehaviour {

		[SerializeField] private GameObject _ball;
		[SerializeField] private Transform _container;

		private readonly int _count = 20;
		private int _counter = 0;

		private void OnTriggerExit2D(Collider2D collision) {
			if (_counter++ < _count)
				Instantiate(_ball, transform.position, Quaternion.identity, _container);
			}

		}
	}