using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSite : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TowerAsset[] towerAssetToBuild;
    public TowerAsset[] TowerAssetToBuild => towerAssetToBuild;

    public static event Action<BuildSite> OnClickEvent;

    public void SetTowerAssetToBuild(TowerAsset[] towers)
    {
        if (towers == null || towers.Length == 0) gameObject.SetActive(false);
        towerAssetToBuild = towers;
    }    

    public static void HideBuyControls()
    {
        OnClickEvent(null);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent(this); 
    }
}
