using Taster.DataLoaders;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSetter : MonoBehaviour
{
    [SerializeField] Image LanguageIcon;
    [SerializeField] string[] ExistsLanguages;

    int languageID;

    void Start()
    {
        for (int i = 0; i < ExistsLanguages.Length; i++)
            if (Localization.currentLanguage == ExistsLanguages[i])
                languageID = i;

        LanguageIcon.sprite = Localization.CurrentFlag;
    }
    public void ChangeLanguage()
    {
        languageID++;
        if (languageID >= ExistsLanguages.Length) languageID = 0;
        Localization.SetLanguage(ExistsLanguages[languageID]);
        LanguageIcon.sprite = Localization.CurrentFlag;
    }
}
