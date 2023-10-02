using UnityEngine;

namespace NWSTDio.TurtleExample {
	public class GameController : MonoBehaviour {
		[SerializeField] private Turtle turtle;

		private Display _display;

		private void Start() {
			_display = turtle.Display;

			_display.ChangeDisplay(1600, 1000);
			_display.EnablePixel(_display.Width / 2 - 3, 0);

			turtle.SetPosition(_display.Width / 2, 0);
			turtle.SetPenSize(4);
			turtle.PenUp();
			turtle.Forward(400);
			turtle.PenDown();
			turtle.SetAngleStepTurn(15);
			turtle.Right();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.Left();
			turtle.Forward(50);
			turtle.SetPosition(_display.Width / 2, _display.Height / 2);
			turtle.SetEyeDirection(0);
			turtle.SetAngleStepTurn(90);
			turtle.Forward(50);
			turtle.Right();
			turtle.Forward(50);
			turtle.Right();
			turtle.Forward(50);
			turtle.Right();
			turtle.Forward(50);
			turtle.Right();

			_display.Render();
			}
		}
	}