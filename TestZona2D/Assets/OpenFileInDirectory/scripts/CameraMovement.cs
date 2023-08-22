using UnityEngine;

namespace OpenFileInDirectory {
    public class CameraMovement : MonoBehaviour {

        public static CameraMovement Instance { get; private set; }

        private readonly float _minSize = 1f;// минимальные размеры камеры
        private readonly float _pixelsPerUnit = 100f;// пикселов на юнит
        private Camera _camera;// камера
        private Vector3 _dragOffset;// начальная позиция касания курсора
        private float _currentSize, _maxSize = 10f;// текущий и максимальный размер камеры
        private bool _freezeCamera;// заморожена ли камера

        private void Awake() {
            Instance = this;
            _camera = Camera.main;
            }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Q)) // заморозить камеру
                _freezeCamera = _freezeCamera == false;

            if (Input.GetKeyDown(KeyCode.E)) // переместить камеру в начальную позицию и установить начальные размеры
                MoveToStart();

            if (_freezeCamera) // если камера заморожена
                return;

            if (Input.GetAxis("Mouse ScrollWheel") > 0) // приблизить камеру
                ZoomOut();

            if (Input.GetAxis("Mouse ScrollWheel") < 0) // отдалить камеру
                ZoomIn();

            if (Input.GetMouseButtonDown(0)) // начало перемещения камеры
                _dragOffset = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButton(0)) { // перемещаем камеру
                Vector3 diff = _dragOffset - _camera.ScreenToWorldPoint(Input.mousePosition);

                diff.z = 0.0f;// ось Z не трогаем

                _camera.transform.position += diff;
                }
            }

        public void MoveToStart() { // перемещение камеры в исходную позицию и установка исходного размера
            _camera.transform.position = new Vector3(0, 0, _camera.transform.position.z);
            _currentSize = _maxSize;

            ChangeSize();
            }

        public void MaxView(int width) { // установим максимальный размер камеры (отдалим, чтобы все изображение попадало)
            _maxSize = width / _pixelsPerUnit;

            _currentSize = _maxSize;

            MoveToStart();
            }

        private void Zoom(float value) { // увеличить/уменьшить зум камеры
            _currentSize = Mathf.Clamp(value, _minSize, _maxSize);

            ChangeSize();
            }

        private void ZoomIn() { // отдалить камеру
            float speed = _currentSize / 10f;

            Zoom(_currentSize + speed);
            }
        private void ZoomOut() { // приблизить камеру
            float speed = _currentSize / 10f;

            Zoom(_currentSize - speed);
            }

        private void ChangeSize() => _camera.orthographicSize = _currentSize;// установим размер камеры

        }
    }