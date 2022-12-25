using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    public static event Action<Enemy> OnEnemySpawn;

    [SerializeField] private Path[] paths;
    [SerializeField] private EnemyWave currentWave;
    public EnemyWave CurrentWave => currentWave;

    [SerializeField] private Enemy m_EnemyPrefab;
    private int activeEnemyCount = 0;

    public event Action OnAllWavesDead;

    private void Start()
    {        
        currentWave.Prepare(SpawnEnemies);
    }

    public void ForceNextWave()
    {
        if (currentWave)
        {
            TDPlayer.Instance.ChangeGold((int)currentWave.GetRemaningTime());
            SpawnEnemies();
        }
        else
        {
            if(activeEnemyCount == 0)
            {
                OnAllWavesDead?.Invoke();
            }            
        }
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
                    e.OnDead += RecordEnemyDead;
                    e.Use(asset);
                    e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                    activeEnemyCount++;
                    OnEnemySpawn?.Invoke(e);
                }
            }
            else
            {
                Debug.LogWarning($"Invalid pathIndex in {name}");
            }
        }
                
        currentWave = currentWave.PrepareNextWave(SpawnEnemies);        
    }

    private void RecordEnemyDead()
    {
        activeEnemyCount--;

        if(activeEnemyCount == 0)
        {
            ForceNextWave();
        }
    }
}
