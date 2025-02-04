using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenLink(string url) => Application.OpenURL(url);
}
