using JoyStick;
using UnityEngine;

namespace ExampleAssets.JoyStick {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2 : MonoBehaviour {

        [SerializeField] private FixedJoystick _joystck;

        private readonly float _speed = 10f, _jumpForce = 30f;
        private Rigidbody2D _body;
        private bool _canJump = true;

        private void Awake() {
            _body = GetComponent<Rigidbody2D>();
            }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space) && _canJump) {
                _body.velocity = new Vector2(_body.velocity.x, _jumpForce);
                _canJump = false;
                }
            }

        private void FixedUpdate() {
            _body.velocity = new Vector2(_joystck.Horizontal * _speed, _body.velocity.y);
            }

        private void OnCollisionEnter2D(Collision2D other) {
            _canJump = true;
            }

        }
    }