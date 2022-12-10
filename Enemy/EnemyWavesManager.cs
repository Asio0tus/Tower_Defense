using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    [SerializeField] private Path[] paths;
    [SerializeField] private EnemyWave currentWave;
    [SerializeField] private Enemy m_EnemyPrefab;

    private void Start()
    {        
        currentWave.Prepare(SpawnEnemies);
    }

    public void ForceNextWave()
    {
        TDPlayer.Instance.ChangeGold((int)currentWave.GetRemaningTime());
        SpawnEnemies();        
    }

    private void SpawnEnemies()
    {
        foreach((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
        {
            if(pathIndex < paths.Length)
            {
                for(int i = 0; i < count; i++)
                {
                    var e = Instantiate(m_EnemyPrefab, paths[pathIndex].StartArea.GetRandomInsideZone(), Quaternion.identity);
                    e.Use(asset);
                    e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                }
            }
            else
            {
                Debug.LogWarning($"Invalid pathIndex in {name}");
            }
        }
                
        currentWave = currentWave.PrepareNextWave(SpawnEnemies);        
    }
}
