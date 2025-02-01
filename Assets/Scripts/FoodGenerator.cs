using System.Collections.Generic;

using Taster.Foods;
using UnityEngine;
namespace Taster.Gameplay
{
	public class FoodGenerator : MonoBehaviour
	{
		[SerializeField] Ingridient[] ingridients;
		private void Awake()
		{
            for (int i = 0; i < 5; i++)
				Food.FromIngridients(GetRandomIngridients());
			IEnumerable<Ingridient> GetRandomIngridients() 
			{
				for(int i = 0; i < 3; i++)
					yield return Instantiate(ingridients[Random.Range(0,ingridients.Length)]);
			}
		}
	}
}