using System;
using System.IO;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

namespace Taster.DataLoaders
{
	internal static class LoaderUtils
	{
		public const string GRAPHICS_FOLDER_NAME = "gfx";
		public static void IterateInPacks(Action<string> action)
		{
			foreach(string dataPath in Directory.GetDirectories(Application.streamingAssetsPath))
				action(dataPath);
		}
		public static Sprite LoadSprite(string path)
		{
			Texture2D texture = new(1, 1, TextureFormat.RGBA32, false) {
				filterMode = FilterMode.Bilinear//,
				//alphaIsTransparency = true
			};
			texture.LoadImage(File.ReadAllBytes(path));
			ApplyAlpha(texture);
			Sprite sprite = Sprite.Create(texture, new(0,0,texture.width,texture.height),Vector2.one/2,100, 0,SpriteMeshType.FullRect,new(-0.5f,-0.5f,1,1),false);
			sprite.name = Path.GetFileName(path);
			
			return sprite;
		}
		//I'm so sorry of making this function with Chat GPT (ToT)
		public static async Task<Sprite> LoadSpriteAsync(string path)
		{
			using UnityWebRequest request = UnityWebRequestTexture.GetTexture("file://" + path);
			var operation = request.SendWebRequest();

			while(!operation.isDone)
				await Task.Yield();

			if(request.result != UnityWebRequest.Result.Success)
				return null;

			Texture2D texture = DownloadHandlerTexture.GetContent(request);
			ApplyAlpha(texture);

			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2, 100, 0, SpriteMeshType.FullRect, new Vector4(-0.5f, -0.5f, 1, 1), false);
			sprite.name = System.IO.Path.GetFileName(path);

			return sprite;
		}

		private static void ApplyAlpha(Texture2D texture)
		{
			for(int x = 0; x < texture.width; x++)
				for(int y = 0; y < texture.height; y++)
				{
					Color c = texture.GetPixel(x, y);
					c.r *= c.a; c.g *= c.a; c.b *= c.a;
					texture.SetPixel(x, y, c);
				}
			texture.Apply();
		}
	}
	
}