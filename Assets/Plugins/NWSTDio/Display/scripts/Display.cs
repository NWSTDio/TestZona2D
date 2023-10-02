using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NWSTDio {
	[RequireComponent(typeof(RawImage))]

	public class Display : MonoBehaviour {

		[SerializeField] private Color bgColor, pixelOffColor, pixelOnColor, borderColor;
		private RawImage _display;
		private Texture2D _buffer;
		private List<int> _pixels;
		private int _displayWidth = 64, _displayHeight = 32;// max 1590 * 918
		private int _pixelSize;
		private int _offsetX, _offsetY;
		private int _borderSize = 5;
		private int _screenWidth, _screenHeight;

		public int Width => _displayWidth;
		public int Height => _displayHeight;

		private void Awake() {
			_display = GetComponent<RawImage>();

			_screenWidth = _display.texture.width;
			_screenHeight = _display.texture.height;

			ChangeDisplay(_displayWidth, _displayHeight);
			}

		public void ChangeDisplay(int width, int height) {
			_displayWidth = width;
			_displayHeight = height;

			DisplayInit();
			Prepare();
			Info();
			Render();
			}

		public void EnablePixel(int x, int y) {
			if (IsBadAddress(x, y, out int address))
				return;

			if (_pixels[address] == 1)
				return;

			DrawPixel(x, y, _pixelSize, _pixelSize, pixelOnColor);

			_pixels[address] = 1;
			}

		public void DisablePixel(int x, int y) {
			if (IsBadAddress(x, y, out int address))
				return;

			if (_pixels[address] == 0)
				return;

			DrawPixel(x, y, _pixelSize, _pixelSize, pixelOffColor);

			_pixels[address] = 0;
			}

		public void Render() {
			_buffer.Apply();

			_display.texture = _buffer;
			}

		public void Info() {
			Debug.Log("W: " + _displayWidth + "px H: " + _displayHeight + "px P: " + _pixelSize + "px OFFSX: " + _offsetX + "px OFFSY: " + _offsetY + "px " + " SUM PXL: " + _pixels.Count);
			}

		public void Clear() {
			for (int i = 0, max_i = _displayWidth * _displayHeight; i < max_i; i++) {
				if (_pixels[i] == 0)
					continue;

				int x = i % _displayWidth;
				int y = i / _displayWidth;

				DisablePixel(x, y);

				_pixels[i] = 0;
				}
			}

		private void DisplayInit() {
			int width = _screenWidth - (_borderSize * 2);
			int height = _screenHeight - (_borderSize * 2);

			if (_displayWidth > width || _displayWidth < 1)
				_displayWidth = width;

			if (_displayHeight > height || _displayHeight < 1)
				_displayHeight = height;

			int pw = width / _displayWidth;
			int ph = height / _displayHeight;

			_pixelSize = (pw == ph || pw < ph) ? pw : ph;

			if (_pixelSize == 0)
				_pixelSize = 1;

			_offsetX = (_screenWidth - (_displayWidth * _pixelSize)) / 2;
			_offsetY = (_screenHeight - (_displayHeight * _pixelSize)) / 2;
			}

		private void Prepare() {
			_pixels = new List<int>();

			for (int i = 0; i < _displayWidth * _displayHeight; i++)
				_pixels.Add(0);

			_buffer = new Texture2D(_screenWidth, _screenHeight, TextureFormat.RGB24, false) {
				filterMode = FilterMode.Point
				};

			FillBuffer();
			}
		private void FillBuffer() {
			for (int i = 0, max_i = _buffer.width; i < max_i; i++)
				for (int j = 0, max_j = _buffer.height; j < max_j; j++)
					_buffer.SetPixel(i, j, bgColor);

			for (int i = _offsetY, max_i = _displayHeight * _pixelSize + _offsetY; i < max_i; i++) {
				int start = _offsetX;

				for (int j = 1, max_j = _borderSize; j <= max_j; j++)
					_buffer.SetPixel(start - j, i, borderColor);

				start = _offsetX + (_displayWidth * _pixelSize);

				for (int j = 0, max_j = _borderSize; j < max_j; j++)
					_buffer.SetPixel(j + start, i, borderColor);
				}
			for (int i = _offsetX - 5, max_i = _displayWidth * _pixelSize + _offsetX + 5; i < max_i; i++) {
				int start = _offsetY;

				for (int j = 1, max_j = _borderSize; j <= max_j; j++)
					_buffer.SetPixel(i, start - j, borderColor);

				start = _offsetY + (_displayHeight * _pixelSize);

				for (int j = 0, max_j = _borderSize; j < max_j; j++)
					_buffer.SetPixel(i, j + start, borderColor);
				}
			}

		private void DrawPixel(int x, int y, int width, int height, Color color) {
			for (int i = x * width, max_i = x * width + width; i < max_i; i++)
				for (int j = y * height, max_j = y * height + height; j < max_j; j++)
					_buffer.SetPixel(i + _offsetX, j + _offsetY, color);
			}

		private bool IsBadAddress(int x, int y, out int address) {
			address = x + (y * _displayWidth);

			if (x < 0 || x >= _displayWidth)
				return true;

			if (y < 0 || y >= _displayHeight)
				return true;

			if (address >= _pixels.Count)
				return true;

			return false;
			}

		}
	}