using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapController : MonoBehaviour
{
    [SerializeField] private MapLevel[] mapLevels;
    [SerializeField] private BranchLevel[] branchLeves;   

    private void Start()
    {
        var drawLevel = 0;
        var score = 1;

        /*    while(score != 0 && drawLevel < mapLevels.Length)
            {
                mapLevels[drawLevel].Initialize();

                if (!mapLevels[drawLevel - 1].IsComplete && (drawLevel - 1) >= 0)
                {
                    mapLevels[drawLevel].SetLevelLocked();
                }

                drawLevel++;
            }

            for (int i = drawLevel; i < mapLevels.Length; i++)
            {
                mapLevels[i].gameObject.SetActive(true);
                if(!mapLevels[i-1].IsComplete && (i - 1) >= 0)
                {
                    mapLevels[i].SetLevelLocked();
                }            
            }
        */

        for (int i = 0; i < mapLevels.Length; i++)
        {
            mapLevels[i].gameObject.SetActive(true);
            mapLevels[drawLevel].Initialize();
            
            if(i > 0)
            {
                if (!mapLevels[i - 1].IsComplete)
                {
                    mapLevels[i].SetLevelLocked();
                }
            }
            
            
        }

        for (int i = 0; i < branchLeves.Length; i++)
        {
            branchLeves[i].TryActivate();
        }
    }    
}
