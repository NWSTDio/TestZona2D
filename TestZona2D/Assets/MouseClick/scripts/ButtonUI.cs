using UnityEngine;
using UnityEngine.UI;

namespace MouseClick {
    public class ButtonUI : MonoBehaviour { // UI кнопка, которая находится над обьектом

        private Image _image;

        private void Awake() {
            _image = GetComponent<Image>();

            GetComponent<Button>().onClick.AddListener(() => {
                _image.color = Functions.GetRandomColor(_image.color.a);
            });// клик по кнопке
            }

        }
    }