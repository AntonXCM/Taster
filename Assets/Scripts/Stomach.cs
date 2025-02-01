using System.Collections.Generic;

using Taster.Foods;
using Taster.Gameplay.Rules;

using UnityEngine;
namespace Taster.Gameplay
{
	public class Stomach : MonoBehaviour
	{
		public int Health = 3;

		public HashSet<Ingredient> EatenIngredients = new();
		public event EatenIngredientsChanged OnEatenIngredientsChanged;
		public delegate void EatenIngredientsChanged(Stomach stomach);

		void Start()
		{
			foreach(var rule in GameRule.All)
				if(rule is StomachRule)
					OnEatenIngredientsChanged += s=>((StomachRule)rule).EatenIngredientsChanged(s);
		}

		public void Eat(Food food)
		{
			foreach(var ingridient in EatenIngredients)
				ingridient.DigestionTime--;
			foreach(var ingridient in food.Ingredients)
				EatenIngredients.Add(ingridient);

			OnEatenIngredientsChanged?.Invoke(this);

			EatenIngredients.RemoveWhere(a => a.DigestionTime <= 0);
			Destroy(food.gameObject);
		}
	}
}