using Taster.Foods;
using Taster.Gameplay;
using Taster.Gameplay.Rules;

public class HealingCombinations : StomachRule
{
    public override void EatenIngredientsChanged(Stomach stomach)
    {
        foreach (Ingredient mainIngridient in stomach.EatenIngredients)
        {
            foreach (string combinationIngridient in mainIngridient.HealingCombinations)
            {
                foreach (Ingredient otherIngridient in stomach.EatenIngredients)
                {
                    if (combinationIngridient == otherIngridient.Tag)
                    {
                        stomach.Healing();
                        return;
                    }
                }
            }
        }
    }
}
