using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDPatrolController : AIController
{
    private Path path;
    private int pathIndex;

    [SerializeField] private UnityEvent OnEndPath;
    
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
        }
        else
        {
            OnEndPath.Invoke();
            Destroy(gameObject);
        }
    }
}
