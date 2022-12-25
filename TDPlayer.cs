using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TDPlayer : Player
{
    public static new TDPlayer Instance { get { return Player.Instance as TDPlayer; } }

    [SerializeField] private int m_gold;
    public int Gold => m_gold;

    [SerializeField] private int m_maxMana;
    public int MaxMana => m_maxMana;

    private int currentMana;
    public int CurrentMana => currentMana;
    [SerializeField] private int timerManaUpdate = 1;

    public static event Action<int> OnGoldUpdate;
    public static event Action<int> OnHealthUpdate;
    public static event Action<int> OnManaUpdate;



    private void Start()
    {
        currentMana = m_maxMana;

        OnGoldUpdate(m_gold);
        OnHealthUpdate(Health);
        OnManaUpdate(currentMana);
    }

    private float timer = 0;

    private void Update()
    {       

        if (currentMana < MaxMana)
        {            
            timer += Time.deltaTime;

            if(timer >= timerManaUpdate)
            {
                ChangeMana(1);                
                OnManaUpdate(currentMana);
                timer = 0;
            }
            
        }
    }

    public void ChangeGold(int changeValue)
    {
        m_gold += changeValue;
        OnGoldUpdate(m_gold);
    }

    public void ChangeMana(int changeValue)
    {
        currentMana += changeValue;
        if (currentMana > MaxMana) currentMana = MaxMana;
        OnManaUpdate(currentMana);
    }

    public void ChangeHealth(int changeValue)
    {
        TakeDamage(changeValue);
        OnHealthUpdate(Health);
    }

    public void BuildTower(Vector3 position, Tower towerPrefab, TowerAsset asset)
    {
        var newTower = Instantiate(towerPrefab, position, Quaternion.identity);        
        newTower.SetBuilPlacesTowerAsset(asset);        
    }

    public void TryBuyTower(TowerAsset towerAsset, Transform buildSite)
    {
        if (towerAsset.goldCost <= m_gold)
        {
            ChangeGold(-towerAsset.goldCost);
            BuildTower(buildSite.position, towerAsset.towerPrefab, towerAsset);
            BuildSite.HideBuyControls();
            Destroy(buildSite.gameObject);
        }
        else return;
    }

}
