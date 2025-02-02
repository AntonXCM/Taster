using System.Linq;

using Taster.DataLoaders;

using UnityEngine;
namespace Taster.Book
{
    public class PoisonProducts : MonoBehaviour
    {
        [SerializeField] BookItem bookItemPrefab;
        [SerializeField] float spacing, labelSpacing;
        [SerializeField] Transform label;
        void Start()
        {
            Vector3 startPos = label.position + Vector3.down * labelSpacing;
            for (int i = 0; i < Database.Poisons.Length; i++)
            {
                BookItem currentItem = Instantiate(bookItemPrefab,transform);
                currentItem.Display = Database.Ingredients.Where(I=>I.Tag == Database.Poisons[i]).First();
                currentItem.transform.position = startPos + Vector3.down * i * spacing;
            }
        }
    }
}