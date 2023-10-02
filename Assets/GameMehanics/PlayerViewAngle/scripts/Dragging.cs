using UnityEngine;

namespace GameMehanics.PlayerViewAngle {
    [RequireComponent(typeof(CircleCollider2D))]
    public class Dragging : MonoBehaviour { // перетаскивание обьекта

        private Camera _camera;// камера
        private Vector3 _dragOffset;// величина смещения

        private void Start() {
            _camera = Camera.main;
            }

        private void OnMouseDown() {
            _dragOffset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
            }

        private void OnMouseDrag() {
            transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)) + _dragOffset;

            Main.Instance.CalculateAngle();
            }

        }
    }