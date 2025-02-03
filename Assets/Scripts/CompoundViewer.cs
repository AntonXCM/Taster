using Taster.Foods;
using Taster.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class CompoundViewer : MonoBehaviour
{
    [SerializeField] GameObject ViewPanel;
    [SerializeField] Image[] Ingredients;

    FoodHolder foodHolder;
    DishSelector dishSelector;

    void Start()
    {
        foodHolder = ServiceLocator.Get<FoodHolder>();
        dishSelector = ServiceLocator.Get<DishSelector>();

        dishSelector.OnChangeDish += ViewCompoundOfDish;
    } 

    void ViewCompoundOfDish()
    {
        bool canGetDish = SelectedDish() != null && foodHolder.AvaibleFoods[dishSelector.SelectID];
        ViewPanel.SetActive(canGetDish);
        if (!canGetDish) return;

        for (int i=0; i<Ingredients.Length; i++)
        {
            Ingredients[i].sprite = SelectedDish().Ingredients[i].Sprite;
        }
    }

    Food SelectedDish()
    {
        if (dishSelector.SelectID >= foodHolder.FoodArray.Length || dishSelector.SelectID<0) return null;
        return foodHolder.FoodArray [dishSelector.SelectID];
    }
}
