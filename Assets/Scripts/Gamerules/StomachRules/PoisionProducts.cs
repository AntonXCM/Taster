using System.Collections.Generic;
using System.Linq;

using Taster.DataLoaders;

using UnityEngine;
namespace Taster.Gameplay.Rules
{
	public class PoisonProducts : StomachRule
	{
		public override void EatenIngredientsChanged(Stomach stomach)
		{
			foreach (var ingridient in stomach.EatenIngredients)
			{
				if (!Database.IsSafe(ingridient.Tag))
				{
					stomach.Poison();
					return;
				}
			}
        }
	}
}