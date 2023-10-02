using UnityEngine;

namespace GameMehanics.GunTrackMouse {
    public class PlayerController : MonoBehaviour {

        [SerializeField] private Gun _gun;// пушка
        [SerializeField] private float speed;// скорость игрока
        [SerializeField, Range(0, 45)] private int _deadZoneAngle;// для просчета мертвой зоны

        private readonly float _startAngle = 90;// начальный угол проверки
        private Rigidbody2D rb;
        private bool _isMouseInPlayer;// если курсор над игроком

        private bool IsRightFace => transform.localScale.x >= 0;// направление взгляда игрока

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            }

        private void FixedUpdate() {
            float xAxisMovement = Input.GetAxis("Horizontal");// направление движения

            rb.velocity = new Vector2(xAxisMovement * speed, rb.velocity.y);// двигаем игрока

            if (xAxisMovement == 0) { // если стоим
                if (_isMouseInPlayer) // если курсор над игроком
                    return;

                float rotation = Mathf.Abs(_gun.rotationZ);

                if (IsRightFace && rotation > _startAngle + _deadZoneAngle) // если смотрим вправо и угол больше мертвой зоны справа
                    Flip();
                else if (IsRightFace == false && rotation < _startAngle - _deadZoneAngle) // если смотрим влево и угол больше мертвой зоны слева
                    Flip();
                }
            else { // если в движении
                if (IsRightFace && xAxisMovement < 0)
                    Flip();
                else if (IsRightFace == false && xAxisMovement > 0)
                    Flip();
                }
            }

        private void OnMouseOver() { // курсор над игроком
            _isMouseInPlayer = true;
            }

        private void OnMouseExit() { // курсор покинул игрока
            _isMouseInPlayer = false;
            }

        private void Flip() { // отражение игрока
            Vector3 scale = transform.localScale;

            scale.x *= -1;

            transform.localScale = scale;
            }

        }
    }

/*

    Если ты хочеш чтобы поле было видно в инспекторе, например скрость то ставь так:
    [SerializeField] private float speed;
    это дает ясность в код и указывает на то что поле (переменная) не будет использована вне класса игрок

    Если поле не будет менятся в коде программы то делай его
    private readonly float offset = -90;

    Все поля желательно делать приватными, т.е. в начале писать private

    Если ты хочеш предоставить доступ к полу другим классам то делай свойство, т.е например у тебя есть скрость
    private float _speed;
    Свойство
    public float Speed => _speed;
    Разрешит другим классам например пушке получить доступ к скорости но никогда не сможет ее поменять, если тебе нужно будет поменять скорость, то делай метод
    public void SetSpeed(float speed) {
        // в котором ты можеш например органичить скорость, чтобы не было проблем
        _speed = Mathf.Clamp(_speed, 0, 100);// ограничит скорость от 0 до 100, чтобы не вводили отрицательную скорость или слишком большую
        
        
        _speed = speed;// ну или не проверяй ничего))
        }

    Публичные поля плохо!

    По игре: чтобы не дергало игрока, нужно настраивать мертвые зоны за которыми не будет поворачивать игрока когда оружие повоачивается
    В пейнте обьясню что и как))

*/