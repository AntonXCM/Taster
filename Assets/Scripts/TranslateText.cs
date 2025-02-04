using Taster.DataLoaders;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TranslateText : MonoBehaviour
{
    Text label;
    string key;
    void Start()
    {
        label = GetComponent<Text>();
        key = label.text;
        Localization.OnChangeLanguage += SetText;
        SetText();
    }

    void SetText() => label.text = Localization.Get(key);

    private void OnDestroy() => Localization.OnChangeLanguage -= SetText;
}
