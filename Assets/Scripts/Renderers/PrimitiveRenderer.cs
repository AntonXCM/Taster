using UnityEngine;

namespace Taster.Foods.Renderers
{
	public class PrimitiveRenderer : FoodRenderer
	{
		protected override void Render()
		{
			gameObject.AddComponent<SpriteRenderer>().sprite = food.Ingredients[0].Sprite;
			transform.position = Random.insideUnitCircle.normalized * 3;
			gameObject.AddComponent<BoxCollider2D>();
		}
	}
}