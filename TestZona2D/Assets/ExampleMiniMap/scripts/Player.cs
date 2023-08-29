using UnityEngine;

namespace ExampleMiniMap {
    public class Player : MonoBehaviour {

        private readonly float _speed = 2f;// скорость движения игрока

        private void Update() {
            Vector3 direction = Vector3.zero;// направление движения игрока

            if (Input.GetKey(KeyCode.D))
                direction.x = 1;
            else if (Input.GetKey(KeyCode.A))
                direction.x = -1;

            if (Input.GetKey(KeyCode.W))
                direction.y = 1;
            else if (Input.GetKey(KeyCode.S))
                direction.y = -1;

            transform.position += _speed * Time.deltaTime * direction;
            }

        }
    }