using TMPro;
using UnityEngine;

namespace ExampleAngle {
    public class Main : MonoBehaviour {

        public static Main Instance { get; private set; }

        [SerializeField] private Transform _player, _player_eye, _target;
        [SerializeField] private TextMeshProUGUI _angleText, _orientationText;

        private void Awake() {
            Instance = this;
            }

        private void Start() {
            CalculateAngle();
            }

        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_player.position, _target.position);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_player.position, _player_eye.position);
            }

        public void CalculateAngle() {
            Vector2 up = _player_eye.position - _player.position;// направление взгляда игрока (можно Vector2.up если игрок не меняет взгляд)

            float angle = Mathf.Floor(Vector2.SignedAngle(up, _target.position - _player.position));// угол от -180 до 180
            // float angle = Mathf.Floor(Vector2.SignedAngle(up, _target.position - _player.position));// делает тоже самое но выдает угол без -

            _angleText.text = angle.ToString();
            _orientationText.text = "цель: " + (angle < 0 ? "c права" : (angle > 0 ? "с лева" : "по центру"));
            }

        }
    }