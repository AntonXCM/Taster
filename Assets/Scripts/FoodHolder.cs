using System.Collections;
using Taster.Foods;
using Taster.Gameplay;
using UnityEngine;

public class FoodHolder : MonoBehaviour
{
    public Transform[] FoodStands;
    public Food[] FoodArray;
    [HideInInspector] public bool[] AvaibleFoods;
    [SerializeField] float TimeForReloadDish;

    public int StandsCount => FoodStands.Length;

    Animator[] standAnimators;

    DishSelector dishSelector;
    FoodGenerator foodGenerator;

    private void Awake() => ServiceLocator.Register(this);
    void Start()
    {
        dishSelector = ServiceLocator.Get<DishSelector>();
        foodGenerator = ServiceLocator.Get<FoodGenerator>();

        standAnimators = new Animator[FoodStands.Length];
        AvaibleFoods = new bool[FoodStands.Length];
        for (int i = 0; i < FoodStands.Length; i++)
        {
            standAnimators[i] = FoodStands[i].GetComponent<Animator>();
            AvaibleFoods[i] = true;
        }
    }

    public void ChangeSelectFood()
    {
        if (AvaibleFoods[dishSelector.SelectID]) StartCoroutine(ChangeFoodOnID(dishSelector.SelectID));
    }

    IEnumerator ChangeFoodOnID(int id)
    {
        AvaibleFoods[id] = false;
        standAnimators[id].Play("Hide");
        dishSelector.ChangeDish();

        yield return new WaitForSecondsRealtime(TimeForReloadDish);

        foodGenerator.GenerateRandomDish(id);
        standAnimators[id].Play("Show");

        yield return new WaitForSecondsRealtime(0.3f);

        AvaibleFoods[id] = true;
        if (dishSelector.SelectID == id) dishSelector.ChangeDish();
    }
}
