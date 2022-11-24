using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyControl : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        BuildSite.OnClickEvent += MoveToBuildSite;

        gameObject.SetActive(false);
    }

    private void MoveToBuildSite(Transform buildSite)
    {
        if (buildSite)
        {
            var position = Camera.main.WorldToScreenPoint(buildSite.position);

            rectTransform.anchoredPosition = position;
            gameObject.SetActive(true);

            foreach(var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
