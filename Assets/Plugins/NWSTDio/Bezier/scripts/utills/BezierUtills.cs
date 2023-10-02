using UnityEngine;

namespace NWSTDio {
    public static class BezierUtills {

        public static Vector3 GetPoint2(Vector3 begin, Vector3 spine1, Vector3 spine2, Vector3 end, float t) {
            t = Mathf.Clamp01(t);

            Vector3 p01 = Lerp(begin, spine1, t);
            Vector3 p12 = Lerp(spine1, spine2, t);
            Vector3 p23 = Lerp(spine2, end, t);

            Vector3 p0112 = Lerp(p01, p12, t);
            Vector3 p1223 = Lerp(p12, p23, t);

            return Lerp(p0112, p1223, t);
            }

        public static Vector3 GetPoint(Vector3 start, Vector3 spine1, Vector3 spine2, Vector3 end, float t) {
            t = Mathf.Clamp01(t);

            float oneMinusT = 1f - t;

            return oneMinusT * oneMinusT * oneMinusT * start + 3f * oneMinusT * oneMinusT * t * spine1 + 3f * oneMinusT * t * t * spine2 + t * t * t * end;
            }

        public static Vector3 GetRotation(Vector3 start, Vector3 spine1, Vector3 spine2, Vector3 end, float t) {
            t = Mathf.Clamp01(t);

            float oneMinusT = 1f - t;

            return 3f * oneMinusT * oneMinusT * (spine1 - start) + 6f * oneMinusT * t * (spine2 - spine1) + 3f * t * t * (end - spine2);
            }

        public static Vector3 Lerp(Vector3 begin, Vector3 end, float t) {
            t = Mathf.Clamp01(t);

            float oneMinusT = 1f - t;

            return oneMinusT * begin + t * end;
            }

        public static Vector3 GetPoint3(Vector3 begin, Vector3 spine, Vector3 end, float t) {
            t = Mathf.Clamp01(t);

            float oneMinusT = 1f - t;

            return oneMinusT * oneMinusT * begin + 2 * oneMinusT * t * spine + t * t * end;
            }
        }
    }