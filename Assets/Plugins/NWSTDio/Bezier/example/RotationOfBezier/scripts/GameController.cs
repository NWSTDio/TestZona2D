using UnityEngine;

namespace NWSTDio.BezierExample.Rotation {
    public class GameController : MonoBehaviour {

        [SerializeField] private Bezier _bezier;// готовый безьер
        private readonly float _speed = .1f;// скорость движения

        private readonly float _maxLength = 1f;
        private float _t;
        private bool _moving = true;

        private void Update() {
            if (Input.GetKeyUp(KeyCode.Space))
                Restart();

            if (_moving == false)
                return;

            if (_t <= _maxLength)
                _t += Time.deltaTime * _speed;
            else
                _moving = false;

            transform.SetPositionAndRotation(_bezier.GetPosition(_t), Quaternion.LookRotation(_bezier.GetRotation(_t)));
            }

        private void Restart() {
            _t = 0;
            _moving = true;
            }
        }
    }