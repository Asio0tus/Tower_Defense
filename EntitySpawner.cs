using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : Spawner
{
    //[SerializeField] private EnemyAsset[] enemySettings;
    [SerializeField] private GameObject[] m_EntityPrefab;
    //[SerializeField] private Path path;

    protected override GameObject GenerateSpawnEntity()
    {
        return Instantiate( m_EntityPrefab[Random.Range(0, m_EntityPrefab.Length)]);
            
            /*if(e.TryGetComponent<TDPatrolController>(out var ai))
            {
                ai.SetPath(path);
            }

            if(e.TryGetComponent<Enemy>(out var en))
            {
                en.Use(enemySettings[Random.Range(0, enemySettings.Length)]);
            } */           
    }
}
