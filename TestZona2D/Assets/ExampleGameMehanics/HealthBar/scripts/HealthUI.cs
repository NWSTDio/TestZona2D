using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ExampleGameMehanics.HealthBar {

    public class HealthUI : MonoBehaviour {

        [SerializeField] private GameObject _heartPrefab;// префаб сердца

        private readonly List<Image> _hearts = new();// список сердец
        private PlayerHealth _playerHealth;// для работы с жизнью игрока

        private void Start() {
            _playerHealth = PlayerHealth.Instance;

            UpdateHearts();// обновить сердца
            }

        private void OnEnable() {
            PlayerHealth.OnHeartsUpdate += UpdateHearts;
            PlayerHealth.OnUpdateHp += UpdateVisual;
            }
        private void OnDisable() {
            PlayerHealth.OnHeartsUpdate -= UpdateHearts;
            PlayerHealth.OnUpdateHp -= UpdateVisual;
            }

        private void UpdateVisual() { // визуальное обновление
            if (_playerHealth == null)
                return;

            float hp = _playerHealth.Hp;// жизнь игрока

            foreach (Image i in _hearts) { // пройдем по всем сердцам
                i.fillAmount = Mathf.Clamp01(hp);// установим состояние закраски сердца
                hp -= 1f;
                }
            }

        private void UpdateHearts() { // обновление сердце
            if (_playerHealth == null)
                return;

            foreach (var i in _hearts) // пройдемся по всем сердцам
                Destroy(i.gameObject);// уничтожим все сердца

            _hearts.Clear();// очистим список

            for (int i = 0; i < _playerHealth.MaxHp; i++) {
                GameObject h = Instantiate(_heartPrefab, transform);
                _hearts.Add(h.GetComponent<Image>());// добавим сердце
                }

            UpdateVisual();// обновим визуально
            }

        }
    }