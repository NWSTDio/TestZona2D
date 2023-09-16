using UnityEngine;
using UnityEngine.UI;

namespace OpenFileInDirectory {
    [RequireComponent(typeof(Button))]
    public class LoadFileButtonUI : MonoBehaviour {

        private void Awake() {
            GetComponent<Button>().onClick.AddListener(() => {
                FileManager.Instance.LoadFile();
            });
            }

        }
    }