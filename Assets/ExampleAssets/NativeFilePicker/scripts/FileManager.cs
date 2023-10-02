using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace ExampleAssets.NativeFilePickerAsset {
    public class FileManager : MonoBehaviour {

        public static FileManager Instance { get; private set; }

        [SerializeField] private SpriteRenderer _pictureRenderer;// картинка на экране

        private readonly string _name = "pic.isave";// имя картинки
        private byte[] _pic;// картинка в байтах

        private void Awake() {
            Instance = this;
            }

        private void Start() {
            if (OpenFile() == false) // если не удалось загрузить картинку из сохранений
                CameraMovement.Instance.MaxView(_pictureRenderer.sprite.texture.width);// репозицинируем камеру относительно стандартной
            }

        public void LoadFile() { // загрузка файла из каталога
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

                CreateTexture(path);// загрузим текстуру
            }, new string[] { png, jpg, jpeg });// открываем просмотр файлов
            }

        public void SaveFile() { // сохранить файл в папке сохранений
            if (_pic == null || _pic.Length <= 0) // если данных нет
                return;

            string path = Application.persistentDataPath + "/" + _name;// полный путь к файлу и его имя

            var file = File.Create(path);// создаем файл

            for (int i = 0; i < _pic.Length; i++) // пройдем по всему файлу
                file.WriteByte(_pic[i]);// запишем байт файла

            file.Close();// закроем файл
            }

        private IEnumerator LoadImageByRequest(string path) { // загрузка изображения через WebRequest
            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("ERROR: " + www.error);
            else
                ChangeImage(DownloadHandlerTexture.GetContent(www));
            }

        private bool OpenFile() { // открыть изображение из сохранений
            string path = Application.persistentDataPath + "/" + _name;

            if (File.Exists(path) == false) // если ничего нет
                return false;

            CreateTexture(path);

            return true;
            }

        private void ChangeImage(Texture2D texture) { // обновление картинки
            _pictureRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.Tight);
            CameraMovement.Instance.MaxView(texture.width);// репозиционируем камеру
            }

        private void CreateTexture(string path) { // получение текстуры из набора байтов
            _pic = File.ReadAllBytes(path);// считаем весь файл

            var loadTexture = new Texture2D(1, 1);// создадим текстуру
            loadTexture.LoadImage(_pic);// загрузим в нее картинку

            ChangeImage(loadTexture);// установим картинку
            }

        }
    }