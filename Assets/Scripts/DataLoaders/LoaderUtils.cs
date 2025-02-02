using System;
using System.IO;

using UnityEngine;

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
			Texture2D texture = new(1,1,TextureFormat.RGBA32, false);
			texture.LoadImage(File.ReadAllBytes(path));
			
			Sprite sprite = Sprite.Create(texture, new(0,0,texture.width,texture.height),Vector2.one/2,100, 1,SpriteMeshType.Tight,new(0.5f,0.5f,1,1),false);
			sprite.name = Path.GetFileName(path);
			return sprite;
		}
	}
}