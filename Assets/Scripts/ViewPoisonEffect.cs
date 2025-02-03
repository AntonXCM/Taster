using Taster.Gameplay;
using UnityEngine;

public class ViewPoisonEffect : MonoBehaviour
{
    [SerializeField] Animator GreenBG;
    [SerializeField] Transform CameraTransform;
    [SerializeField] float CameraShakingPower, CameraShakingSpeed;

    Stomach stomach;

    float poisonTime, poisonLocalPower;
    bool effectActive;

    void Start()
    {
        stomach = ServiceLocator.Get<Stomach>();
        stomach.OnChangePoisonedStatus += OnChangePoisonedStatus;
    }

    void OnChangePoisonedStatus()
    {
        GreenBG.SetBool("View", stomach.Poisoned);
        if (stomach.Poisoned)
        {
            poisonTime = 0;
            poisonLocalPower = 0;
            effectActive = true;
        }
    }

    private void Update()
    {
        if (!effectActive) return;

        poisonTime += Time.deltaTime;
        CameraTransform.transform.eulerAngles = new Vector3(0, 0, poisonLocalPower * Mathf.Cos(poisonTime * CameraShakingSpeed) * CameraShakingPower);

        if (stomach.Poisoned)
        {
            if (poisonLocalPower < 1) poisonLocalPower = Mathf.Min(poisonLocalPower + Time.deltaTime / 5, 1);
        } 
        else
        {
            poisonLocalPower = Mathf.Max(poisonLocalPower - Time.deltaTime / 5, 0);
            if (poisonLocalPower <= 0) effectActive = false;
        }
    }
}
