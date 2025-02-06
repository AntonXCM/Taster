using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Taster.DataLoaders;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Text Label, Description;

    static LevelData level;
    public static int levelID = 1;
    void Awake() => ServiceLocator.Register(this);
        
    public void OpenLevel(int id)
    {
        levelID = id;
        currentLevel = Resources.Load<LevelData>("Levels/Level " + id);

        Label.text = Localization.Get("day") + " " + id + " - " + Localization.Get("level_" + level.Name);
        Description.text = GetLevelInfo(level.Name);
    }

    public static string GetLevelInfo(string levelName)
    {
        string text = Localization.Get("level_" + levelName + "_message");
        text = text.Replace("[allergy1]", Database.IngredientDictionary[Database.AllergyIngredients[0]].Name);
        return text;
    }

    public void StartLevel() => SceneManager.LoadScene("Game");

    public static LevelData currentLevel
    {
        get { 
            if (level == null)
                currentLevel = Resources.Load<LevelData>("Levels/Level 1");
            return level; 
        }
        private set { level = value; }
    }

    public static void setLevel(LevelData nextLevel) => currentLevel = nextLevel;
}
