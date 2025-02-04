using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] int Minutes, Seconds;

    Text label;
    void Start()
    {
        label = GetComponent<Text>();
        UpdateTimer();
        StartCoroutine(Timer());
    }

    void UpdateTimer() => label.text = Minutes + ":" + (Seconds > 9 ? Seconds : "0" + Seconds);

    IEnumerator Timer()
    {
        while (Minutes>0 || Seconds>0)
        {
            yield return new WaitForSecondsRealtime(1f);
            Seconds--;
            if (Seconds<0)
            {
                Minutes--;
                Seconds += 60;

                if (Minutes<0)
                {
                    yield break;
                }
            }
            UpdateTimer();
        }
    }
}
