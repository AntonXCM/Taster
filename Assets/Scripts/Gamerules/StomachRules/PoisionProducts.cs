using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Taster.Gameplay.Rules
{
	public class PoisonProducts : StomachRule
	{
		public override void EatenIngredientsChanged(Stomach stomach)
		{
			foreach (var ingridient in stomach.EatenIngredients)
				if(Database.Poisons.Contains(ingridient.Name)) stomach.Health--;
		}
	}
}