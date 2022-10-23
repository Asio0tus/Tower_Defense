using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float m_Radius;

    private Turret[] turrets;
    private Destructible target = null;

    public float Radius => m_Radius;

    private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

    private void Start()
    {
        turrets = GetComponentsInChildren<Turret>();
    }

    private void Update()
    {
        if (target)
        {
            Vector2 targetVector = target.transform.position - transform.position;

            if(targetVector.magnitude <= m_Radius)
            {
                foreach (var turret in turrets)
                {
                    turret.transform.up = targetVector;
                    turret.Fire();
                }
            }
            else
            {
                target = null;
            }
        }
        else
        {
            var enter = Physics2D.OverlapCircle(transform.position, m_Radius);

            if (enter)
            {
                target = enter.transform.root.GetComponent<Destructible>();
            }
        }  
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
}
