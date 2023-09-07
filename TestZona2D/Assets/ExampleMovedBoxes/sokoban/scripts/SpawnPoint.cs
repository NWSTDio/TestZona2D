using UnityEngine;

namespace ExampleMovedBoxes.Sokoban {
    public class SpawnPoint : MonoBehaviour {

        public enum TYPE { BOX, WALL } // тип точки спавна

        [SerializeField] private TYPE _type;

        public TYPE Type => _type;
        public Vector3 Position => transform.position;

        }
    }