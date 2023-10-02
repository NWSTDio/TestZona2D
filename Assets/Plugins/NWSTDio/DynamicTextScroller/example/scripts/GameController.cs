using UnityEngine;

namespace NWSTDio.DynamicTextScrollerExample {
	public class GameController : MonoBehaviour {

		[SerializeField] TextResizer information;

		private void Start() {
			TextAsset data = Resources.Load("texts/info") as TextAsset;

			information.SetText(data.text);
			}

		}
	}