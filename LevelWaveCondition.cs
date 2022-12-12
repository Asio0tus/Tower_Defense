using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWaveCondition : MonoBehaviour, ILevelCondition
{
    private bool isCompleted;
    public bool IsCompleted => isCompleted;

    private void Start()
    {
        FindObjectOfType<EnemyWavesManager>().OnAllWavesDead += () =>
        {
            isCompleted = true;
        };
    }
}
