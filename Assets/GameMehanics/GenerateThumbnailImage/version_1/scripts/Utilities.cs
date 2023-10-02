using UnityEngine;

namespace GameMehanics.GenerateThumbnailImage.version_1 {

    public static class Utilities {

        public static Texture2D CompressImage(Texture2D texture, float size = .5f) {
            size = size.Clamp(.1f, .9f);

            // размеры основной текстуры
            int width = texture.width;
            int height = texture.height;

            // размеры уменьшеной текстуры
            int new_width = (int)Mathf.Ceil(width * size);
            int new_height = (int)Mathf.Ceil(height * size);

            var colors = new Color[new_height, new_width];// сумма цветов для пикселя
            var quantities = new int[new_height, new_width];// количество пикселей входящих в пиксель новой текстуры

            // пройдемся по пикселям основной текстуры
            for (int y = 0; y < height; y++) {
                int yy = (int)Mathf.Floor(y * size);

                for (int x = 0; x < width; x++) {
                    int xx = (int)Mathf.Floor(x * size);

                    colors[yy, xx] += texture.GetPixel(x, y);

                    quantities[yy, xx]++;
                    }
                }

            var newImage = new Texture2D(new_width, new_height, TextureFormat.ARGB32, false) {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
                };// создадим уменьшенную текстуру

            // пройдемся по уменьшенной текстуре
            for (int y = 0; y < new_height; y++) {
                for (int x = 0; x < new_width; x++)
                    newImage.SetPixel(x, y, colors[y, x] / quantities[y, x]);
                }

            newImage.Apply();

            return newImage;
            }

        public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);

        }
    }