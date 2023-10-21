using JoyStick;
using UnityEngine;

namespace ExampleAssets.JoyStick {
    public class PlayerController : MonoBehaviour {

        [SerializeField] private FixedJoystick _joystck;

        private readonly float _speed = 2f;

        private void Update() {
            var move = new Vector2(_joystck.Horizontal, _joystck.Vertical);

            transform.position += _speed * Time.deltaTime * (Vector3)move;
            }

        }
    }