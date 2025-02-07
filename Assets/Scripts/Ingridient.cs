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
		}
		public string Tag;
		public string Name => Localization.Get(Tag);
		public int MaxDigestionTime, DigestionTime;
		public Sprite Sprite;

		[HideInInspector] public List<string> DangerCombinations = new List<string>();
        [HideInInspector] public List<string> HealingCombinations = new List<string>();

		public Ingredient GetRandomDangerCombination()
		{
			return Database.IngredientDictionary[DangerCombinations[UnityEngine.Random.Range(0, DangerCombinations.Count)]].Clone();
		}
        public Ingredient GetRandomHealingCombination()
        {
            return Database.IngredientDictionary[HealingCombinations[UnityEngine.Random.Range(0, HealingCombinations.Count)]].Clone();
        }
        public Ingredient Clone() => (Ingredient)this.MemberwiseClone();
	}
}