using System.Collections.Generic;
using UnityEngine;

namespace OtherExamples.BezierCurve {
    public class Bezier : MonoBehaviour {

        public List<Vector2> ControlPoints = new List<Vector2>()    {
        new (0, 0),
        new (35, 15),
        new (47, 13),
        new (45, 5),
        new (48, 0),
        new (25, -5),
        new (15, -18),
        new (36, -20),
        new (64, -20),
        new (85, -18),
        new (75, -5),
        new (52, 0),
        new (55, 5),
        new (53, 13),
        new (65, 15),
        new (100, 0)
    };
        public GameObject PointPrefab;

        LineRenderer[] mLineRenderers;
        List<GameObject> mPointGameObjects = new List<GameObject>();

        public float LineWidth;
        public float LineWidthBezier;
        public Color LineColour = new Color(0.5f, 0.5f, 0.5f, 0.8f);
        public Color BezierCurveColour = new Color(0.5f, 0.6f, 0.8f, 0.8f);
        public Color BezierCurveColour2 = new Color(0.8f, 0.6f, 0.5f, 0.8f);


        private LineRenderer CreateLine() {
            GameObject obj = new GameObject();
            LineRenderer lr = obj.AddComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = LineColour;
            lr.endColor = LineColour;
            lr.startWidth = LineWidth;
            lr.endWidth = LineWidth;
            return lr;
            }

        void Start() {
            mLineRenderers = new LineRenderer[3];
            mLineRenderers[0] = CreateLine();
            mLineRenderers[1] = CreateLine();
            mLineRenderers[2] = CreateLine();

            mLineRenderers[0].gameObject.name = "LineRenderer_obj_0";
            mLineRenderers[1].gameObject.name = "LineRenderer_obj_1";

            // Create the instances of PointPrefab
            // to show the control points.
            for (int i = 0; i < ControlPoints.Count; ++i) {
                GameObject obj = Instantiate(PointPrefab, ControlPoints[i], Quaternion.identity);
                obj.name = "ControlPoint_" + i.ToString();
                mPointGameObjects.Add(obj);
                }
            }

        // Update is called once per frame
        void Update() {
            LineRenderer lineRenderer = mLineRenderers[0];
            LineRenderer curveRenderer = mLineRenderers[1];
            LineRenderer curveRenderer_Reflection = mLineRenderers[2];

            List<Vector2> pts = new List<Vector2>();

            for (int k = 0; k < mPointGameObjects.Count; ++k) {
                pts.Add(mPointGameObjects[k].transform.position);
                }

            // create a line renderer for showing the straight lines between control points.
            lineRenderer.positionCount = pts.Count;
            for (int i = 0; i < pts.Count; ++i) {
                lineRenderer.SetPosition(i, pts[i]);
                }

            // we take the control points from the list of points in the scene.
            // recalculate points every frame.
            List<Vector2> curve = BezierCurve.PointList2(pts, 0.01f);
            curveRenderer.startColor = BezierCurveColour;
            curveRenderer.endColor = BezierCurveColour;
            curveRenderer.positionCount = curve.Count;
            curveRenderer.startWidth = LineWidthBezier;
            curveRenderer.endWidth = LineWidthBezier;

            curveRenderer_Reflection.startColor = BezierCurveColour2;
            curveRenderer_Reflection.endColor = BezierCurveColour2;
            curveRenderer_Reflection.positionCount = curve.Count;
            curveRenderer_Reflection.startWidth = LineWidthBezier;
            curveRenderer_Reflection.endWidth = LineWidthBezier;
            for (int i = 0; i < curve.Count; ++i) {
                curveRenderer.SetPosition(i, curve[i]);
                curveRenderer_Reflection.SetPosition(i, new Vector3(curve[i].x, -curve[i].y, 0.0f));
                }
            }

        void OnGUI() {
            Event e = Event.current;
            if (e.isMouse) {
                if (e.clickCount == 2 && e.button == 0) {

                    Vector2 rayPos = new Vector2(
                        Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                        Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

                    InsertNewControlPoint(rayPos);
                    }
                }
            }

        void InsertNewControlPoint(Vector2 p) {
            if (mPointGameObjects.Count >= 16) {
                Debug.Log("Cannot create any new control points. Max number is 16");
                return;
                }
            GameObject obj = Instantiate(PointPrefab, p, Quaternion.identity);
            obj.name = "ControlPoint_" + mPointGameObjects.Count.ToString();
            mPointGameObjects.Add(obj);
            }
        }
    }