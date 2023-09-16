using UnityEngine;

namespace ExampleMovedBoxes.Sokoban {
    public class Box : MonoBehaviour {

        public bool Move(Vector2 direction) {
            if (Blocked(transform.position, direction)) // если впереди преграда
                return false;

            transform.Translate(direction);// двигаем ящик

            return true;
            }

        private bool Blocked(Vector3 position, Vector2 direction) {
            Vector2 newPos = (Vector2)position + direction;// предварительно двигаем

            foreach (var wall in Spawner.Instance.Walls) { // пройдемся по стенам
                if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
                    return true;
                }

            foreach (var box in Spawner.Instance.Boxes) { // пройдемся по другим ящикам
                if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
                    return true;
                }

            return false;
            }

        }
    }