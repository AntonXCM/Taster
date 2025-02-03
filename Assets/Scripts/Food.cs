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
        public static Food FromIngridients(IEnumerable<Ingredient> ingredients, Transform stand, string name = "New food object")
        {
            GameObject newDish = new GameObject(name);
            Food result = newDish.AddComponent<Food>();

            foreach (var ingredient in ingredients)
                result.Ingredients.Add(ingredient);

            newDish.AddComponent<PrimitiveRenderer>();
            newDish.transform.parent = stand;
            newDish.transform.position = stand.position;

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