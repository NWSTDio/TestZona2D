using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NWSTDio.DisplayExample {
    public class Main : MonoBehaviour {

        [SerializeField] private Display _display;

        private List<Players> _players;
        private string _scene;

        private void Awake() {
            _scene = SceneManager.GetActiveScene().name;
            }

        private void Start() {
            _players = new List<Players>();

            _display.ChangeDisplay(10, 10);

            AddPlayer();

            StartCoroutine(Step());
            }

        private void Update() {
            if (Input.GetKeyUp(KeyCode.Space))
                SceneManager.LoadScene(_scene);

            if (Input.GetKeyUp(KeyCode.Q))
                AddPlayer();
            }

        private IEnumerator Step() {
            while (true) {
                for (int i = 0, max_i = _players.Count; i < max_i; i++) {
                    Players p = _players[i];

                    int tx = p.x + p.dirX;
                    int ty = p.y + p.dirY;

                    if (tx >= _display.Width || tx < 0) {
                        p.dirX *= -1;

                        tx = p.x + p.dirX;
                        }

                    if (ty >= _display.Height || ty < 0) {
                        p.dirY *= -1;

                        ty = p.y + p.dirY;
                        }

                    p.x = tx;
                    p.y = ty;

                    _display.EnablePixel(p.x, p.y);
                    }

                _display.Render();

                foreach (Players p in _players)
                    _display.DisablePixel(p.x, p.y);

                yield return new WaitForSeconds(.1f);
                }
            }
        private void AddPlayer() {
            var p = new Players {
                x = Random.Range(0, _display.Width),
                y = Random.Range(0, _display.Height),

                dirX = Random.Range(0, 2) == 0 ? -1 : 1,
                dirY = Random.Range(0, 2) == 0 ? -1 : 1
                };

            _players.Add(p);
            }

        }

    public class Players {

        public int x, y;
        public int dirX, dirY;

        }
    }