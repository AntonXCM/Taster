using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    int Minutes, Seconds = 0;

    Text label;
    void Start()
    {
        Minutes = LevelSelector.currentLevel.Minutes;

        label = GetComponent<Text>();
        UpdateTimer();
        StartCoroutine(Timer());
    }

    void UpdateTimer() => label.text = Minutes + ":" + (Seconds > 9 ? Seconds : "0" + Seconds);

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            
            Seconds--;
            if (Seconds<0)
            {
                Minutes--;
                Seconds += 60;

                if (Minutes<0)
                {
                    SceneManager.LoadScene("GameOver");
                    yield break;
                }
            }
            UpdateTimer();
        }
    }
}
