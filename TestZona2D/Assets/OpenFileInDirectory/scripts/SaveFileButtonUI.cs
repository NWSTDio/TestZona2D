using UnityEngine;
using UnityEngine.UI;

namespace OpenFileInDirectory {
    [RequireComponent(typeof(Button))]
    public class SaveFileButtonUI : MonoBehaviour {

        private void Awake() {
            GetComponent<Button>().onClick.AddListener(() => {
                FileManager.Instance.SaveFile();
            });
            }

        }
    }