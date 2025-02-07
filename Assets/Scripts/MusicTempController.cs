using UnityEngine;
using System.Collections;

public class MusicTempController : MonoBehaviour
{
    LevelTimer levelTimer;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelTimer = ServiceLocator.Get<LevelTimer>();

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (levelTimer.Minutes < 2)
                audioSource.pitch = 1f + (2f - (levelTimer.Minutes*60 + levelTimer.Seconds) / 60f);
        }
    }
}