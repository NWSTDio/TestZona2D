using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public class Ladder : MonoBehaviour { // лестница/стена

        [SerializeField] private Transform _box;// верхняя поверхность
        [SerializeField] private bool _freezeXPosition;// заморозить ли управление по оси X

        public bool FreezeXPosition => _freezeXPosition;

        public void Show() => _box.gameObject.SetActive(true);
        public void Hide() => _box.gameObject.SetActive(false);

        }
    }