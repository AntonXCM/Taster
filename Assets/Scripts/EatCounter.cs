using UnityEngine;
using UnityEngine.UI;

public class EatCounter : MonoBehaviour
{
    Eater eater;
    Text LabelText;

    void Start()
    {
        LabelText = GetComponent<Text>();
        eater = ServiceLocator.Get<Eater>();
        eater.OnEatFood += ChangeScore;
        LabelText.text = "Съедено блюд: " + eater.Score;
    }

    void ChangeScore() => LabelText.text = "Съедено блюд: " + eater.Score;
}
