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
        public string assetName;
        public int level = 0;
    }

    [SerializeField] private UpgradeSave[] upgradeSaves;
    [SerializeField] private UpgradeAsset[] upgradeAssets;

    private new void Awake()
    {
        base.Awake();
        Saver<UpgradeSave[]>.TryLoad(filename, ref upgradeSaves);       
        
        for (int i = 0; i < upgradeAssets.Length; i++)
        {
            upgradeSaves[i].assetName = upgradeAssets[i].assetName;
        }        
    }


    public static void BuyUpgrade(UpgradeAsset upgradeAsset)
    {
        foreach (var upgrade in Instance.upgradeSaves)
        {
            if(upgrade.assetName == upgradeAsset.assetName)            
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
            if (upgrade.assetName == upgradeAsset.assetName)
            {
                return upgrade.level;
            }
        }

        return 0;
    }

    public int GetCostUpgradeLevel(string assetName, int level)
    {
        foreach(var asset in upgradeAssets)
        {
            if(assetName == asset.assetName)
            {
                return asset.costUpgrade[level];
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
                result += Instance.GetCostUpgradeLevel(upgrade.assetName, i);
            }
        }

        return result;
    }
}
