using UnityEngine;

namespace GameMehanics.GunTrackMouse {
    public class Bullet : MonoBehaviour {

        private readonly float _speed = 5f;
        private float _bulletLifeTime = 5f;

        private void Update() {
            if (_bulletLifeTime <= 0)
                Destroy(gameObject);
            else
                _bulletLifeTime -= Time.deltaTime;

            transform.Translate(_speed * Time.deltaTime * Vector2.up);
            }

        }
    }