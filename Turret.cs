using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private TurretProperties m_TurretProperties;
    public TurretProperties TurretProperties => m_TurretProperties;

    private float m_RefireTimer;

    public bool CanFire => m_RefireTimer <= 0;

    private Spaceship m_Ship;

    private void Start()
    {
        m_Ship = transform.root.GetComponent<Spaceship>();
    }

    private void Update()
    {
        if (m_RefireTimer >= 0)
            m_RefireTimer -= Time.deltaTime;
        else if (m_Mode == TurretMode.Auto)
            Fire();
    }

    // Public API
    public void Fire()
    {
        if (m_TurretProperties == null) return;

        if (m_RefireTimer > 0) return;

        if(m_Ship != null)
        {
            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;

            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
        }        

        Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.transform.up = transform.up;
        projectile.SetParentShooter(m_Ship);

        m_RefireTimer = m_TurretProperties.RateOfFire;

        //Debug.Log("Fire");

        //SFX Audio
    }

    public void AssignLoadout(TurretProperties props)
    {
        if (m_Mode != props.Mode) return;

        m_RefireTimer = 0;
        m_TurretProperties = props;
    }
}

