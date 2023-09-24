using DG.Tweening;
using UnityEngine;

namespace ExampleGameMehanics.CameraShake2 {
	public class CameraShake : MonoBehaviour {

		[SerializeField] private Vector3 _shakeAltitude;
		[SerializeField] private float _shakeTime;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space))
				ShakeCamera();
			}

		private void ShakeCamera() {
			transform.DOComplete();
			transform.DOShakePosition(_shakeTime, _shakeAltitude);
			}

		}
	}