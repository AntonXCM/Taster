using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void OpenScene(string name) => SceneManager.LoadScene(name);

    public void QuitGame() => Application.Quit();
}
