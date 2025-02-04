using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterGame : MonoBehaviour
{
    public void LoadLevel(int id)
    {
        SceneManager.LoadScene("Game");
    }
}
