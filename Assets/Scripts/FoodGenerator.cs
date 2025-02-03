using System.Collections.Generic;

using Taster.DataLoaders;
using Taster.Foods;
using UnityEngine;
namespace Taster.Gameplay
{
	public class FoodGenerator : MonoBehaviour
	{
		[SerializeField] Transform[] FoodStands;
		private void Awake()
		{
			foreach (Transform i in FoodStands)
			{
				Food.FromIngridients(GetRandomIngridients(), i);
			}

			IEnumerable<Ingredient> GetRandomIngridients() 
			{
				for(int i = 0; i < 3; i++)
					yield return Database.Ingredients[Random.Range(0, Database.Ingredients.Length)].Clone();
			}
		}
	}
}