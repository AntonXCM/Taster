using Taster.DataLoaders;
using Taster.Foods;
using UnityEngine;
using UnityEngine.UI;

public class BookCombination : MonoBehaviour
{
    [SerializeField] Image image1, image2;

    string[] combination;
    public string[] Combination
    {
        get => combination;
        set
        {
            combination = value;
            image1.sprite = Database.IngredientDictionary[combination[0]].Sprite;
            image2.sprite = Database.IngredientDictionary[combination[1]].Sprite;
        }
    }
}
