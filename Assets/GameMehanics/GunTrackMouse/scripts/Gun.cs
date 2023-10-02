using UnityEngine;

namespace GameMehanics.GunTrackMouse {
    public class Gun : MonoBehaviour {

        public float rotationZ;// значение угла поворота пушки

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;

        private readonly float offset = -90;// смещение угла пушки
        private readonly float _timeShootDelay = .5f;// задержка выстрела
        private Camera _camera;// камера
        private float _timerShootDelay;// текущее время задержки выстрела

        private void Start() {
            _camera = Camera.main;// обязательно нужно кешировать запросс к камере!!!
            }

        public void Update() {
            Vector3 difference = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + offset);

            if (_timerShootDelay <= 0) { // если можно стрелять
                if (Input.GetMouseButton(0)) { // ЛКМ
                    _timerShootDelay = _timeShootDelay;// задержка перед стрельбой

                    Instantiate(_bulletPrefab, _bulletSpawnPoint.position, transform.rotation);
                    }
                }
            else
                _timerShootDelay -= Time.deltaTime;
            }
        }
    }