using System;
using System.Collections.Generic;

using Taster.Foods;
using Taster.Gameplay.Rules;

using UnityEngine;
namespace Taster.Gameplay
{
	public class Stomach : MonoBehaviour
	{
		public bool Poisoned;

		public HashSet<Ingredient> EatenIngredients = new();
        public List<Ingredient> JustNowEatenIngredients = new();
        public event EatenIngredientsChanged OnEatenIngredientsChanged;
		public delegate void EatenIngredientsChanged(Stomach stomach);

		public Action OnChangePoisonedStatus;

        private void Awake() => ServiceLocator.Register(this);

        void Start()
		{
			foreach(var rule in GameRule.All)
				if(rule is StomachRule)
					OnEatenIngredientsChanged += s=>((StomachRule)rule).EatenIngredientsChanged(s);
		}

		public void Eat(Food food)
		{
			JustNowEatenIngredients = new();

            foreach (var ingridient in EatenIngredients)
				ingridient.DigestionTime--;
			foreach (var ingridient in food.Ingredients)
			{
				EatenIngredients.Add(ingridient);
                JustNowEatenIngredients.Add(ingridient);
            }

			OnEatenIngredientsChanged?.Invoke(this);

			EatenIngredients.RemoveWhere(a => a.DigestionTime <= 0);
			Destroy(food.gameObject);
		}

		public void Poison()
		{
			if (!Poisoned)
			{
				Poisoned = true;
				OnChangePoisonedStatus.Invoke();
            } 
			else
			{
				Debug.Log("Вы мертвы");
			}
		}
        public void Healing()
        {
            if (Poisoned)
            {
                Poisoned = false;
                OnChangePoisonedStatus.Invoke();
            }
        }
    }
}