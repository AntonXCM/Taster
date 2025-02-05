using Taster.DataLoaders;
using UnityEngine;

namespace Taster.Gameplay.Rules
{
	public class Allergy : StomachRule
	{
		[SerializeField] int AllergyNumber;
        public override void EatenIngredientsChanged(Stomach stomach)
		{
			foreach (var ingridient in stomach.JustNowEatenIngredients)
			{
				if (ingridient.Tag == Database.AllergyIngredients[AllergyNumber])
				{
					stomach.IsPoisonedNow = true;
					return;
				}
			}
        }
	}
}