using UnityEngine;
using UnityEngine.UI;

namespace ExampleAssets.NativeFilePickerAsset {
    [RequireComponent(typeof(Button))]
    public class SaveFileButtonUI : MonoBehaviour {

        private void Awake() {
            GetComponent<Button>().onClick.AddListener(() => {
                FileManager.Instance.SaveFile();
            });
            }

        }
    }