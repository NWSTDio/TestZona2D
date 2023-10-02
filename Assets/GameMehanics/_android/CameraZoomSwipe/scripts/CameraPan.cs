using UnityEngine;

namespace GameMehanics.Android.CameraZoomSwipe {
    [RequireComponent(typeof(Camera))]
    public class CameraPan : MonoBehaviour {

        [SerializeField] private SpriteRenderer _renderer;
        private readonly float _minSize = 1f, _maxSize = 25f;// предельные размеры камеры
        private readonly float _minDifference = 2f;// минимальная разница при которой засчитывает свайп
        private Camera _camera;
        private Vector3 _startPosition;// начальная позиция касания, при перемещениях
        private float _currentSize;// текущий размер камеры
        private bool _multiTouch;// для проверки мультитач или нет
        private Rect _bounds;// границы перемещения камеры

        private void Awake() {
            _camera = GetComponent<Camera>();
            }

        private void Start() {
            _currentSize = _maxSize;// текущие размеры камеры

            float ppu = _renderer.sprite.pixelsPerUnit;// пикселей на юнит, чтобы правильно расчитать размер изображения в юнитах

            // получим половины размеров текстуры
            int bounds_x = _renderer.sprite.texture.width / 2;
            int bounds_y = _renderer.sprite.texture.height / 2;

            _bounds = new Rect() {
                xMin = -bounds_x / ppu,
                xMax = bounds_x / ppu,
                yMin = -bounds_y / ppu,
                yMax = bounds_y / ppu
                };// создадим границы камеры

            ChangeSize();// установим размеры
            }

        private void Update() {
            if (Application.platform == RuntimePlatform.Android) { // если запущено на андроид
                if (Input.touchCount == 1) { // если 1 палец
                    Touch touch = Input.GetTouch(0);// данные пальца

                    if (touch.phase == TouchPhase.Began || _multiTouch) { // если начало касания или был режим мультитач
                        _multiTouch = false;// уберем мультитач

                        _startPosition = _camera.ScreenToWorldPoint(touch.position);// запомним стартовую позицию курсора
                        }

                    if (touch.phase == TouchPhase.Moved && _multiTouch == false) { // если режим перемещение и не мультитач
                        Vector3 diff = _startPosition - _camera.ScreenToWorldPoint(Input.mousePosition);

                        _camera.transform.position += diff;// переместим камеру
                        }
                    }
                else if (Input.touchCount == 2) { // если мультитач (2 пальца)
                    _multiTouch = true;// включим мультитач

                    Touch firstTouch = Input.GetTouch(0);// первый палец
                    Touch secondTouch = Input.GetTouch(1);// второй палец

                    // предыдущие позиции пальцев
                    Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
                    Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

                    // разницы между текущими и предыдущими позициями в виде магнитуд
                    float touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
                    float touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                    float difference = touchesCurPosDifference - touchesPrevPosDifference;// разница между текущей магнитудой и предыдущей

                    if (Mathf.Abs(difference) >= _minDifference) { // если она больше минимума
                        if (touchesPrevPosDifference > touchesCurPosDifference)
                            ZoomIn();

                        if (touchesPrevPosDifference < touchesCurPosDifference)
                            ZoomOut();
                        }

                    }
                }

            else { // если с компа (редактора)
                if (Input.GetMouseButtonDown(0)) // если курсор кликнул
                    _startPosition = _camera.ScreenToWorldPoint(Input.mousePosition);// запомним стартовые позиции

                if (Input.GetMouseButton(0)) { // если курсор зажат
                    Vector3 diff = _startPosition - _camera.ScreenToWorldPoint(Input.mousePosition);

                    _camera.transform.position += diff;// двигаем камеру
                    }
                }

            // зум при помощи колесика мыши
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                ZoomOut();

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                ZoomIn();

            transform.position = new Vector3() {
                x = Mathf.Clamp(transform.position.x, _bounds.xMin, _bounds.xMax),
                y = Mathf.Clamp(transform.position.y, _bounds.yMin, _bounds.yMax),
                z = transform.position.z
                };// ограничим передвижение камеры

            }

        private void Zoom(float value) { // смена зума
            _currentSize = Mathf.Clamp(value, _minSize, _maxSize);// ограничим значение

            ChangeSize();// сменим размер камеры
            }

        private void ZoomIn() { // увеличим зум (отдалим)
            float speed = _currentSize / 10f;

            Zoom(_currentSize + speed);// обновим зум
            }
        private void ZoomOut() { // уменьшим зум (приблизим)
            float speed = _currentSize / 10f;

            Zoom(_currentSize - speed);// обновим зум
            }

        private void ChangeSize() => _camera.orthographicSize = _currentSize;

        }
    }