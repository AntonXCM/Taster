using System.Collections.Generic;

using Taster.DataLoaders;
using Taster.Foods;
using UnityEngine;
namespace Taster.Gameplay
{
	public class FoodGenerator : MonoBehaviour
	{
		[SerializeField] Transform[] FoodStands;
		public Food[] FoodArray;
		
		public int StandsCount => FoodStands.Length;

		private void Awake() => ServiceLocator.Register(this);

		private void Start()
		{
			FoodArray = new Food[StandsCount];

            for (int i = 0; i<FoodStands.Length; i++)
			{
                FoodArray[i] = Food.FromIngridients(GetRandomIngridients(), FoodStands[i]);
			}

			IEnumerable<Ingredient> GetRandomIngridients() 
			{
				for(int i = 0; i < 3; i++)
					yield return Database.Ingredients[Random.Range(0, Database.Ingredients.Length)].Clone();
			}
		}
	}
}