using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using NWSTDio;

namespace SimpleGames.Zuma.version_2 {
	public enum TYPES { RED, GREEN, BLUE }

	public class GameController : MonoBehaviour {

		public static GameController Instance { get; private set; }

		[SerializeField] private Color[] _colors;
		[SerializeField] private Transform _ballContainer;
		[SerializeField] private Ball _ballPrefab, _tail;
		[SerializeField] private Point _start, _end;
		[SerializeField] private Transform _bazaPosition;
		[SerializeField] private GameObject _movedBallPrefab;
		[SerializeField] private float _ballSpeed = 1f;

		private List<Ball> _balls;
		private Camera _camera;
		private bool _forward = true;
		private bool _canSpawnBall;

		public bool IsCanSpawnBall => _balls.Count < 20;
		public bool IsForward => _forward;

		private void Awake() {
			Instance = this;

			_balls = new();
			}

		private void Start() {
			_balls.Add(AddBall());

			_camera = Camera.main;
			}

		private void Update() {
			if (_canSpawnBall)
				_balls.Add(AddBall(true));

			float time = Time.deltaTime;

			foreach (var ball in _balls.Where(ball => ball.IsEndOfPath == false)) {
				ball.Move(time, _ballSpeed);

				if (ball.IsDestination)
					ball.NextDestination();
				}

			if (Input.GetMouseButtonDown(0)) {
				Vector3 difference = _camera.ScreenToWorldPoint(Input.mousePosition) - _bazaPosition.position;

				float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

				Quaternion rotate = Quaternion.Euler(0f, 0f, rotateZ);

				Fire(rotate);
				}

			if (Input.GetKeyDown(KeyCode.B) && _forward)
				Back();

			Debug.DrawLine(_bazaPosition.position, _camera.ScreenToWorldPoint(Input.mousePosition));
			}

		public void SpawnBall() => _canSpawnBall = true;

		private Ball AddBall(bool spawner = false) {
			if (spawner)
				_canSpawnBall = false;

			var ball = Instantiate(_ballPrefab, _start.Position, Quaternion.identity, _ballContainer.transform);

			ball.SetDestination(_start.GetNextPoint());

			if (_balls.Count > 0) {
				ball.ChangeNextBall(_balls[^1]);

				_balls[^1].ChangePrevBall(ball);
				}

			TYPES type = (TYPES)Random.Range(0, (int)TYPES.BLUE + 1);

			ball.GetComponent<SpriteRenderer>().color = _colors[(int)type];

			_tail = ball;

			return ball;
			}
		public void RemoveBall(Ball ball) {
			if (ball.PrevBall != null)
				ball.PrevBall.ChangeNextBall(null);

			_balls.Remove(ball);

			Destroy(ball.gameObject);
			}
		public void Fire(Quaternion q) {
			Instantiate(_movedBallPrefab, _bazaPosition.position, q);
			}

		public bool IsTail(Ball ball) => _tail == ball;

		public void Back() {
			foreach (var ball in _balls.Where(ball => !ball.IsEndOfPath))
				ball.PrevDestination();

			_balls.Reverse();

			_forward = false;
			}
		}
	}