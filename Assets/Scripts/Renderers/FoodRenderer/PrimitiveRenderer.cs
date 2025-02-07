using UnityEngine;

namespace Taster.Foods.Renderers
{
	public class PrimitiveRenderer : FoodRenderer
	{
		protected override void Render()
		{
			gameObject.AddComponent<SpriteRenderer>().sprite = DishesBase.Dishes[Random.Range(0, DishesBase.Dishes.Length)];
			//gameObject.AddComponent<Billboard>();
		}
	}
}