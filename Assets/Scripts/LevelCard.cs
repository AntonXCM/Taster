using UnityEngine;
using UnityEngine.UI;
using Taster.DataLoaders;

public class LevelCard : MonoBehaviour
{
    [SerializeField] int Number;
    [SerializeField] string Name;
    [SerializeField] Text DayNumberText, LabelText;

    LevelSelector levelSelector;

    void Start()
    {
        levelSelector = ServiceLocator.Get<LevelSelector>();

        GetComponent<Button>().onClick.AddListener(SelectLevel);

        Localization.OnChangeLanguage += SetText;
        SetText();
    }

    void SelectLevel() => levelSelector.OpenLevel(Number);

    void SetText()
    {
        DayNumberText.text = Localization.Get("day") + " " + Number;
        LabelText.text = Localization.Get("level_" + Name);
    }

    private void OnDestroy() => Localization.OnChangeLanguage -= SetText;
}
