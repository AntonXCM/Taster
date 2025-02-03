using System.Collections.Generic;
using System.IO;
using Taster.Foods;
using UnityEngine;

namespace Taster.DataLoaders
{
	public static class Database
	{
		public static Ingredient[] Ingredients;
		public static Dictionary<string, Ingredient> IngredientDictionary;
		public static List<string> Poisons;

		public static List<Ingredient> SafeIngredients;
		public static List<Ingredient> DangerIngredients;

		public static List<string[]> DangerCombinations;

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
			Poisons = poisons;

			SafeIngredients = new List<Ingredient>();
			DangerIngredients = new List<Ingredient>();
			IngredientDictionary = new Dictionary<string, Ingredient>();

			foreach (Ingredient i in Ingredients)
			{
				IngredientDictionary.Add(i.Tag, i);

				if (IsSafe(i.Tag))
					SafeIngredients.Add(i);
				else
					DangerIngredients.Add(i);
			}

			ProcessDangerCombinations(2);
		}

        static void ProcessDangerCombinations(int numberCombinations)
		{
			DangerCombinations = new List<string[]>();
			
			List<string> BlankIngredients = new List<string>();
			foreach (Ingredient i in SafeIngredients) BlankIngredients.Add(i.Tag);

			int rand1, rand2;
            string item1, item2;

            while (BlankIngredients.Count>1)
			{
				rand1 = UnityEngine.Random.Range(0, BlankIngredients.Count);
                rand2 = UnityEngine.Random.Range(0, BlankIngredients.Count);
				item1 = BlankIngredients[rand1];
                item2 = BlankIngredients[rand2];

                if (rand1 == rand2) rand2++;
				if (rand2 >= BlankIngredients.Count) rand2 = 0;

                IngredientDictionary[item1].DangerCombinations.Add(item2);
                IngredientDictionary[item2].DangerCombinations.Add(item1);
				DangerCombinations.Add(new string[2] { item1, item2 } );

				if (IngredientDictionary[item1].DangerCombinations.Count >= numberCombinations) BlankIngredients.Remove(item1);
                if (IngredientDictionary[item2].DangerCombinations.Count >= numberCombinations) BlankIngredients.Remove(item2);
            }
        }

		public static bool IsSafe(string name) => !Poisons.Contains(name);

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