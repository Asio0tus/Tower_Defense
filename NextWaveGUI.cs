using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveGUI : MonoBehaviour
{
    private EnemyWavesManager wavesManager;

    [SerializeField] private Text amountBonusText;
    private float timeToNextWave;

    private void Start()
    {
        wavesManager = FindObjectOfType<EnemyWavesManager>();
        EnemyWave.OnWavePrepare += (float time) =>
        {
            timeToNextWave = time;
        };
    }

    public void CallWave()
    {
        wavesManager.ForceNextWave();
    }

    private void Update()
    {
        if(timeToNextWave > 0)
        {
            amountBonusText.text = ((int)timeToNextWave).ToString();
            timeToNextWave -= Time.deltaTime;
        }
        else
        {
            amountBonusText.text = "0";
        }       
    }
}
