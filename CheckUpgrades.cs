using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckUpgrades : MonoBehaviour
{
    [Header("UpgradeAssets")]
    [SerializeField] private UpgradeAsset healthUpgrade;
    [SerializeField] private UpgradeAsset goldUpgrade;
    [SerializeField] private UpgradeAsset buildPlaceUpgrade;
    [SerializeField] private UpgradeAsset accuracyUpgrade;

    [Header("Other links")]
    [SerializeField] private GameObject bonusBuildPlace;
    [SerializeField] private Tower[] towersPrefabs;
    [SerializeField] private Tower[] towersOnScene;

    [SerializeField] private float bonusAccuracyCoefficient;
    [SerializeField] private float baseAccuracyCoefficient;


    private void Start()
    {
        int levelHPUpgrade = UpgradesSaver.GetUpgradeLevel(healthUpgrade);
        TDPlayer.Instance.ChangeHealth(-levelHPUpgrade * 5);

        int levelGoldUpgrade = UpgradesSaver.GetUpgradeLevel(goldUpgrade);
        TDPlayer.Instance.ChangeGold(levelGoldUpgrade * 5);

        int levelbuildPlaceUpgrade = UpgradesSaver.GetUpgradeLevel(buildPlaceUpgrade);
        bonusBuildPlace.SetActive(levelbuildPlaceUpgrade > 0);

        int levelAccuracyUpgrade = UpgradesSaver.GetUpgradeLevel(accuracyUpgrade);
        towersOnScene = FindObjectsOfType<Tower>();

        foreach (Tower tower in towersPrefabs)
        {
            if(levelAccuracyUpgrade > 0)
                tower.accuracyCoefficient = bonusAccuracyCoefficient;
            else
                tower.accuracyCoefficient = baseAccuracyCoefficient;
        }

        foreach (Tower tower in towersOnScene)
        {
            if (levelAccuracyUpgrade > 0)
                tower.accuracyCoefficient = bonusAccuracyCoefficient;
            else
                tower.accuracyCoefficient = baseAccuracyCoefficient;
        }

        Debug.Log(levelHPUpgrade);
        Debug.Log(levelGoldUpgrade);
        Debug.Log(levelbuildPlaceUpgrade);
        Debug.Log(levelAccuracyUpgrade);
    }

}
