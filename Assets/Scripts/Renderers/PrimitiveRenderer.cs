using UnityEngine;

namespace Taster.Foods.Renderers
{
	public class PrimitiveRenderer : FoodRenderer
	{
		protected override void Render()
		{
			gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("image");
			transform.position = Random.insideUnitCircle.normalized * 3;
			gameObject.AddComponent<BoxCollider2D>();
		}
	}
}