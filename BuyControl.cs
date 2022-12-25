using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyControl : MonoBehaviour
{
    [SerializeField] private TowerBuyControl towerBuyControlPrefab;      
    private List<TowerBuyControl> activeTowerBuyControl;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        BuildSite.OnClickEvent += MoveToBuildSite;

        gameObject.SetActive(false);
    }

    private void MoveToBuildSite(BuildSite buildSite)
    {
        if (buildSite)
        {
            var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);

            rectTransform.anchoredPosition = position;
            activeTowerBuyControl = new List<TowerBuyControl>();
            foreach(var asset in buildSite.TowerAssetToBuild)
            {
                if(asset.IsAvailable())                
                {
                    var newControl = Instantiate(towerBuyControlPrefab, transform);
                    activeTowerBuyControl.Add(newControl);                    
                    newControl.SetTowerBuyControlAsset(asset);
                    newControl.GoldStatusCheck(TDPlayer.Instance.Gold);
                }                
            }

            if(activeTowerBuyControl.Count > 0)
            {                
                gameObject.SetActive(true);

                var angle = 360 / activeTowerBuyControl.Count;

                for (int i = 0; i < activeTowerBuyControl.Count; i++)
                {
                    var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.up * 125);
                    activeTowerBuyControl[i].transform.position += offset;
                }

                foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                {
                    tbc.SetBuildSite(buildSite.transform.root);
                }
            }

            
        }
        else
        {
            if(activeTowerBuyControl != null)
            {
                foreach (var control in activeTowerBuyControl) Destroy(control.gameObject);
                activeTowerBuyControl.Clear();                
            }
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        BuildSite.OnClickEvent -= MoveToBuildSite;
    }
}
