using UnityEngine;

namespace Taster.Foods.Renderers
{
	[RequireComponent(typeof(Food))]
	public abstract class FoodRenderer : MonoBehaviour
	{
		Food food;
		private void Start()
		{
			food = GetComponent<Food>();
			Render();
		}
		abstract protected void Render();
	}
}