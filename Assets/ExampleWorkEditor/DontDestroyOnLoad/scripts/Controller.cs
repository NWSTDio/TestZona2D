using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExampleWorkEditor.DontDestroyOnLoad {
    public class Controller : MonoBehaviour { // создается при каждой загрузки сцены

        [SerializeField] private TextMeshProUGUI _counter;

        private void Start() => UpdateVisual();
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                GameData.Instance.IncreaseCounter();

                UpdateVisual();
                }

            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(GameData.Instance.SceneName);
            }

        private void UpdateVisual() => _counter.text = GameData.Instance.Counter.ToString();

        }
    }