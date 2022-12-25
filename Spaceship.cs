using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship : Destructible
{
    /// <summary>
    /// Mass for automatic set rigid
    /// </summary>
    [Header("Space ship")]
    [SerializeField] private float m_Mass;

    /// <summary>
    /// Pushing force forward
    /// </summary>
    [SerializeField] private float m_Thrust;

    /// <summary>
    /// Rotating force
    /// </summary>
    [SerializeField] private float m_Mobility;

    

    /// <summary>
    /// Max line speed
    /// </summary>
    [SerializeField] private float m_MaxLinearVelocity;
    private float m_MaxLinearVelocityBackup;
    public float ShipSpeed => m_MaxLinearVelocity;
    public void HalfMaxLinearVelosity() 
    { 
        m_MaxLinearVelocityBackup = m_MaxLinearVelocity; 
        m_MaxLinearVelocity = 0.3f; 
    }
    public void RestoreMaxLinearVelosity() { m_MaxLinearVelocity = m_MaxLinearVelocityBackup; }

    /// <summary>
    /// Max rotational speed. degrees per second
    /// </summary>
    [SerializeField] private float m_MaxAngularVelocity;

    /// <summary>
    /// Save link to rigid
    /// </summary>
    private Rigidbody2D m_Rigid;

    #region Public API

    /// <summary>
    /// Control linear thrust. -1.0 to +1.0
    /// </summary>
    public float ThrustControl { get; set; }

    /// <summary>
    /// Control rotational thrust. -1.0 to +1.0
    /// </summary>
    public float TorqueControl { get; set; }

    #endregion

    #region Unity Event
    protected override void Start()
    {
        base.Start();

        m_Rigid = GetComponent<Rigidbody2D>();
        m_Rigid.mass = m_Mass;

        m_Rigid.inertia = 1;

        //InitOffensive();
    }
       

    private void FixedUpdate()
    {
        //UpdateEnergyRegen();

        UpdateRigidBody();
        
        if(activateFreezing == true)
        {
            timerBoostSpeed -= Time.fixedDeltaTime;

            if(timerBoostSpeed <= 0) DeactivateFreezing();
        }
        
    }

    #endregion

    /// <summary>
    /// Method adding forces to move spaceship
    /// </summary>
    private void UpdateRigidBody()
    {        
        m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    /*
    #region Offensive

    [SerializeField] private Turret[] m_Turrets;

    

    [SerializeField] private int m_MaxEnergy;
    [SerializeField] private int m_MaxAmmo;
    [SerializeField] private int m_EnergyRegenPerSecond;
    private float m_PrimaryEnergy;
    private int m_SecondaryAmmo;

    public void AddEnergy(int energy)
    {
        m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
    }

    public void AddAmmo(int ammo)
    {
        m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
    }

    private void InitOffensive()
    {
        m_PrimaryEnergy = m_MaxEnergy;
        m_SecondaryAmmo = m_MaxAmmo;
    }

    private void UpdateEnergyRegen()
    {
        if(m_PrimaryEnergy < m_MaxEnergy) 
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
    }
        
    
    public void AssignWeapon(TurretProperties props)
    {
        for(int i = 0; i < m_Turrets.Length; i++)
        {
            m_Turrets[i].AssignLoadout(props);
        }
    }

    #endregion
    */

    private float startMaxLinearVelocity;    
    private bool activateFreezing = false;
    private float timerBoostSpeed;

    public void ActivateFreezing(float maxTimeUseBoost)
    {
        if(activateFreezing == false) 
        {
            startMaxLinearVelocity = m_MaxLinearVelocity;
           
            timerBoostSpeed = maxTimeUseBoost;
            activateFreezing = true;

            var enemy = GetComponentInParent<Enemy>();
            enemy.ChangeSpriteColor(Color.blue);

            
            m_MaxLinearVelocity = 0.3f;
        }        
    }

    private void DeactivateFreezing()
    {
        if(activateFreezing == true)
        {
            timerBoostSpeed = 0;

            var enemy = GetComponentInParent<Enemy>();
            enemy.ChangeSpriteColor(enemy.SpriteColor);

            m_MaxLinearVelocity = startMaxLinearVelocity;
            

            activateFreezing = false;
        }      
            
    }

    /// <summary>
    /// TODO: заменить временный метод-заглушку
    /// используется турелями
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool DrawAmmo(int count)
    {
        return true;
    }

    /// <summary>
    /// TODO: заменить временный метод-заглушку
    /// используется турелями
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool DrawEnergy(int energy)
    {
        return true;
    }

    /// <summary>
    /// TODO: заменить временный метод-заглушку
    /// используется ИИ
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public void Fire(TurretMode mode)
    {
        return;
    }


    public new void Use(EnemyAsset asset)
    {
        m_MaxLinearVelocity = asset.moveSpeed;
        base.Use(asset);
    }

}
