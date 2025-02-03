using Taster.Gameplay;
using UnityEngine;

public class Eater : MonoBehaviour
{
    FoodHolder foodHolder;
    DishSelector dishSelector;
    Stomach stomach;

    void Start()
    {
        foodHolder = ServiceLocator.Get<FoodHolder>();
        dishSelector = ServiceLocator.Get<DishSelector>();
        stomach = ServiceLocator.Get<Stomach>();
    }

    public void EatSelectedDish()
    {
        stomach.Eat(foodHolder.FoodArray[dishSelector.SelectID]);
        foodHolder.FoodArray[dishSelector.SelectID] = null;
        foodHolder.ChangeSelectFood();
    }
}
