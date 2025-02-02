using System;
using System.Collections.Generic;
using System.IO;

using Taster.Foods;

using UnityEngine;
namespace Taster.DataLoaders
{
	public static class Database
	{
		public static Ingredient[] Ingredients = new Ingredient[0];
		public static string[] Poisons = new string[0];
		static Database()
		{
			List<Ingredient> ingredients = new();
			List<string> poisons = new();

			LoaderUtils.IterateInPacks(dataPath =>
			{
				ParseIngredients(ingredients, dataPath);
				ParsePoisons(poisons, dataPath);
			});

			Ingredients = ingredients.ToArray();
			Poisons = poisons.ToArray();
		}
		private static void ParseIngredients(List<Ingredient> ingredients, string dataPath)
		{
			foreach(string line in File.ReadLines(Path.Combine(dataPath, "data", "ingredients.csv")))
			{
				string[] parts = line.Split(';');
				string name = parts[0].Trim();
				ingredients.Add(new(
					int.Parse(parts[1].Trim()), //Время переваривания
					name, //Имя
					LoaderUtils.LoadSprite(Path.Combine(dataPath, LoaderUtils.GRAPHICS_FOLDER_NAME, "ingredients", name + ".png"))));//Спрайт
			}
		}
		private static void ParsePoisons(List<string> poisons, string dataPath)
		{
			poisons.AddRange(File.ReadAllText(Path.Combine(dataPath, "data", "poisons.txt")).Split(','));
		}
	}
}