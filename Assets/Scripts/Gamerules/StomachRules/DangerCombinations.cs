using System.Collections;
using Taster.Foods;
using Taster.Gameplay;
using Taster.Gameplay.Rules;
using UnityEngine;

public class DangerCombinations : StomachRule
{
    public override void EatenIngredientsChanged(Stomach stomach) => StartCoroutine(ProcessCombination(stomach));

    IEnumerator ProcessCombination(Stomach stomach)
    {
        foreach (Ingredient mainIngridient in stomach.JustNowEatenIngredients)
        {
            foreach (Ingredient otherIngridient in stomach.JustNowEatenIngredients)
            {
                if (mainIngridient.DangerCombinations.Contains(otherIngridient.Tag))
                {
                    stomach.IsPoisonedNow = true;
                    yield break;
                }
            }
        }
        yield break;
    }
}
