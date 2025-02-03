using System.Linq;

using Taster.DataLoaders;

using UnityEngine;
namespace Taster.Book
{
    public class PoisonProducts : MonoBehaviour
    {
        [SerializeField] BookItem bookItemPrefab;
        [SerializeField] float spacing, labelSpacing;
        void Start()
        {
            for (int i = 0; i < Database.DangerIngredients.Count; i++)
            {
                BookItem currentItem = Instantiate(bookItemPrefab,transform);
                currentItem.Display = Database.DangerIngredients[i];
                currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, labelSpacing + i * spacing);
            }
        }
    }
}