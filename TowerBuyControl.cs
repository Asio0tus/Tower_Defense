using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuyControl : MonoBehaviour
{

    [SerializeField] private TowerAsset towerAsset;
    [SerializeField] private Text textCost;
    [SerializeField] private Button button;

    [SerializeField] private Transform buildSite;

    private void Awake()
    {
        TDPlayer.OnGoldUpdate += GoldStatusCheck;        
    }

    private void Start()
    {
        textCost.text = towerAsset.goldCost.ToString();
        button.GetComponent<Image>().sprite = towerAsset.towerGUI;
    }

    private void GoldStatusCheck(int gold)
    {
        if(gold >= towerAsset.goldCost != button.interactable)
        {
            button.interactable = !button.interactable;
            textCost.color = button.interactable ? Color.white : Color.red;
        }
    }

    public void BuyTower()
    {
        TDPlayer.Instance.TryBuyTower(towerAsset, buildSite);
        Debug.Log("buy tower");
    }

    
}
