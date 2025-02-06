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

        float chanceForPoisonProduct, chanceForPoisonCombination, chanceForHealingCombination, chanceForPoisonAndHealingProduct, chanceForDobulePoisonProduct;

        private void Awake() => ServiceLocator.Register(this);

        private void Start()
		{
			foodHolder = ServiceLocator.Get<FoodHolder>();

            foodHolder.FoodArray = new Food[foodHolder.StandsCount];

            chanceForPoisonProduct = LevelSelector.currentLevel.ChanceForPoisonProduct;
            chanceForPoisonCombination = chanceForPoisonProduct + LevelSelector.currentLevel.ChanceForPoisonCombination;
            chanceForHealingCombination = chanceForPoisonCombination + LevelSelector.currentLevel.ChanceForHealingCombination;
            chanceForPoisonAndHealingProduct = chanceForHealingCombination + LevelSelector.currentLevel.ChanceForPoisonAndHealingProduct;
            chanceForDobulePoisonProduct = chanceForPoisonAndHealingProduct + LevelSelector.currentLevel.ChanceForDobulePoisonProduct;

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
                int randID = UnityEngine.Random.Range(0, safeList.Count);
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
            int otherProduct = mainProductID == 0 ? 2 : 0;
            Ingredient mainProduct = compound[mainProductID];

            switch (randomChance)
            {
                case float i when (i < chanceForPoisonProduct):
                    Debug.Log("Poison");
                    // Добавление ядовитого продукта
                    compound[Random.Range(0, 3)] = Database.GetRandomPoison();
                    break;

                case float i when (i < chanceForPoisonCombination && mainProduct.DangerCombinations.Count > 0):
                    Debug.Log("Poison Combo");
                    // Добавление опасной комбинации
                    compound[mainProductID + 1] = mainProduct.GetRandomDangerCombination();
                    break;

                case float i when (i < chanceForHealingCombination && mainProduct.HealingCombinations.Count > 0):
                    Debug.Log("Healing");
                    // Добавление лечебной комбинации
                    compound[mainProductID + 1] = mainProduct.GetRandomHealingCombination();
                    break;

                case float i when (i < chanceForPoisonAndHealingProduct && mainProduct.HealingCombinations.Count > 0):
                    Debug.Log("Healing and Poison");
                    // Добавление лечебной комбинации с ядовитым продуктом
                    compound[mainProductID + 1] = mainProduct.GetRandomHealingCombination();
                    compound[otherProduct] = Database.GetRandomPoison();
                    break;

                case float i when (i < chanceForDobulePoisonProduct && mainProduct.DangerCombinations.Count > 0):
                    Debug.Log("Dobule Poison");
                    // Добавление опасной комбинации с ядовитым продуктом
                    compound[mainProductID + 1] = mainProduct.GetRandomDangerCombination();
                    compound[otherProduct] = Database.GetRandomPoison();
                    break;

                default:
                    Debug.Log("Normal");
                    break;
            }

            foodHolder.FoodArray[id] = Food.FromIngridients(compound, foodHolder.FoodStands[id]);
        }
	}
}