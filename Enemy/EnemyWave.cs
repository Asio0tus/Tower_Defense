using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public static Action<float> OnWavePrepare;

    [Serializable]
    private class Squad
    {
        public EnemyAsset asset;
        public int count;
    }

    [Serializable]
    private class PathGroup
    {
        public Squad[] squads;
    }

    [SerializeField] private float prepareTime = 10f;
    [SerializeField] private PathGroup[] groups;

    private void Awake()
    {
        enabled = false;
    }

    private event Action OnWaveReady;

    public void Prepare(Action spawnEnemies)
    {
        OnWavePrepare?.Invoke(prepareTime);
        prepareTime += Time.time;
        enabled = true;
        OnWaveReady += spawnEnemies;
    }

    private void Update()
    {
        if(Time.time >= prepareTime)
        {
            enabled = false;
            OnWaveReady?.Invoke();
        }
    }

    public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
    {
        for(int i = 0; i < groups.Length; i++)
        {
            foreach(var squad in groups[i].squads)
            {
                yield return (squad.asset, squad.count, i);
            }
            
        }        
    }

    [SerializeField] private EnemyWave nextWave;

    public EnemyWave PrepareNextWave(Action spawnEnemies)
    {
        OnWaveReady -= spawnEnemies;
        if(nextWave) nextWave.Prepare(spawnEnemies);
        return nextWave;
    }

    public float GetRemaningTime()
    {
        return prepareTime - Time.time;
    }
}
