using UnityEngine;
using UnityEngine.Tilemaps;

namespace ExampleWorkEditor.TileMapDrawing {
    public class Main : MonoBehaviour {

        [SerializeField] private Tile[] _tiles;// тайлы созданые в палитре тайлов
        [SerializeField] private Tilemap _render;// карта тайлов, где можно рисовать

        private readonly int _width = 20, _height = 10;// границы карты

        private void Start() => Render();
        private void Update() {
            if (Input.GetKey(KeyCode.Space))
                Render();
            }

        private void Render() { // рисуем тайлами
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _width; x++) {
                    Tile tile = _tiles[Random.Range(0, _tiles.Length)];

                    _render.SetTile(new Vector3Int(x - _width / 2, y - _height / 2, 0), tile);
                    }
            }

        private void Clear() { // очистка тайлов
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _width; x++)
                    _render.SetTile(new Vector3Int(x - _width / 2, y - _height / 2, 0), null);// убрать тайл

            }

        }
    }