using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CursorUI : MonoBehaviour {

	public static CursorUI Instance { get; private set; }

	private Image _pic;

	#region MonoBehaviour
	private void Awake() {
		Instance = this;

		_pic = GetComponent<Image>();
		}
	private void Update() => transform.position = Input.mousePosition;
	private void Start() => Hide();
	#endregion

	public void Show(Sprite sprite) {
		gameObject.SetActive(true);

		transform.position = Input.mousePosition;

		_pic.sprite = sprite;
		}
	public void Hide() => gameObject.SetActive(false);

	}