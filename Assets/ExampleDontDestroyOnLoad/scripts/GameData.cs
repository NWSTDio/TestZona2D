using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExampleDontDestroyOnLoad {
    public class GameData : Singleton<GameData> { // класс одиночка, который не разрушится при уничтожении сцены

        private string _scene;
        private int _counter;

        private void Start() { // вызовется 1 раз за весь цикл приложения
            Debug.Log("Кнопка Space ув. счетчик, кнопка R перезагрузить сцену!");

            _scene = SceneManager.GetActiveScene().name;
            }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(_scene);
            }

        public void IncreaseCounter() {
            _counter++;

            ShowCounter();
            }
        public void ShowCounter() => Debug.Log("Counter: " + _counter);

        }
    }