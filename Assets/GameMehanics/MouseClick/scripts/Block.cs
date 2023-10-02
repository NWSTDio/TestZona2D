using UnityEngine;

namespace GameMehanics.MouseClick {
    public class Block : MonoBehaviour {

        [SerializeField] private SpriteRenderer _sprite;// картинка игрового обьекта

        private void OnMouseDown() { // если нажали на обьект
            if (Functions.IsClickUIObject()) // если был клик по UI
                return;

            _sprite.color = Functions.GetRandomColor(_sprite.color.a);
            }

        }
    }