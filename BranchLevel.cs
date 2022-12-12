using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MapLevel))]
public class BranchLevel : MonoBehaviour
{
    [SerializeField] private MapLevel rootLevel;
    private int needPoints = 3;
    [SerializeField] private GameObject lockPanel;
    [SerializeField] private Button levelButton;

    private void Awake()
    {
        lockPanel.SetActive(true);
        levelButton.interactable = false;
    }
           
    
    public void TryActivate()
    {
        gameObject.SetActive(rootLevel.IsComplete);

        if(needPoints <= rootLevel.ScoreResultLevel)
        {
            lockPanel.SetActive(false);
            levelButton.interactable = true;
            GetComponent<MapLevel>().Initialize();
        }
        
    }
}
