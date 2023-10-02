using UnityEngine;

namespace SimpleGames.Platformer {
	public class CameraController : MonoBehaviour {

		[SerializeField] private Transform _target;// цель за кем следовать

		private void Update() {
			var pos = _target.position;

			pos.z = transform.position.z;// ограничим движение по оси Z

			transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
			}

		}
	}