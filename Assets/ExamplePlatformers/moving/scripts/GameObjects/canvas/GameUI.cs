using TMPro;
using UnityEngine;

namespace ExamplePlatformer.moving {
    public class GameUI : MonoBehaviour {

        public static GameUI Instance { get; private set; }
        [SerializeField] private TextMeshProUGUI _lives;// счетчик жизней

        private void Awake() {
            Instance = this;
            }

        public void UpdateLives(int lives) => _lives.text = lives.ToString();

        }
    }