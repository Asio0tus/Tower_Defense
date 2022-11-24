using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSite : MonoBehaviour, IPointerDownHandler
{
    public static event Action<Transform> OnClickEvent;

    public static void HideBuyControls()
    {
        OnClickEvent(null);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent(transform.root); 
    }
}
