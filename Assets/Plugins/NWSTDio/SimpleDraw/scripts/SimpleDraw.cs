using UnityEngine;

namespace NWSTDio {
    public class SimpleDraw : MonoBehaviour {

        [SerializeField] private Display _display;

        public Display Display => _display;

        public void DrawLine(float x0, float y0, float x1, float y1, float th) {

            float dx = Mathf.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            float dy = Mathf.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;

            float err;

            float e2 = Mathf.Sqrt(dx * dx + dy * dy);

            if (th <= 1 || e2 == 0) {
                PlotLineAA(x0, y0, x1, y1);

                return;
                }

            dx *= 255 / e2;
            dy *= 255 / e2;

            th = 255 * (th - 1);// scale values

            if (dx < dy) {
                // steep line
                x1 = Mathf.Round((e2 + th / 2) / dy);// start offset

                err = x1 * dy - th / 2;// shift error value to offset width

                for (x0 -= x1 * sx; ; y0 += sy) {
                    SetPixel(x1 = x0, y0, err);// aliasing pre-pixel

                    for (e2 = dy - err - th; e2 + dy < 255; e2 += dy)
                        SetPixel(x1 += sx, y0);// pixel on the line

                    SetPixel(x1 + sx, y0, e2);// aliasing post-pixel

                    if (y0 == y1)
                        break;

                    err += dx;// y-step

                    if (err > 255) { // x-step
                        err -= dy;
                        x0 += sx;
                        }
                    }
                }
            else {
                // flat line
                y1 = Mathf.Round((e2 + th / 2) / dx);// start offset

                err = y1 * dx - th / 2;// shift error value to offset width

                for (y0 -= y1 * sy; ; x0 += sx) {
                    SetPixel(x0, y1 = y0, err);// aliasing pre-pixel

                    for (e2 = dx - err - th; e2 + dx < 255; e2 += dx)
                        SetPixel(x0, y1 += sy);// pixel on the line

                    SetPixel(x0, y1 + sy, e2);// aliasing post-pixel

                    if (x0 == x1)
                        break;

                    err += dy;// x-step

                    if (err > 255) { // y-step
                        err -= dx;
                        y0 += sy;
                        }
                    }
                }

            }
        private void PlotLineAA(float x0, float y0, float x1, float y1) {
            // draw a black (0) anti-aliased line on white (255) background
            float dx = Mathf.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            float dy = Mathf.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;

            float err = dx - dy, e2, x2;// error value e_xy

            var ed = dx + dy == 0 ? 1 : Mathf.Sqrt(dx * dx + dy * dy);

            for (; ; ) { // pixel loop
                SetPixel(x0, y0, 255 * Mathf.Abs(err - dx + dy) / ed);

                e2 = err;
                x2 = x0;

                if (2 * e2 >= -dx) {
                    // x step
                    if (x0 == x1)
                        break;

                    if (e2 + dy < ed)
                        SetPixel(x0, y0 + sy, 255 * (e2 + dy) / ed);

                    err -= dy;
                    x0 += sx;
                    }
                if (2 * e2 <= dy) {
                    // y step
                    if (y0 == y1)
                        break;

                    if (dx - e2 < ed)
                        SetPixel(x2 + sx, y0, 255 * (dx - e2) / ed);

                    err += dx;

                    y0 += sy;
                    }
                }
            }
        private void SetPixel(float x, float y) {
            _display.EnablePixel((int)x, (int)y);
            }

        private void SetPixel(float x, float y, float alpha) {
            alpha = 1 - alpha / 255;

            if (alpha > .5f)
                _display.EnablePixel((int)x, (int)y);
            }

        }
    }