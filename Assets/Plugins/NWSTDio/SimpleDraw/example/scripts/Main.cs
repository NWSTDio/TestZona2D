using UnityEngine;

namespace NWSTDio.SimpleDrawExample {
    public class Main : MonoBehaviour {

        [SerializeField] private SimpleDraw _draw;

        private Display _display;

        private void Start() {
            _display = _draw.Display;
            _display.ChangeDisplay(1600, 1000);

            _draw.DrawLine(1, 1, 105, 105, 5);

            _display.Render();
            }

        }
    }