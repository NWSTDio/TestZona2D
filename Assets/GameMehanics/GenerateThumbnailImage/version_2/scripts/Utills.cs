using System.IO;
using UnityEngine;

namespace GameMehanics.GenerateThumbnailImage.version_2 {
	public static class Utills {

		public static Texture2D LoadTexture2D(string path) {
			if (File.Exists(path) == false)
				return null;

			byte[] data = File.ReadAllBytes(path);

			var result = new Texture2D(1, 1);

			result.LoadImage(data);

			return result;
			}

		public static Texture2D Compress(Texture2D texture, float scale) {
			scale = Mathf.Clamp(scale, .005f, .995f);

			int width = (int)(texture.width * scale);
			int height = (int)(texture.height * scale);

			var result = new Texture2D(width, height, texture.format, true) {
				wrapMode = TextureWrapMode.Clamp,
				filterMode = FilterMode.Point,
				};

			Color32[] pixels = result.GetPixels32(0);

			float x = 1f / texture.width * ((float)texture.width / width);
			float y = 1f / texture.height * ((float)texture.height / height);

			for (int i = 0; i < pixels.Length; i++)
				pixels[i] = texture.GetPixelBilinear(x * ((float)i % width), y * ((float)Mathf.Floor(i / width)));

			result.SetPixels32(pixels, 0);
			result.Apply();

			return result;
			}

		}
	}