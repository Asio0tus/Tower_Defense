using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float timeLimit = 4f;

    public bool IsCompleted => Time.time >= timeLimit;
}