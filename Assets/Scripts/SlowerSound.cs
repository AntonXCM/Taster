using UnityEngine;

public class SlowerSound : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().pitch = 1 - (PlayerPrefs.GetInt("Completed levels")) / 5 * 0.2f;
    }
}
