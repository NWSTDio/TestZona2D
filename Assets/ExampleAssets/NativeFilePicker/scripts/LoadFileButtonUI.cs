using UnityEngine;
using UnityEngine.UI;

namespace ExampleAssets.NativeFilePickerAsset {
    [RequireComponent(typeof(Button))]
    public class LoadFileButtonUI : MonoBehaviour {

        private void Awake() {
            GetComponent<Button>().onClick.AddListener(() => {
                FileManager.Instance.LoadFile();
            });
            }

        }
    }