using System.Linq;
using Taster.Gameplay;
using UnityEngine;

public class DecoreSetter : MonoBehaviour
{
    public GameObject[] Decores;
    void Start() => Decores[LevelSelector.levelID-1].SetActive(true);
}
