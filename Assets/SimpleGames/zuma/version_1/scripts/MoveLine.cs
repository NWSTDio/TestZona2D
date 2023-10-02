using UnityEngine;

namespace SimpleGames.Zuma.version_1 {
    public class MoveLine : MonoBehaviour {

        private Camera _camera;

        private void Awake() {
            _camera = Camera.main;
            }

        private void Update() {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            float angle = Vector2.Angle(Vector2.up, mousePosition - transform.position);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, transform.position.x < mousePosition.x ? -angle : angle), 10);
            }
        }
    }