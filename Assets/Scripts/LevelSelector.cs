using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Taster.DataLoaders;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Text Label, Description;

    static LevelData level;
    void Awake() => ServiceLocator.Register(this);
        
    public void OpenLevel(int id)
    {
        currentLevel = Resources.Load<LevelData>("Levels/Level " + id);

        Label.text = Localization.Get("day") + " " + id + " - " + Localization.Get("level_" + level.Name);
        
        string text = Localization.Get("level_" + level.Name + "_message");
        text = text.Replace("[allergy1]", Database.IngredientDictionary[Database.AllergyIngredients[0]].Name);
        Description.text = text;
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
}
