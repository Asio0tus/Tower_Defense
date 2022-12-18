using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDProjectile : Projectile
{
    public enum DamageType
    {
        Base,
        Magic,
        Multipurpose
    }

    [SerializeField] private DamageType damageType;

    protected override void OnHit(RaycastHit2D hit)
    {
        Enemy enemy = hit.collider.transform.root.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(m_Damage, damageType);
        }
    }
}
