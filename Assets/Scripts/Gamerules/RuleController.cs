using System.Linq;
using Taster.Gameplay;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    public GameObject[] Rules;
    void Start()
    {
        string[] needRules = LevelSelector.currentLevel.Rules;

        foreach (GameObject rule in Rules) rule.SetActive(needRules.Contains(rule.name));

        ServiceLocator.Get<Stomach>().RegisterAllStomachRules();
    }
}
