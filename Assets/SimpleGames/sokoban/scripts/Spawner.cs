using System.Collections.Generic;
using UnityEngine;

namespace SimpleGames.Sokoban {
    public class Spawner : MonoBehaviour {

        public static Spawner Instance { get; private set; }

        [SerializeField] private Box _boxPrefab;// префаб ящика
        [SerializeField] private Wall _wallPrefab;// префаб преграды
        [SerializeField] private Transform _worldContainer;// контейнер обьектов

        public List<Box> Boxes { get; private set; } // все ящики
        public List<Wall> Walls { get; private set; } // все преграды

        private void Awake() {
            Instance = this;

            Boxes = new List<Box>();
            Walls = new List<Wall>();
            }

        private void Start() {
            foreach (Transform child in transform) { // пройдемся по всем точкам спауна
                if (child.TryGetComponent(out SpawnPoint point)) { // убедимся что это точка спауна
                    if (point.Type == SpawnPoint.TYPE.BOX) // если тип точки ящик
                        Boxes.Add(Instantiate(_boxPrefab, point.Position, Quaternion.identity, _worldContainer));
                    else
                        Walls.Add(Instantiate(_wallPrefab, point.Position, Quaternion.identity, _worldContainer));
                    }
                }
            }

        }
    }