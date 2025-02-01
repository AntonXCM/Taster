using Taster.Foods;
using Taster.Gameplay;

using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] Stomach stomach;
	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			var collider = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero).collider;
			if(collider!=null && collider.TryGetComponent(out Food food))
				stomach.Eat(food);
		}
	}
}