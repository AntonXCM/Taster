using System;

using UnityEngine;

namespace Taster.Foods
{
	[Serializable]
	public class Ingredient
	{
		public Ingredient(int maxDigestionTime, string name, Sprite sprite)
		{
			DigestionTime = maxDigestionTime;
			MaxDigestionTime = maxDigestionTime;
			Name = name;
			Sprite = sprite;
			Texture = sprite.texture;
		}
		public string Name;
		public int MaxDigestionTime, DigestionTime;
		public Sprite Sprite;
		public Texture2D Texture;
		public Ingredient Clone() => (Ingredient)this.MemberwiseClone();
	}
}