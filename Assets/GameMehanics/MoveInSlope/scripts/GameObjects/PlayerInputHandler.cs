using UnityEngine;

namespace GameMehanics.MoveInSlope {
	public class PlayerInputHandler : MonoBehaviour {

		public Vector2Int Movement { get; private set; } // движение по оси X и Y
		public bool RunButton { get; private set; } // кнопка бега
		public bool GrabButton { get; private set; } // кнопка захвата стены
		public bool JumpButton { get; private set; } // кнопка прыжка
		public bool JumpButtonStop { get; private set; } // проверка остановки кнопки прыжка

		private readonly float _inputHoldTime = .2f;// время удержания нажатия кнопки
		private MenuUI _menu;
		private float _jumpInputStartTime;

		private void Start() => _menu = MenuUI.Instance;
		private void Update() {
			Movement = new Vector2Int() {
				x = (int)Input.GetAxisRaw("Horizontal"),
				y = (int)Input.GetAxisRaw("Vertical")
				};// движение по оси X и Y

			RunButton = Input.GetKey(KeyCode.LeftShift);// бег
			GrabButton = Input.GetKey(KeyCode.Q);// захват стены

			if (Input.GetKeyDown(KeyCode.Space)) {
				JumpButton = true;
				JumpButtonStop = false;

				_jumpInputStartTime = Time.time + _inputHoldTime;
				}
			else if (Input.GetKeyUp(KeyCode.Space))
				JumpButtonStop = true;

			CheckInputHoldTime();

			_menu.UpdateMovementButton(Movement);
			_menu.UpdateHoldButtons(RunButton, GrabButton, JumpButton);
			}


		public void UseJumpButton() => JumpButton = false;// сообщим что использовали кнопку прыжка

		private void CheckInputHoldTime() {
			if (Time.time >= _jumpInputStartTime) // если время вышло
				UseJumpButton();// отключим нажатие кнопки
			}

		}
	}