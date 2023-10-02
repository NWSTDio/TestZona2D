using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleGames.Zuma.version_1 {
	public class GameController : MonoBehaviour {

		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private Transform spawnPoint;
		[SerializeField] public Color[] colors;

		private Camera _camera;
		private Vector3 mousePos, startPos;
		private bool running;
		private int type = -1;

		private void Start() {
			_camera = Camera.main;
			startPos = new Vector3(0, -4, 0);
			running = true;
			type = Random.Range(0, colors.Length);
			spawnPoint.gameObject.GetComponent<SpriteRenderer>().color = colors[type];
			}
		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
				GameObject tmpObj = Instantiate(bulletPrefab, startPos, Quaternion.identity);
				tmpObj.GetComponent<SpriteRenderer>().color = colors[type];
				tmpObj.GetComponent<Bullet>().moveTo = mousePos - startPos;
				tmpObj.GetComponent<Bullet>().type = type;
				type = Random.Range(0, colors.Length);
				spawnPoint.gameObject.GetComponent<SpriteRenderer>().color = colors[type];
				}

			if (Input.GetKeyDown(KeyCode.R))
				SceneManager.LoadScene(0);

			if (Input.GetKeyDown(KeyCode.Q))
				Application.Quit();
			}
		private void OnDrawGizmos() {
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(startPos, mousePos);

			if (running) {
				Gizmos.color = Color.green;
				Gizmos.DrawLine(startPos, _camera.ScreenToWorldPoint(Input.mousePosition));
				}
			}

		}
	}