using UnityEngine;
using UnityEngine.UI;
using Taster.DataLoaders;

public class LevelCard : MonoBehaviour
{
    [SerializeField] int Number;
    [SerializeField] string Name;
    [SerializeField] Text DayNumberText, LabelText;

    bool opened;
    LevelSelector levelSelector;

    void Start()
    {
        levelSelector = ServiceLocator.Get<LevelSelector>();

        GetComponent<Button>().onClick.AddListener(SelectLevel);

        opened = Number <= PlayerPrefs.GetInt("Completed levels") + 1;
        GetComponent<Button>().interactable = opened;

        Localization.OnChangeLanguage += SetText;
        SetText();
    }

    void SelectLevel() => levelSelector.OpenLevel(Number);

    void SetText()
    {
        DayNumberText.text = Localization.Get("day") + " " + Number;
        LabelText.text = opened ? Localization.Get("level_" + Name) : "?";
    }

    private void OnDestroy() => Localization.OnChangeLanguage -= SetText;
}
