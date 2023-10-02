using System;
using UnityEngine;

namespace NWSTDio.BezierExample {
    public class MovingBall : MonoBehaviour {

        [SerializeField] private Bezier _bezier;

        private readonly float _speed = .1f;
        private float _t;
        private bool _moving;

        private void Start() {
            Restart();
            }

        private void OnEnable() {
            GameController.OnClickRestart += ClickRestart;
            }
        private void OnDisable() {
            GameController.OnClickRestart -= ClickRestart;
            }

        private void Update() {
            if (_moving == false)
                return;

            if (_t < 1f)
                _t += _speed * Time.deltaTime;
            else
                _moving = false;

            transform.position = _bezier.GetPosition(_t);
            }

        private void Restart() {
            _moving = true;
            _t = 0;
            }

        private void ClickRestart(object sender, EventArgs e) {
            Restart();
            }

        }
    }