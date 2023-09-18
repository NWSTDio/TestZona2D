using UnityEngine;

namespace ExampleGameMehanics.CameraShake {
    public class CameraShake : MonoBehaviour {

        private readonly float _shakeTimerSpeed = 1.5f;// скорость исчезновение эффекта стряски камеры
        private readonly float _shakeAmount = 0.1f;// величина тряски камеры
        private Camera _camera;
        private Vector3 _startPosition;
        private float _shakeTimer;

        public bool IsShake => _shakeTimer > 0;

        private void Awake() {
            _camera = Camera.main;
            }

        private void Start() {
            _startPosition = _camera.transform.localPosition;
            }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space))
                Shake();

            if (IsShake == false)
                return;

            _shakeTimer -= Time.deltaTime * _shakeTimerSpeed;

            _camera.transform.localPosition = _startPosition + Random.insideUnitSphere * _shakeAmount;
            }

        public void Shake() {
            if (IsShake)
                return;

            _shakeTimer = 1f;
            }

        }
    }