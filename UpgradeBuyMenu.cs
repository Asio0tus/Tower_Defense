using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeBuyMenu : MonoBehaviour
{   
    private int money;
    private int totalScore;

    [SerializeField] private Text moneyText;
    [SerializeField] private BuyUpgrade[] sales;

    [SerializeField] private GameObject upgradeShopPanelObject;
    [SerializeField] private GameObject upgradeButtonObject;

    private void Awake()
    {
        upgradeShopPanelObject.SetActive(false);
        upgradeButtonObject.SetActive(true);
    }

    private void Start()
    {  

        foreach(var slot in sales)
        {
            slot.Initialize();
            slot.PlayerBuyUpgrade += UpdateMoney;
        }

        UpdateMoney();
    }

    

    public void UpdateMoney()
    {
        totalScore = MapCompletion.Instance.TotalScore;
        money = totalScore - UpgradesSaver.GetTotalSpendCost();

        moneyText.text = money.ToString();

        foreach(var slot in sales)
        {
            slot.CheckCost(money);
        }
    }

    
}
