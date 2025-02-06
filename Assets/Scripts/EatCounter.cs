using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EatCounter : MonoBehaviour
{
    Eater eater;
    Text LabelText;

    int needCount;

    void Start()
    {
        LabelText = GetComponent<Text>();
        eater = ServiceLocator.Get<Eater>();
        eater.OnEatFood += ChangeScore;
        needCount = LevelSelector.currentLevel.NeedEatDishCount;
        ChangeScore();
    }

    void ChangeScore()
    {
        LabelText.text = eater.Score + "/" + needCount;
        if (eater.Score >= needCount) SceneManager.LoadScene("Win");
    }
}
