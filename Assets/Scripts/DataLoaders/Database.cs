﻿using System.Collections.Generic;
using System.IO;
using Taster.Foods;
using UnityEngine;

namespace Taster.DataLoaders
{
    [CreateAssetMenu(fileName = "Database", menuName = "Scriptable Objects/Database")]
    public class Database: ScriptableObject
    {
        [SerializeField] Ingredient[] LoadIngredients;
        [SerializeField] List<string> LoadPoisons;

        public static Ingredient[] Ingredients;
		public static List<string> Poisons;

        public static Dictionary<string, Ingredient> IngredientDictionary;

        public static List<Ingredient> SafeIngredients;
		public static List<Ingredient> DangerIngredients;

		public static List<string[]> DangerCombinations;
		public static List<string[]> HealingCombinations;
        public static List<string> AllergyIngredients;


        [RuntimeInitializeOnLoadMethod]
        private static void OnGameStart()
        {
            Database loadBase = Resources.Load<Database>("Database");

			Ingredients = loadBase.LoadIngredients;
            Poisons = loadBase.LoadPoisons;

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

			if (DataSaver.IsSaveExists("DangerCombinations"))
			{
				DataSaver.Open<List<string[]>>("DangerCombinations", out DangerCombinations);
                DataSaver.Open<List<string[]>>("HealingCombinations", out HealingCombinations);
                DataSaver.Open<List<string>>("AllergyIngredients", out AllergyIngredients);

				LoadDangerCombinations();
				LoadHealingCombination();
            } 
			else
			{
                ProcessDangerCombinations(2);
                ProcessHealingCombinations(1);
				ProcessAllergy(3);

                DataSaver.Save(DangerCombinations, "DangerCombinations");
                DataSaver.Save(HealingCombinations, "HealingCombinations");
                DataSaver.Save(AllergyIngredients, "AllergyIngredients");
            }
        }

        static void ProcessDangerCombinations(int numberCombinations)
		{
			DangerCombinations = new List<string[]>();
			
			// Создаем список всех предметов, с которыми будем делать комбинации
			List<string> BlankIngredients = new List<string>();
			foreach (Ingredient i in SafeIngredients) BlankIngredients.Add(i.Tag);

			int rand1, rand2;
            string item1, item2;

            while (BlankIngredients.Count>1)
			{
				// Подбираем два случайных id предметов из списка
				rand1 = UnityEngine.Random.Range(0, BlankIngredients.Count);
                rand2 = UnityEngine.Random.Range(0, BlankIngredients.Count);

				// Следим чтобы предметы не были одинаковыми
                if (rand1 == rand2) rand2++;
				if (rand2 >= BlankIngredients.Count) rand2 = 0;

                item1 = BlankIngredients[rand1];
                item2 = BlankIngredients[rand2];

				// Обновляем данные этих предметов, записывая комбинации в каждый из них
                IngredientDictionary[item1].DangerCombinations.Add(item2);
                IngredientDictionary[item2].DangerCombinations.Add(item1);
				DangerCombinations.Add(new string[2] { item1, item2 } );

				// Если у предмета уже достаточно комбинаций - удаляем его из списка
				if (IngredientDictionary[item1].DangerCombinations.Count >= numberCombinations) BlankIngredients.Remove(item1);
                if (IngredientDictionary[item2].DangerCombinations.Count >= numberCombinations) BlankIngredients.Remove(item2);
            }
        }

        static void LoadDangerCombinations()
        {
            string item1, item2;

            foreach (var i in DangerCombinations)
            {
                item1 = i[0];
                item2 = i[1];

                // Обновляем данные этих предметов, записывая комбинации в каждый из них
                IngredientDictionary[item1].DangerCombinations.Add(item2);
                IngredientDictionary[item2].DangerCombinations.Add(item1);
            }
        }
        static void ProcessHealingCombinations(int numberCombinations)
        {
            HealingCombinations = new List<string[]>();

            // Создаем список всех предметов, с которыми будем делать комбинации
            List<string> BlankIngredients = new List<string>();
            foreach (Ingredient i in SafeIngredients) BlankIngredients.Add(i.Tag);

            int rand1, rand2;
            string item1, item2;

            while (BlankIngredients.Count > 1)
            {
				// Берем один случайный предмет из списка
                rand1 = UnityEngine.Random.Range(0, BlankIngredients.Count);
                item1 = BlankIngredients[rand1];

				// Создаем список доступных комбинаций для этого предмета, следим чтобы не было пересечений с опасными комбинациями
                List<string> AvailableCombinations = new List<string>();
				foreach (string i in BlankIngredients) AvailableCombinations.Add(i);
                AvailableCombinations.Remove(item1);
				foreach (string i in IngredientDictionary[item1].DangerCombinations)
				{
					if (AvailableCombinations.Contains(i)) AvailableCombinations.Remove(i);
				}

				if (AvailableCombinations.Count == 0)
				{
					// Если среди списка нету доступных комбинаций для этого предмета - удаляем его из списка, с ним уже ничего не выйдет
                    BlankIngredients.Remove(item1);
                } 
				else 
				{
					// Если доступные комбинации есть - записываем случайную комбинацию

					rand2 = UnityEngine.Random.Range(0, AvailableCombinations.Count);
					item2 = AvailableCombinations[rand2];

                    // Обновляем данные этих предметов, записывая комбинации в каждый из них
                    IngredientDictionary[item1].HealingCombinations.Add(item2);
					IngredientDictionary[item2].HealingCombinations.Add(item1);
					HealingCombinations.Add(new string[2] { item1, item2 });

                    // Если у предмета уже достаточно комбинаций - удаляем его из списка
                    if (IngredientDictionary[item1].HealingCombinations.Count >= numberCombinations) BlankIngredients.Remove(item1);
					if (IngredientDictionary[item2].HealingCombinations.Count >= numberCombinations) BlankIngredients.Remove(item2);
				}
            }
        }

        static void LoadHealingCombination()
        {
            string item1, item2;

            foreach (var i in HealingCombinations)
            {
                item1 = i[0];
                item2 = i[1];

                // Обновляем данные этих предметов, записывая комбинации в каждый из них
                IngredientDictionary[item1].HealingCombinations.Add(item2);
                IngredientDictionary[item2].HealingCombinations.Add(item1);
            }
        }
        static void ProcessAllergy(int number)
		{
            AllergyIngredients = new List<string>();

            List<string> BlankIngredients = new List<string>();
			foreach (Ingredient i in SafeIngredients) BlankIngredients.Add(i.Tag);

			int randID;
			for (int i = 0; i < number; i++)
			{
				randID = Random.Range(0, BlankIngredients.Count);

				AllergyIngredients.Add(BlankIngredients[randID]);
                BlankIngredients.RemoveAt(randID);
            }
		}

        public static Ingredient GetRandomPoison()
        {
            return DangerIngredients[UnityEngine.Random.Range(0, DangerIngredients.Count)].Clone();
        }
        public static bool IsSafe(string name) => !Poisons.Contains(name);
	}
}