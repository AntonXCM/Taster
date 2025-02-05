using System;
using Taster.Gameplay;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Eater : MonoBehaviour
{
    public int Score;

    FoodHolder foodHolder;
    DishSelector dishSelector;
    Stomach stomach;

    public Action OnEatFood;

    private void Awake() => ServiceLocator.Register(this);

    void Start()
    {
        foodHolder = ServiceLocator.Get<FoodHolder>();
        dishSelector = ServiceLocator.Get<DishSelector>();
        stomach = ServiceLocator.Get<Stomach>();
    }

    public bool CanEatThis => foodHolder.AvaibleFoods[dishSelector.SelectID];

    public void EatSelectedDish()
    {
        if (!CanEatThis) return;

        stomach.Eat(foodHolder.FoodArray[dishSelector.SelectID]);
        foodHolder.FoodArray[dishSelector.SelectID] = null;
        foodHolder.ChangeSelectFood();
        Score++;
        OnEatFood?.Invoke();
    }
}
