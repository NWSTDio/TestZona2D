using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public static class Extensions {

        public static float Round(this float value) => Mathf.Round(value * 100) * .01f;

        }
    }