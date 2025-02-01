using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

using Taster.Foods.Renderers;

using UnityEngine;
namespace Taster.Foods
{
    public class Food : MonoBehaviour
    {
        public HashSet<Ingridient> Ingridients = new HashSet<Ingridient>();
        public static Food FromIngridients(IEnumerable<Ingridient> ingridients, string name = "New food object")
        {
            Food result = new GameObject(name).AddComponent<Food>();
            foreach(var ingridient in ingridients)
                result.Ingridients.Add(ingridient);
            result.gameObject.AddComponent<PrimitiveRenderer>();
            return result;
        }
        private void OnMouseDown()
        {
            StringBuilder builder = new StringBuilder("This food contains: ");
            foreach(var ingridient in Ingridients)
            {
                builder.Append(ingridient.Name);
                builder.Append(", ");
            }
            print(builder.ToString());
        }
    }
}