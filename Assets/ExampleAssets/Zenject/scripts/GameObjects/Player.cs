using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ExampleAssets.ZenjectAsset {
    public class Player : MonoBehaviour {

        public Action OnDamage;

        private Hp _hp;

        public int CurrentHp => _hp.Value;

        [Inject]
        private void Construct(Hp hp) {
            _hp = hp;
            }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space))
                TakeDamage(Random.Range(1, 5));
            }

        public void TakeDamage(int damage) {
            _hp.IncreaseHp(damage);

            OnDamage?.Invoke();
            }

        }
    }