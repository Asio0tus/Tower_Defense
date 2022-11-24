using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingEffect : ProjectileSpecialEffects
{
    [SerializeField] private int m_BoostValue;
    [SerializeField] private float m_MaxTime;    

    protected override void ActivateProjectileEffect(Spaceship ship)
    {
        ship.ActivateFreezing(m_MaxTime);
    }
}
