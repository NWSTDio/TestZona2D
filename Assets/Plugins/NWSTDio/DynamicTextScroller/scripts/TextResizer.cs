using UnityEngine;
using UnityEngine.UI;

namespace NWSTDio {
	[RequireComponent(typeof(Text), typeof(RectTransform))]
	public class TextResizer : MonoBehaviour {

		[SerializeField] private RectTransform ScrollContent;

		private Text _content;
		private RectTransform _rect;

		public float Height => _rect.sizeDelta.y;

		private void Start() {
			_content = GetComponent<Text>();
			_rect = GetComponent<RectTransform>();
			}
		private void Update() {
			if (Height != _content.preferredHeight)
				Resize();
			}
		public void SetText(string text) {
			_content.text = text;

			Resize();
			}

		private void Resize() {
			_rect.sizeDelta = new Vector2(_rect.sizeDelta.x, _content.preferredHeight);

			if (ScrollContent != null)
				ScrollContent.sizeDelta = new Vector2(ScrollContent.sizeDelta.x, _content.preferredHeight);
			}
		}
	}