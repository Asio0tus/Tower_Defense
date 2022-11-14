using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyControl : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        BuildSite.OnClickEvent += MoveToTransform;

        gameObject.SetActive(false);
    }

    private void MoveToTransform(Transform target)
    {
        if (target)
        {
            var position = Camera.main.WorldToScreenPoint(target.position);

            rectTransform.anchoredPosition = position;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
