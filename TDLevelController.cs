using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDLevelController : LevelController
{
    private int levelScore = 3;
    public int LevelScore => levelScore;

    private float timeAfterStartLevel;

    private new void Start()
    {
        base.Start();
        TDPlayer.Instance.OnPlayerDead += () =>
        {
            StopLevelActivity();
            LevelResultController.Instance.Show(false);
        };

        m_ReferenceTime += Time.time;

        m_EventLevelCompleted.AddListener(() =>
        {
            StopLevelActivity();
            if (m_ReferenceTime <= Time.time) levelScore -= 1;            
            MapCompletion.SaveEpisodeResult(levelScore);
        });

        timeAfterStartLevel = 0;

        void LifeScoreChange(int _)
        {
            if(timeAfterStartLevel >= 1)
            {
                levelScore -= 1;
                TDPlayer.OnHealthUpdate -= LifeScoreChange;
            }            
        }

        TDPlayer.OnHealthUpdate += LifeScoreChange;
    }

    private void FixedUpdate()
    {
        timeAfterStartLevel += Time.fixedDeltaTime;
    }

    private void StopLevelActivity()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            enemy.GetComponent<Spaceship>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        foreach(var enemySpawner in FindObjectsOfType<EnemySpawner>())
        {
            enemySpawner.enabled = false;
        }

        foreach (var projectile in FindObjectsOfType<Projectile>())
        {
            projectile.enabled = false;
        }

        foreach (var tower in FindObjectsOfType<Tower>())
        {
            tower.enabled = false;
        }

        foreach (var GUI in FindObjectsOfType<NextWaveGUI>())
        {
            GUI.enabled = false;
        }
    }

}
