using System.Collections.Generic;

using Taster.DataLoaders;
using Taster.Foods;
using UnityEngine;
namespace Taster.Gameplay
{
	public class FoodGenerator : MonoBehaviour
	{
		private void Awake()
		{
            for (int i = 0; i < 5; i++)
				Food.FromIngridients(GetRandomIngridients());
			IEnumerable<Ingredient> GetRandomIngridients() 
			{
				for(int i = 0; i < 3; i++)
					yield return Database.Ingredients[Random.Range(0, Database.Ingredients.Length)].Clone();
			}
		}
	}
}