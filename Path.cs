using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private AIPatrolArea[] points;
    public int Length { get { return points.Length; } }
    public AIPatrolArea this[int i] { get => points[i]; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        foreach(var point in points)
        {
            Gizmos.DrawSphere(point.transform.position, point.Radius);
        }
    }
} 
