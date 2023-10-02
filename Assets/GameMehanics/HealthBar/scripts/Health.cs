using System;
using UnityEngine;

namespace GameMehanics.HealthBar {
    public class Health : MonoBehaviour {

        public static Health Instance { get; private set; }
        public static event Action OnUpdateHp;// обновление хп
        public static event Action OnHeartsUpdate;// обновление количества сердец

        private readonly int _minHearts = 1, _maxHearts = 15;// мин. и макс. колл-во сердец
        private readonly float _damage = .1f;// получаемый урон
        private readonly float _heal = 1f;// количество единиц востановления хп
        private float _hearts = 5;// колл-во сердец
        private float _hp;// текущее хп

        public float MaxHp => _hearts;// макс. хп
        public float Hp => _hp;// текущее хп

        private void Awake() {
            Instance = this;

            SetHp(2);
            }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) // получить урон
                TakeDamage(_damage);

            if (Input.GetKeyDown(KeyCode.H)) // пополнить хп
                Heal(_heal);

            if (Input.GetKeyDown(KeyCode.E)) // добавить 1 сердце
                AddHeart();

            if (Input.GetKeyDown(KeyCode.Q)) // убрать 1 сердце
                RemoveHeart();

            if (Input.GetKeyDown(KeyCode.G)) // пополнить максимально хп
                SetMaxHp();
            }

        public void SetHp(int health) { // установка хп
            _hp = Mathf.Clamp(health, 0, _hearts);

            OnUpdateHp?.Invoke();
            }
        public void SetMaxHp() { // установка макс. хп
            _hp = _hearts;

            OnUpdateHp?.Invoke();
            }
        public void TakeDamage(float damage) { // получение урона
            if (_hp <= 0)
                return;

            _hp -= damage;

            OnUpdateHp?.Invoke();
            }
        public void Heal(float heal) { // восстановления здоровья
            _hp = Mathf.Clamp(_hp + heal, 0, _hearts);

            OnUpdateHp?.Invoke();
            }
        public void AddHeart() { // добавить сердечко
            if (_hearts >= _maxHearts)
                return;

            _hearts++;

            OnHeartsUpdate?.Invoke();
            }
        public void RemoveHeart() { // удалить сердечко
            if (_hearts <= _minHearts)
                return;

            _hearts--;

            if (_hp > _hearts)
                _hp = _hearts;

            OnHeartsUpdate?.Invoke();
            }

        }
    }