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
        yield return null;

        foreach (Ingredient mainIngridient in stomach.JustNowEatenIngredients)
        {
            foreach (Ingredient otherIngridient in stomach.JustNowEatenIngredients)
            {
                if (mainIngridient.DangerCombinations.Contains(otherIngridient.Tag))
                {
                    yield return new WaitForSecondsRealtime(0.5f);
                    stomach.Poison();
                    yield break;
                }
            }
            yield return null;
        }
    }
}
