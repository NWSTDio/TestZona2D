using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NWSTDio.PathGeneratorExample {
	public class GameController : MonoBehaviour {

		public static GameController Instance;

		[SerializeField] private Point _startPosition, _endPosition;

		private string _scene;
		private bool _running = false;

		public Point EndPosition => _endPosition;
		public Point StartPosition => _startPosition;
		public bool IsRunning => _running;

		private void Awake() {
			Instance = this;

			_scene = SceneManager.GetActiveScene().name;
			}

		private void Start() {
			StartCoroutine(Running());
			}

		private void Update() {
			if (Input.GetKeyUp(KeyCode.Space))
				SceneManager.LoadScene(_scene);
			}

		private IEnumerator Running() {
			Debug.Log("Wait For 5 seconds!");

			yield return new WaitForSeconds(5f);

			_running = true;
			}
		}
	}