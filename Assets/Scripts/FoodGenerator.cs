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

        float chanceForPoisonProduct, chanceForPoisonCombination, chanceForHealingCombination, chanceForPoisonAndHealingProduct;

        private void Awake() => ServiceLocator.Register(this);

        private void Start()
		{
			foodHolder = ServiceLocator.Get<FoodHolder>();

            foodHolder.FoodArray = new Food[foodHolder.StandsCount];

            chanceForPoisonProduct = LevelSelector.currentLevel.ChanceForPoisonProduct;
            chanceForPoisonCombination = chanceForPoisonProduct + LevelSelector.currentLevel.ChanceForPoisonCombination;
            chanceForHealingCombination = chanceForPoisonCombination + LevelSelector.currentLevel.ChanceForHealingCombination;
            chanceForPoisonAndHealingProduct = chanceForHealingCombination + LevelSelector.currentLevel.ChanceForPoisonAndHealingProduct;

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

            if (randomChance < chanceForPoisonProduct)
            {
                Debug.Log("Poison");
                // Добавление ядовитого продукта
                compound[Random.Range(0, 3)] = Database.DangerIngredients[Random.Range(0, Database.DangerIngredients.Count)].Clone();
            } 
            else if (randomChance < chanceForPoisonCombination && mainProduct.DangerCombinations.Count>0)
            {
                Debug.Log("Poison Combo");
                // Добавление опасной комбинации
                string newProduct = mainProduct.DangerCombinations[Random.Range(0, mainProduct.DangerCombinations.Count)];

                compound[mainProductID + 1] = Database.IngredientDictionary[newProduct].Clone();
            }
            else if (randomChance < chanceForHealingCombination && mainProduct.HealingCombinations.Count>0)
            {
                Debug.Log("Healing");
                // Добавление лечебной комбинации
                string newProduct = mainProduct.HealingCombinations[Random.Range(0, mainProduct.HealingCombinations.Count)];

                compound[mainProductID + 1] = Database.IngredientDictionary[newProduct].Clone();
            }
            else if (randomChance < chanceForPoisonAndHealingProduct && mainProduct.HealingCombinations.Count > 0)
            {
                Debug.Log("Healing and Poison");
                // Добавление лечебной комбинации с ядовитым продуктом
                string newProduct = mainProduct.HealingCombinations[Random.Range(0, mainProduct.HealingCombinations.Count)];

                compound[mainProductID + 1] = Database.IngredientDictionary[newProduct].Clone();

                int otherProduct = 0;
                if (mainProductID == 0) otherProduct = 2;
                compound[otherProduct] = Database.DangerIngredients[Random.Range(0, Database.DangerIngredients.Count)].Clone();
            } 
            else
            {
                Debug.Log("Normal");
            }

            foodHolder.FoodArray[id] = Food.FromIngridients(compound, foodHolder.FoodStands[id]);
        }
	}
}