using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public class PlayerInputHandler : MonoBehaviour { // обработчик ввода

		public Vector2Int Movement { get; private set; } // движение по оси X и Y
		public Vector2 RawDashDirection { get; private set; } // направление рывка в необработанном состоянии
		public Vector2Int DashDirection { get; private set; } // направление рывка в обработанном состоянии
		public bool RunButton { get; private set; } // кнопка бега
		public bool JumpButton { get; private set; } // кнопка прыжка
		public bool DashButton { get; private set; } // кнопка рывка
		public bool JumpButtonStop { get; private set; } // проверка остановки кнопки прыжка
		public bool DashButtonStop { get; private set; } // проверка остановки кнопки рывка
		public bool GrabButton { get; private set; } // кнопка захвата стены

		private readonly float _inputHoldTime = .2f;// время удержания нажатия кнопки
		private Camera _camera;
		private float _jumpInputStartTime, _dashInputStartTime;// стартовое время нажатий прыжка и рывка

		private void Start() => _camera = Camera.main;
		private void Update() {
			Movement = new Vector2Int() {
				x = (int)Input.GetAxisRaw("Horizontal"),
				y = (int)Input.GetAxisRaw("Vertical")
				};// движение по оси X и Y 

			RunButton = Input.GetKey(KeyCode.LeftShift);// бег
			GrabButton = Input.GetKey(KeyCode.LeftControl);// держатся на стене

			if (Input.GetKeyDown(KeyCode.Q)) {
				DashButton = true;
				DashButtonStop = false;

				_dashInputStartTime = Time.time + _inputHoldTime;
				}

			if (Input.GetKeyUp(KeyCode.Q))
				DashButtonStop = true;

			if (Input.GetKeyDown(KeyCode.Space)) {
				JumpButton = true;
				JumpButtonStop = false;

				_jumpInputStartTime = Time.time + _inputHoldTime;
				}

			if (Input.GetKeyUp(KeyCode.Space))
				JumpButtonStop = true;

			if (Input.GetMouseButton(0))
				DashInput();

			CheckInputHoldTime();
			}

		public void UseJumpButton() => JumpButton = false;// сообщим что использовали кнопку прыжка
		public void UseDashButton() => DashButton = false;// сообщим что использовали кнопку рывка

		private void CheckInputHoldTime() {
			if (Time.time >= _jumpInputStartTime) // если время вышло
				UseJumpButton();// отключим нажатие кнопки

			if (Time.time >= _dashInputStartTime) // если время вышло
				UseDashButton();// отключим нажатие кнопки
			}
		private void DashInput() {
			RawDashDirection = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

			DashDirection = Vector2Int.RoundToInt(RawDashDirection.normalized);
			} // получение направление рывка

		}
	}