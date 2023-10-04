using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleWorkEditor.TestAnimations.version_2 {
	public class Controller : MonoBehaviour {

		[SerializeField] private Explosion _explosionPrefab;// взрыв 

		private readonly List<Explosion> _explosions = new();// обьекты взрывов
		private Rect _bounds;

		private void Start() {
			_bounds = new Rect() {
				xMin = -8f,
				xMax = 8f,
				yMin = -4f,
				yMax = 4f
				};

			StartCoroutine(MakeExplosions());
			}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space))
				MakeExplosion();
			}

		private IEnumerator MakeExplosions() {
			var wfs = new WaitForSeconds(1f);

			while (true) {
				for (int i = 0, max = Random.Range(1, 4); i < max; i++)
					MakeExplosion();

				yield return wfs;
				}
			}

		private void MakeExplosion() {
			GetExplosion().Show(new Vector3(Random.Range(_bounds.xMin, _bounds.xMax), Random.Range(_bounds.yMin, _bounds.yMax), 0));
			}

		private Explosion GetExplosion() {
			foreach (var e in _explosions) {
				if (e.gameObject.activeSelf == false)
					return e;
				}

			Explosion explosion = Instantiate(_explosionPrefab, Vector3.zero, Quaternion.identity);

			_explosions.Add(explosion);

			return explosion;
			}

		}
	}