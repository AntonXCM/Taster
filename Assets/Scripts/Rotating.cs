using UnityEngine;
namespace SK.Visuals
{
    public class Rotating : MonoBehaviour
    {
        [SerializeField] Transform thingToRotate;
        [SerializeField] float anglePerSecond = 90;
        private void Update() => thingToRotate.Rotate(0,0,Time.deltaTime * anglePerSecond);
    }
}