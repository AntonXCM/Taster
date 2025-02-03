using System;
using Taster.DataLoaders;
using System.Collections.Generic;
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
			Tag = name;
			Sprite = sprite;
			Texture = sprite.texture;
		}
		public string Tag;
		public string Name => Localization.Get(Tag);
		public int MaxDigestionTime, DigestionTime;
		public Sprite Sprite;
		public Texture2D Texture;

		public List<string> DangerCombinations = new List<string>();
        public Ingredient Clone() => (Ingredient)this.MemberwiseClone();
	}
}