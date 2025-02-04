using UnityEngine;

public class BookController : MonoBehaviour
{
    [SerializeField] GameObject[] Pages;

    int oldPageId;

    public void OpenPage(int id)
    {
        Pages[oldPageId].SetActive(false);
        Pages[id].SetActive(true);
        oldPageId = id;
    }
}
