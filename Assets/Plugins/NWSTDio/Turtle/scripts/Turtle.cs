using System.Collections.Generic;
using UnityEngine;

namespace NWSTDio {
	public class Turtle : MonoBehaviour {

		[SerializeField] private SimpleDraw _draw;

		private List<Memory> memory;
		private Vector2 _position;
		private float _eyeDirection = 0;
		private int _angleStepTurn = 0;
		private bool _isDraw = false;
		private float _penSize = 1;

		public Display Display => _draw.Display;
		public Vector2 Position => _position;
		public float EyeDirecton => _eyeDirection;
		public int Angle => _angleStepTurn;
		public float PenSize => _penSize;

		private void Awake() {
			memory = new List<Memory>();
			}

		public void SetPosition(float x, float y) {
			_position = new Vector2(x, y);
			}
		public void SetX(float x) => _position.x = x;
		public void SetY(float y) => _position.y = y;

		public void SetEyeDirection(float angle) => _eyeDirection = angle;

		public void SetAngleStepTurn(int angle) => _angleStepTurn = angle;

		public void PenUp() => _isDraw = false;
		public void PenDown() => _isDraw = true;

		public void SetPenSize(float size) => _penSize = size;

		public void Forward(int length) => Move(true, length);
		public void Backward(int length) => Move(false, length);

		public void Left() => LeftAngle(_angleStepTurn);
		public void LeftAngle(float angle) {
			if (_eyeDirection < 0)
				_eyeDirection += 360;

			_eyeDirection -= angle;
			}
		public void Right() => RightAngle(_angleStepTurn);
		public void RightAngle(float angle) {
			if (_eyeDirection >= 360)
				_eyeDirection -= 360;

			_eyeDirection += angle;
			}

		public void MemoryAdd() {
			memory.Add(new() {
				Position = _position,
				EyeDirecton = _eyeDirection,
				PenSize = _penSize
				});
			}

		public void MemoryGet() {
			Memory tmp = memory[^1];

			SetPosition(tmp.Position.x, tmp.Position.y);
			SetPenSize(tmp.PenSize);
			SetEyeDirection(tmp.EyeDirecton);

			memory.RemoveAt(memory.Count - 1);
			}

		private void Move(bool fd, int length) {
			float tx = length * Mathf.Sin(ToRadians(_eyeDirection));
			float ty = length * Mathf.Cos(ToRadians(_eyeDirection));

			float dx = fd ? _position.x + tx : _position.x - tx;
			float dy = fd ? _position.y + ty : _position.y - ty;

			if (_isDraw)
				_draw.DrawLine((int)_position.x, (int)_position.y, (int)dx, (int)dy, _penSize);

			_position.x = dx;
			_position.y = dy;
			}

		private float ToRadians(float angle) => Mathf.PI * angle / 180;

		}

	public struct Memory {

		public Vector2 Position;
		public float EyeDirecton;
		public float PenSize;

		}
	}