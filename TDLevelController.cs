using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDLevelController : LevelController
{
    private new void Start()
    {
        base.Start();
        TDPlayer.Instance.OnPlayerDead += () =>
        {
            StopLevelActivity();
            LevelResultController.Instance.Show(false);
        };

        m_EventLevelCompleted.AddListener(StopLevelActivity);
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
    }

}
