using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler
{
    private Image blockImage;

    private void Start()
    {
        blockImage = GetComponent<Image>();        
    }

    private Action<Vector2> OnClickAction;

    public void Activate (Action<Vector2> mouseAction)
    {
        blockImage.enabled = true;
        OnClickAction = mouseAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        blockImage.enabled = false;
        OnClickAction(eventData.pressPosition);
        OnClickAction = null;
    }
}
