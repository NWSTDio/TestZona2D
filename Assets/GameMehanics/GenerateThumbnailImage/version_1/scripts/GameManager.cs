using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace GameMehanics.GenerateThumbnailImage.version_1 {
    public class GameManager : MonoBehaviour {

        [SerializeField] private Image _original, _result;// оригинал и уменьшенная картинки

        private void Start() {
            Texture2D texture = Utilities.CompressImage(_original.sprite.texture, .1f);// уменьшаем текстуру

            File.WriteAllBytes(Application.persistentDataPath + "/thumbnail.isave", texture.EncodeToJPG());// сохраняем уменьшенную картинку

            _result.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100f, 0, SpriteMeshType.FullRect);
            }

        }
    }