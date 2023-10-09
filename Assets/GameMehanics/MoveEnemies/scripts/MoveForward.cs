using UnityEngine;

namespace GameMehanics.MoveEnemies {
	public class MoveForward : MonoBehaviour {

		[SerializeField] private Transform _eye;// направление взгляда для поиска колизий
		[SerializeField] private bool CheckWall;// проверять наличие колизий

		private readonly float _speed = 2f;
		private Rigidbody2D _body;

		public bool IsRightMove => transform.localScale.x == 1;// направление движения

		private void Awake() {
			_body = GetComponent<Rigidbody2D>();
			}

		private void Update() {
			_body.velocity = new Vector2(transform.localScale.x, 0) * _speed;

			bool isLineCast = Physics2D.Linecast(transform.position, _eye.position, 1 << LayerMask.NameToLayer("block"));// с чем колизия

			if (CheckWall && isLineCast) // если проверять колизию со стеной и она была, значит стена
				transform.localScale = new Vector3(IsRightMove ? -1 : 1, 1, 1);// меняем направление движения
			else if (CheckWall == false && isLineCast == false) // если не проверять колизию и под ногами нет поверхности, значит пропасть
				transform.localScale = new Vector3(IsRightMove ? -1 : 1, 1, 1);// меняем направление движения

			Debug.DrawLine(transform.position, _eye.position, Color.yellow);
			}

		}
	}