using Taster.Foods;
using Taster.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class CompoundViewer : MonoBehaviour
{
    [SerializeField] Image[] Ingredients;

    FoodGenerator foodGenerator;
    DishSelector dishSelector;

    void Start()
    {
        foodGenerator = ServiceLocator.Get<FoodGenerator>();
        dishSelector = ServiceLocator.Get<DishSelector>();

        dishSelector.OnChangeDish += ViewCompoundOfDish;

        Invoke("ViewCompoundOfDish", 0);
    } 

    void ViewCompoundOfDish()
    {
        for (int i=0; i<Ingredients.Length; i++)
        {
            Ingredients[i].sprite = foodGenerator.FoodArray[dishSelector.SelectID].Ingredients[i].Sprite;
        }
    }
}
