using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TDPlayer : Player
{
    public static new TDPlayer Instance { get { return Player.Instance as TDPlayer; } }

    [SerializeField] private int m_gold;

    public static event Action<int> OnGoldUpdate;
    public static event Action<int> OnHealthUpdate;

    private void Start()
    {
        OnGoldUpdate(m_gold);
        OnHealthUpdate(Health);
    }

    public void ChangeGold(int changeValue)
    {
        m_gold += changeValue;
        OnGoldUpdate(m_gold);
    }

    public void ChangeHealth(int changeValue)
    {
        TakeDamage(changeValue);
        OnHealthUpdate(Health);
    }

    public void BuildTower(Vector3 position, Tower towerPrefab)
    {
        var newTower = Instantiate(towerPrefab, position, Quaternion.identity);
    }

    public void TryBuyTower(TowerAsset towerAsset, Transform buildSite)
    {
        if (towerAsset.goldCost <= m_gold)
        {
            ChangeGold(-towerAsset.goldCost);
            BuildTower(buildSite.position, towerAsset.towerPrefab);
            BuildSite.HideBuyControls();
            Destroy(buildSite.gameObject);
        }
        else return;
    }

}
