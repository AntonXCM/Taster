using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform thisTransform, cameraTransform;
	private void Start()
	{
		thisTransform = transform;
		cameraTransform = Camera.main.transform;
	}
	void Update()
    {
		Vector3 lookVector = thisTransform.position - cameraTransform.position;
		lookVector.y = 0;
		thisTransform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.LookRotation(lookVector), .5f);
    }
}
