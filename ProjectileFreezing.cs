using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFreezing : Projectile
{
    [SerializeField] private float m_MaxTime;    

    protected override void ActivateDamage(Destructible dest)
    {
        base.ActivateDamage(dest);

        var enemy = dest.GetComponentInParent<Spaceship>();

        if (enemy)
        {
            enemy.ActivateFreezing(m_MaxTime);
        }
    }
}
