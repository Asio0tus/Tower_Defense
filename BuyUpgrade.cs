using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuyUpgrade : MonoBehaviour
{
    [SerializeField] private Image upgradeImage;
    [SerializeField] private Text statUpgrade;
    [SerializeField] private Text levelUpgrade;
    [SerializeField] private Text costUpgrade;
    [SerializeField] private Button buttonBuyUpgrade;
    [SerializeField] private UpgradeAsset asset;
    [SerializeField] private GameObject costPanel;
    [SerializeField] private GameObject saledPanel;

    public event Action PlayerBuyUpgrade;

    public void Initialize()
    {
        upgradeImage.sprite = asset.spriteImageUpgrade;        
        

        int savedLevel = UpgradesSaver.GetUpgradeLevel(asset);

        if(savedLevel >= asset.costUpgrade.Length)
        {
            buttonBuyUpgrade.interactable = false;
            //upgradeImage.color = new Color(135, 135, 135, 174);
            levelUpgrade.text = null;
            statUpgrade.text = null;
            costPanel.SetActive(false);
            saledPanel.SetActive(true);
        }
        else
        {
            statUpgrade.text = asset.textStatUpgrade;
            levelUpgrade.text = $"LevelUp #{savedLevel + 1}";
            costUpgrade.text = asset.costUpgrade[savedLevel].ToString();
        }        
    }

    public void CheckCost(int money)
    {
        int savedLevel = UpgradesSaver.GetUpgradeLevel(asset);

        if (asset.costUpgrade[savedLevel] > money)
        {
            //print(asset.costUpgrade[savedLevel]);
            buttonBuyUpgrade.interactable = false;
        }        
    }

    public void Buy()
    {
        UpgradesSaver.BuyUpgrade(asset);
        Initialize();
        PlayerBuyUpgrade?.Invoke();
    }
        
}
