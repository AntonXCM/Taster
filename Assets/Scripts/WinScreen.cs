using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Taster.DataLoaders;

public class WinScreen : MonoBehaviour
{
    [SerializeField] Text Label, NextLabel, Description;
    [SerializeField] GameObject NextButton;

    LevelData nextLevel;
    void Start()
    {
        if (PlayerPrefs.GetInt("Completed levels") < LevelSelector.levelID) 
            PlayerPrefs.SetInt("Completed levels", LevelSelector.levelID);

        nextLevel = Resources.Load<LevelData>("Levels/Level " + (LevelSelector.levelID + 1));

        Label.text = Localization.Get("day") + " " + LevelSelector.levelID + " - " + Localization.Get("level_" + LevelSelector.currentLevel.Name);
        
        if (nextLevel != null)
        {
            NextLabel.text = Localization.Get("day") + " " + (LevelSelector.levelID + 1) + " - " + Localization.Get("level_" + nextLevel.Name);
            Description.text = LevelSelector.GetLevelInfo(nextLevel.Name);
        } 
        else
        {
            NextLabel.text = Localization.Get("day") + " " + (LevelSelector.levelID + 1) + " - " + Localization.Get("end");
            Description.text = Localization.Get("ending_message");
            NextButton.SetActive(false);
        }
    }

    public void StartNextLevel()
    {
        if (nextLevel == null) return;
        LevelSelector.setLevel(nextLevel);
        LevelSelector.levelID++;
        SceneManager.LoadScene("Game");
    }
}
