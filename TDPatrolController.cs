using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPatrolController : AIController
{
    private Path path;
    private int pathIndex;

    
    public void SetPath(Path newPath)
    {       
        path = newPath;
        pathIndex = 0;
        SetPatrolBehaviour(path[pathIndex]);        
    }

    protected override void GetNewPoint()
    {
        pathIndex++;

        if (path.Length > pathIndex)
        {
            SetPatrolBehaviour(path[pathIndex]);
            Debug.Log(pathIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
