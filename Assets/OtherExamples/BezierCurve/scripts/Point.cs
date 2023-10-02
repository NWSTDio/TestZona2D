using UnityEngine;
using UnityEngine.EventSystems;

namespace OtherExamples.BezierCurve {
    public class Point : MonoBehaviour {

        private Vector3 _mOffset;

        private void OnMouseDown() {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            _mOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            }

        private void OnMouseDrag() {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _mOffset;

            transform.position = curPosition;
            }
        private void OnMouseUp() {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            }

        }
    }