using Taster.Foods;

using UnityEngine;
using UnityEngine.UI;
namespace Taster.Book
{
    public class BookItem : MonoBehaviour
    {
        [SerializeField] Text title;
        [SerializeField] Image image;
        Ingredient display;
        public Ingredient Display
        { 
            get => display;
            set
            {
                display = value;
                title.text = display.Name;
                image.sprite = display.Sprite;
            }
        }
    }
}