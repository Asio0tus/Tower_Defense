using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class UpgradesSaver : SingletonBase<UpgradesSaver>
{
    public const string filename = "upgrades.dat";

    [Serializable]
    private class UpgradeSave
    {      
        public UpgradeAsset asset;
        public int level = 0;
    }

    [SerializeField] private UpgradeSave[] upgradeSaves;

    private new void Awake()
    {
        base.Awake();
        Saver<UpgradeSave[]>.TryLoad(filename, ref upgradeSaves);
    }


    public static void BuyUpgrade(UpgradeAsset upgradeAsset)
    {
        foreach (var upgrade in Instance.upgradeSaves)
        {
            if(upgrade.asset == upgradeAsset)
            {
                upgrade.level += 1;
                Saver<UpgradeSave[]>.Save(filename, Instance.upgradeSaves);
            }
        }
    }

    public static int GetUpgradeLevel(UpgradeAsset upgradeAsset)
    {
        foreach (var upgrade in Instance.upgradeSaves)
        {
            if (upgrade.asset == upgradeAsset)
            {
                return upgrade.level;
            }
        }

        return 0;
    }

    public static int GetTotalSpendCost()
    {
        int result = 0;

        foreach( var upgrade in Instance.upgradeSaves)
        {
            for(int i = 0; i < upgrade.level; i++)
            {
                result += upgrade.asset.costUpgrade[i];
            }
        }

        return result;
    }
}
