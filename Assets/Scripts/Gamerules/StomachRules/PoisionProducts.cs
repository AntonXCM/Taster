using Taster.DataLoaders;

namespace Taster.Gameplay.Rules
{
	public class PoisonProducts : StomachRule
	{
		public override void EatenIngredientsChanged(Stomach stomach)
		{
			foreach (var ingridient in stomach.JustNowEatenIngredients)
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