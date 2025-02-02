using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

using Taster.DataLoaders;
using Taster.Foods.Renderers;

using UnityEngine;
namespace Taster.Foods
{
    public class Food : MonoBehaviour
    {
        public List<Ingredient> Ingredients = new();
        public static Food FromIngridients(IEnumerable<Ingredient> ingredients, string name = "New food object")
        {
            Food result = new GameObject(name).AddComponent<Food>();
            foreach(var ingredient in ingredients)
                result.Ingredients.Add(ingredient);
            result.gameObject.AddComponent<PrimitiveRenderer>();
            return result;
        }
        private void OnMouseDown()
        {
            StringBuilder builder = new StringBuilder("This food contains: ");
            foreach(var ingredient in Ingredients)
            {
                builder.Append(Localization.Get(ingredient.Tag));
                builder.Append(", ");
            }
            print(builder.ToString());
        }
    }
}