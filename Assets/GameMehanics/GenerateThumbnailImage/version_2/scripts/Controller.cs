using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace GameMehanics.GenerateThumbnailImage.version_2 {
	public class Controller : MonoBehaviour {

		[SerializeField] private Button _open, _save;
		[SerializeField] private Image _image;
		[SerializeField] private Slider _slider;


		private Texture2D _thumbnail, _original;
		private float _compression, _max;

		private void Awake() {
			_open.onClick.AddListener(() => {
				OpenFile();
			});
			_save.onClick.AddListener(() => {
				Save();
			});
			}

		private void Start() {
			_compression = _slider.value;
			}

		private void OpenFile() {
			if (NativeFilePicker.IsFilePickerBusy())
				return;

			// поддерживаемые расширения картинок
			string png = NativeFilePicker.ConvertExtensionToFileType("png");
			string jpg = NativeFilePicker.ConvertExtensionToFileType("jpg");
			string jpeg = NativeFilePicker.ConvertExtensionToFileType("jpeg");

			NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) => {
				if (path == null) {
					Debug.Log("ERROR!");

					return;
					}

				_original = Utills.LoadTexture2D(path);

				int max = Mathf.Max(_original.width, _original.height);

				_max = 3000f / max * .1f;

				Compress();

			}, new string[] { png, jpg, jpeg });// открываем просмотр файлов
			}

		private void Compress() {
			_thumbnail = Utills.Compress(_original, _compression);

			Show();
			}

		public void ValueChanged() {
			if (_original == null)
				return;

			if (_slider.value >= _max)
				_slider.value = _max;

			_compression = _slider.value;

			Compress();
			}

		private void Show() {
			_image.sprite = Sprite.Create(_thumbnail, new Rect(0, 0, _thumbnail.width, _thumbnail.height), new Vector2(.5f, .5f), 100f, 0, SpriteMeshType.Tight);
			}

		private void Save() {
			byte[] bytes = _thumbnail.EncodeToPNG();
			File.WriteAllBytes(Application.persistentDataPath + "/i", bytes);
			}

		}
	}