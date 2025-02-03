using UnityEngine;

[CreateAssetMenu(fileName = "DishesBase", menuName = "Scriptable Objects/DishesBase")]
public class DishesBase : ScriptableObject
{
    public Sprite[] DishesArray;

    public static Sprite[] Dishes;

    [RuntimeInitializeOnLoadMethod]
    private static void OnGameStart()
    {
        DishesBase loadBase = Resources.Load<DishesBase>("DishesBase");
        Dishes = loadBase.DishesArray;
    }
}
