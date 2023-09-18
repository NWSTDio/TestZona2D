using TMPro;
using UnityEngine;

namespace ExampleGameMehanics.FormattingNumbers {
    public class Controller : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _numText;

        private double _num;

        private double _click = 1;
        private double _price = 100;

        private void Update() {
            _num += _click;

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (_num < _price)
                    return;

                _click *= 100;
                _num -= _price;

                _price *= 10;
                }

            UpdateVisual();
            }

        private void UpdateVisual() {
            _numText.text = FormatNumber.Format(_num);
            }

        }
    }