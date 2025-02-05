using Taster.DataLoaders;

namespace Taster.Gameplay.Rules
{
    public class NeedPoisonProducts : StomachRule
    {
        public override void EatenIngredientsChanged(Stomach stomach)
        {
            foreach (var ingridient in stomach.JustNowEatenIngredients)
                if (!Database.IsSafe(ingridient.Tag)) return;
            stomach.IsPoisonedNow = true;
        }
    }
}