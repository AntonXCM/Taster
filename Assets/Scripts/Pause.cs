using UnityEngine;

public class Pause : MonoBehaviour
{
    public void StartPause() => Time.timeScale = 0;

    public void StopPause() => Time.timeScale = 1;
}
