using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMehanics.MoveInSlope {
	public class MenuUI : MonoBehaviour {

		public static MenuUI Instance { get; private set; }

		[SerializeField] private Transform _infoPanel;// панель со всей инофрмацией
		[SerializeField] private Button _exitButton;// кнопка выхода

		[SerializeField] private TextMeshProUGUI _state, _prevState;// текущее и предыдущее состояние игрока
		[SerializeField] private TextMeshProUGUI _velocity;// скорость движения
		[SerializeField] private TextMeshProUGUI _gravotyScale;// сила гравитации
		[SerializeField] private TextMeshProUGUI _position;// позиция игрока
		[SerializeField] private TextMeshProUGUI _isOnGround, _isOnSlope;// игрок на земле, на на клонной поверхности
		[SerializeField] private TextMeshProUGUI _isTouchWall, _isTouchLedge, _isTouchCeiling;// игрок касается стены, уступа, потолка
		[SerializeField] private TextMeshProUGUI _isLadder;// игрок у лестници?
		[SerializeField] private TextMeshProUGUI _isTrigger;// игрок триггер или нет
		[SerializeField] private TextMeshProUGUI _slopeAngle;// угол наклонной поверхности
		[SerializeField] private TextMeshProUGUI _friction;// трение
		[SerializeField] private TextMeshProUGUI _canJump;// может ли прыгать
		[SerializeField] private TextMeshProUGUI _facingDirection;// направление взгляда

		[SerializeField] private TextMeshProUGUI _bool;// любое будево значение
		[SerializeField] private TextMeshProUGUI _movementButton;// кнопки движения
		[SerializeField] private TextMeshProUGUI _holdButtons;// кнопки удержания бег, прыжок, захват

		[SerializeField] private TextMeshProUGUI _keysInfo;// помощь по навигации
		[SerializeField] private TextMeshProUGUI _infoKeyText;// сообщение о показе скрытие панели

		private CanvasGroup _gruop;// для контроля прозрачности панели информации
		private float _minimalAlpha;// минимальная прозрачность панели информации

		private void Awake() {
			Instance = this;

			_gruop = _infoPanel.GetComponent<CanvasGroup>();
			_minimalAlpha = _gruop.alpha;

			_exitButton.onClick.AddListener(() => {
				Application.Quit();
			});
			}
		private void Update() {
			if (Input.GetKeyDown(KeyCode.P)) {
				_infoPanel.gameObject.SetActive(_infoPanel.gameObject.activeSelf == false);

				_infoKeyText.text = "Click [<color=green>P</color>] from " + (_infoPanel.gameObject.activeSelf ? "Hide" : "Show") + " Status Information";
				}

			if (Input.GetKey(KeyCode.O)) {
				_gruop.alpha += .005f;

				if (_gruop.alpha >= 1f)
					_gruop.alpha = 1f;
				}
			if (Input.GetKey(KeyCode.I)) {
				_gruop.alpha -= .005f;

				if (_gruop.alpha <= _minimalAlpha)
					_gruop.alpha = _minimalAlpha;
				}
			}

		public void UpdateState(string nameState) => _state.text = "State: " + nameState;
		public void UpdatePrevState(string nameState) => _prevState.text = "PrevState: " + nameState;
		public void UpdateVelocity(float xVelocity, float yVelocity) => _velocity.text = $"Velocity: X={xVelocity.Round():00.00} Y={yVelocity.Round():00.00}";
		public void UpdateGravityScale(float gravity) => _gravotyScale.text = "GravityScale: " + gravity;
		public void UpdatePosition(Vector3 position) => _position.text = $"Position: X={position.x.Round():00.00} Y={position.y.Round():00.00} Z={position.z.Round():00.00}";
		public void UpdateIsOnGround(bool value) => _isOnGround.text = "IsOnGround: " + value;
		public void UpdateIsOnSlope(bool value) => _isOnSlope.text = "IsOnSlope: " + value;
		public void UpdateIsTouchWall(bool value) => _isTouchWall.text = "IsTouchWall: " + value;
		public void UpdateIsTouchLedge(bool value) => _isTouchLedge.text = "IsTouchLedge: " + value;
		public void UpdateIsTouchCeiling(bool value) => _isTouchCeiling.text = "IsTouchCeiling: " + value;
		public void UpdateIsLadder(bool value) => _isLadder.text = "IsLadder: " + value;
		public void UpdateIsTrigger(bool value) => _isTrigger.text = "IsTrigger: " + value;
		public void UpdateSlopeAngle(float angle) => _slopeAngle.text = "SlopeAngle: " + angle;
		public void UpdateFriction(string frictionName) => _friction.text = "Friction: " + frictionName;
		public void UpdateCanJump(bool value) => _canJump.text = "CanJump: " + value;
		public void UpdateFacingDirection(int direction) => _facingDirection.text = "FacingDirection: " + (direction == 1 ? "Right" : "Left");
		public void UpdateBool(string msg, bool value) => _bool.text = $"{msg}: {value}";
		public void UpdateKeysInfo(string msg) => _keysInfo.text = msg;
		public void ClearKeysInfo() => _keysInfo.text = "";

		public void UpdateMovementButton(Vector2 movement) => _movementButton.text = $"Movement: X={movement.x} Y={movement.y}";
		public void UpdateHoldButtons(bool run, bool grab, bool jump) => _holdButtons.text = $"Hold Buttons: Run={run} Grab={grab} Jump={jump}";

		}
	}