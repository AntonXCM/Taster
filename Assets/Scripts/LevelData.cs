using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public string Name;
    public string[] Rules;
    public int Minutes;
    public int NeedEatDishCount;

    [Range(0, 1)] public float ChanceForPoisonProduct, ChanceForPoisonCombination, ChanceForHealingCombination, ChanceForPoisonAndHealingProduct;
}
