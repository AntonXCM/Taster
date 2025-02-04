using System.Collections;
using System.Collections.Generic;

using Taster.DataLoaders;
using Taster.Foods;
using UnityEngine;
namespace Taster.Gameplay
{
	public class FoodGenerator : MonoBehaviour
	{
		FoodHolder foodHolder;

        [Range(0, 1)] public float ChanceForPoisonProduct;
        [Range(0, 1)] public float ChanceForPoisonCombination;
        [Range(0, 1)] public float ChanceForHealingCombination;

        private void Awake() => ServiceLocator.Register(this);

        private void Start()
		{
			foodHolder = ServiceLocator.Get<FoodHolder>();

            foodHolder.FoodArray = new Food[foodHolder.StandsCount];

            StartCoroutine(Generation());
        }

        IEnumerator Generation()
        {
            for (int i = 0; i < foodHolder.StandsCount; i++)
            {
                GenerateRandomDish(i);
                yield return null;
            }
        }

        public void GenerateRandomDish(int id)
		{
            if (foodHolder.FoodArray[id] != null) Destroy(foodHolder.FoodArray[id].gameObject);

            Ingredient[] compound = new Ingredient[3];

            // Генерация состава из безопасных ингредиентов без комбинаций
            List<Ingredient> safeList = new List<Ingredient>();
            foreach (Ingredient i in Database.SafeIngredients) safeList.Add(i);

            for (int i = 0; i < 3; i++)
            {
                int randID = Random.Range(0, safeList.Count);
                Ingredient randProduct = safeList[randID];
                compound[i] = randProduct.Clone();

                foreach (var product in randProduct.DangerCombinations)
                    if (safeList.Contains(Database.IngredientDictionary[product]))
                        safeList.Remove(Database.IngredientDictionary[product]);

                foreach (var product in randProduct.HealingCombinations)
                    if (safeList.Contains(Database.IngredientDictionary[product]))
                        safeList.Remove(Database.IngredientDictionary[product]);

                safeList.Remove(randProduct);
            }

            float randomChance = Random.Range(0f, 1f);
            int mainProductID = Random.Range(0, 2);
            Ingredient mainProduct = compound[mainProductID];

            if (randomChance < ChanceForPoisonProduct)
            {
                // Добавление ядовитого продукта
                compound[Random.Range(0, 3)] = Database.DangerIngredients[Random.Range(0, Database.DangerIngredients.Count)].Clone();
            } 
            else if (randomChance < ChanceForPoisonProduct + ChanceForPoisonCombination && mainProduct.DangerCombinations.Count>0)
            {
                // Добавление опасной комбинации
                string newProduct = mainProduct.DangerCombinations[Random.Range(0, mainProduct.DangerCombinations.Count)];

                compound[mainProductID + 1] = Database.IngredientDictionary[newProduct].Clone();
            }
            else if (randomChance < ChanceForPoisonProduct + ChanceForPoisonCombination + ChanceForHealingCombination && mainProduct.HealingCombinations.Count>0)
            {
                // Добавление лечебной комбинации
                string newProduct = mainProduct.HealingCombinations[Random.Range(0, mainProduct.HealingCombinations.Count)];

                compound[mainProductID + 1] = Database.IngredientDictionary[newProduct].Clone();
            }

            foodHolder.FoodArray[id] = Food.FromIngridients(compound, foodHolder.FoodStands[id]);
        }
	}
}