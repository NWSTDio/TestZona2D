#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class AutoSave : MonoBehaviour {
	private readonly static int DELAY_SECONDS = 300;

	static AutoSave() {
		var nextTime = EditorApplication.timeSinceStartup + DELAY_SECONDS;

		EditorApplication.update += () => {
			if (EditorApplication.isPlaying == false && nextTime < EditorApplication.timeSinceStartup) {
				Debug.Log("AUTO SAVING ALL OPEN SCENES...");

				EditorSceneManager.SaveOpenScenes();

				AssetDatabase.SaveAssets();

				nextTime = EditorApplication.timeSinceStartup + DELAY_SECONDS;
				}
		};
		}

	}
#endif