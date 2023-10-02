using UnityEngine;

namespace NWSTDio.PathGeneratorExample {
	public class MoveBall : MonoBehaviour {

		private readonly float _speed = 2f;
		private GameController _controller;
		private Point _destination;
		private Transform _finish;

		private void Start() {
			_controller = GameController.Instance;

			transform.position = _controller.StartPosition.transform.position;

			_destination = _controller.StartPosition.GetNextPoint();

			_finish = _controller.EndPosition.transform;
			}

		private void Update() {
			if (_controller.IsRunning == false)
				return;

			if (_destination == null)
				return;

			if (transform.position == _finish.position)
				Debug.Log("GAME OVER");

			transform.position = Vector3.MoveTowards(transform.position, _destination.Position, _speed * Time.deltaTime);

			if (transform.position == _destination.Position)
				_destination = _destination.GetNextPoint();
			}
		}
	}