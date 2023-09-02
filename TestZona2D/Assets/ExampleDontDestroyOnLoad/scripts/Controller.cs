using UnityEngine;

namespace ExampleDontDestroyOnLoad {
    public class Controller : MonoBehaviour { // создается при каждой загрузки сцены

        private void Start() {
            Debug.Log("Controller.Start()");

            GameData.Instance.ShowCounter();
            }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space))
                GameData.Instance.IncreaseCounter();
            }

        }
    }