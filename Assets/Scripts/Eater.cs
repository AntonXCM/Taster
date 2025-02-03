using Taster.Gameplay;
using UnityEngine;

public class Eater : MonoBehaviour
{
    FoodGenerator foodGenerator;
    DishSelector dishSelector;
    Stomach stomach;

    void Start()
    {
        foodGenerator = ServiceLocator.Get<FoodGenerator>();
        dishSelector = ServiceLocator.Get<DishSelector>();
        stomach = ServiceLocator.Get<Stomach>();
    }

    public void EatSelectedDish()
    {
        stomach.Eat(foodGenerator.FoodArray[dishSelector.SelectID]);
    }
}
