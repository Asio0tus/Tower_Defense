using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private EnemyAsset[] enemySettings;
    [SerializeField] private Enemy m_EnemyPrefab;
    [SerializeField] private Path path;

    protected override GameObject GenerateSpawnEntity()
    {
        var e =  Instantiate(m_EnemyPrefab);
        e.Use(enemySettings[Random.Range(0, enemySettings.Length)]);
        e.GetComponent<TDPatrolController>().SetPath(path);             

        return e.gameObject;
    }
}
