using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class ProjectileSpecialEffects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spaceship ship = collision.transform.root.GetComponent<Spaceship>();

        if(ship != null)
        {
            ActivateProjectileEffect(ship);            
        }
    }

    protected abstract void ActivateProjectileEffect(Spaceship ship);
}
