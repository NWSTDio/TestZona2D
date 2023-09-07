using UnityEngine;

namespace ExampleMovedBoxes.Sokoban {
    public class Player : MonoBehaviour {

        private bool _readyInput;// можно ли двигатся

        private void Update() {
            Vector2 moveDirection = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            moveDirection.Normalize();

            if (moveDirection.sqrMagnitude > .5f) { // если мы держим клавишу
                if (_readyInput) { // если можно двигатся
                    _readyInput = false;// запрет на движение

                    Move(moveDirection);// двигать
                    }
                }
            else
                _readyInput = true;// разрешить движение
            }

        public bool Move(Vector2 direction) {
            if (Mathf.Abs(direction.x) < .5f) // если короткий импульс
                direction.x = 0;// запрет по оси X
            else
                direction.y = 0;// запрет по оси Y

            direction.Normalize();

            if (Blocked(transform.position, direction)) // есть ли преграда
                return false;

            transform.Translate(direction);// двигаем

            return true;
            }

        private bool Blocked(Vector3 position, Vector2 direction) {
            Vector2 newPos = (Vector2)position + direction;// предварительно двигаем

            foreach (var wall in Spawner.Instance.Walls) { // пройдемся по преградам
                if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
                    return true;// запрет движения
                }

            foreach (var box in Spawner.Instance.Boxes) { // пройдемся по ящикам
                if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
                    return box.Move(direction) == false;// можно ли двигать текущий ящик
                }

            return false;// преград нет
            }

        }
    }