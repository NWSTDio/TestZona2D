using UnityEngine.SceneManagement;

namespace ExampleWorkEditor.DontDestroyOnLoad {
    public class GameData : Singleton<GameData> { // класс одиночка, который не разрушится при уничтожении сцены

        public string SceneName { get; private set; }
        public int Counter { get; private set; }

        private void Start() => SceneName = SceneManager.GetActiveScene().name;// вызовется 1 раз за весь цикл приложения

        public void IncreaseCounter() => Counter++;

        }
    }