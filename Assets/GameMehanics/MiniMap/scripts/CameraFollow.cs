using UnityEngine;

namespace GameMehanics.MiniMap {
    public class CameraFollow : MonoBehaviour {

        [SerializeField] private Transform _target;// цель следования камеры

        private readonly float _speed = 5f;// скорость следования камеры

        private void LateUpdate() {
            var position = new Vector3() {
                x = _target.position.x,
                y = _target.position.y,
                z = transform.position.z
                };// ограничим движение по оси Z

            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * _speed);
            }

        }
    }