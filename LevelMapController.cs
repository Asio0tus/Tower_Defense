using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapController : MonoBehaviour
{
    [SerializeField] private MapLevel[] mapLevels;
       

    private void Start()
    {
        var drawLevel = 0;
        var score = 1;

        while(score != 0 && drawLevel < mapLevels.Length && MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
        {
            mapLevels[drawLevel++].SetLevelData(episode, score);
        }
        for (int i = drawLevel; i < mapLevels.Length; i++)
        {
            mapLevels[i].gameObject.SetActive(false);
        }
    }
}
