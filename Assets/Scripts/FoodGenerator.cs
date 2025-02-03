using System.Collections.Generic;

using Taster.DataLoaders;
using Taster.Foods;
using UnityEngine;
namespace Taster.Gameplay
{
	public class FoodGenerator : MonoBehaviour
	{
		FoodHolder foodHolder;

        private void Awake() => ServiceLocator.Register(this);

        private void Start()
		{
			foodHolder = ServiceLocator.Get<FoodHolder>();

            foodHolder.FoodArray = new Food[foodHolder.StandsCount];

            for (int i = 0; i < foodHolder.StandsCount; i++) GenerateRandomDish(i);
        }

		public void GenerateRandomDish(int id)
		{
            if (foodHolder.FoodArray[id] != null) Destroy(foodHolder.FoodArray[id].gameObject);

            foodHolder.FoodArray[id] = Food.FromIngridients(GetRandomIngridients(), foodHolder.FoodStands[id]);

            IEnumerable<Ingredient> GetRandomIngridients()
            {
                for (int i = 0; i < 3; i++)
                    yield return Database.Ingredients[Random.Range(0, Database.Ingredients.Length)].Clone();
            }
        }
	}
}