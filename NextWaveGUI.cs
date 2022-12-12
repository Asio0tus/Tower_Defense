using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveGUI : MonoBehaviour
{
    private EnemyWavesManager wavesManager;
    [SerializeField] Button buttonNextLevel;

    [SerializeField] private Text amountBonusText;
    private float timeToNextWave;

    private void Start()
    {
        wavesManager = FindObjectOfType<EnemyWavesManager>();
        EnemyWave.OnWavePrepare += (float time) =>
        {
            timeToNextWave = time;
        };

        CheckButtonNextWaveToActive();
    }

    public void CallWave()
    {
        wavesManager.ForceNextWave();
        CheckButtonNextWaveToActive();
    }

    private void Update()
    {
        if(timeToNextWave > 0)
        {
            amountBonusText.text = ((int)timeToNextWave).ToString();
            timeToNextWave -= Time.deltaTime;
        }
        
        if(timeToNextWave <= 0 || wavesManager.CurrentWave == null)
        {
            amountBonusText.text = "0";
        }       
    }

    private void CheckButtonNextWaveToActive()
    {
        if (wavesManager.CurrentWave == null)
        {
            buttonNextLevel.interactable = false;
        }
    }
}
