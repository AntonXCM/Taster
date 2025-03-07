using System.Collections;
using Taster.Foods;
using Taster.Gameplay;
using Taster.Gameplay.Rules;
using UnityEngine;

public class HealingCombinations : StomachRule
{
    public override void EatenIngredientsChanged(Stomach stomach) => StartCoroutine(ProcessCombination(stomach));

    IEnumerator ProcessCombination(Stomach stomach)
    {
        foreach (Ingredient mainIngridient in stomach.JustNowEatenIngredients)
        {
            foreach (Ingredient otherIngridient in stomach.JustNowEatenIngredients)
            {
                if (mainIngridient.HealingCombinations.Contains(otherIngridient.Tag))
                {
                    stomach.IsHealingNow = true;
                    yield break;
                }
            }
        }
        yield break;
    }
}
