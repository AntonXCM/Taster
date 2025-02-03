using Taster.DataLoaders;
using UnityEngine;

namespace Taster.Book
{
    public class PoisonCombinations : MonoBehaviour
    {
        [SerializeField] BookCombination bookCombinationPrefab;
        [SerializeField] float spacing, labelSpacing, sideDistance;
        void Start()
        {
            int x = 0, y = 0;
            for (int i = 0; i < Database.DangerCombinations.Count; i++)
            {
                BookCombination currentItem = Instantiate(bookCombinationPrefab, transform);
                currentItem.Combination = Database.DangerCombinations[i];
                currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector2((x*2 - 1) * sideDistance, 0 - labelSpacing - y * spacing);

                x++;
                if (x>1)
                {
                    x = 0;
                    y++;
                }
            }
            if (x == 1) y++;
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, y * spacing + labelSpacing * 2);
        }
    }
}
