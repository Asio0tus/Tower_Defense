using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Destructible object on scene. That may have hitpoints.
/// </summary>
public class Destructible : Entity
{
    #region Properties
    /// <summary>
    /// Object ignores damage
    /// </summary>
    [SerializeField] private bool m_Indestructible;
    public bool IsIndestructible => m_Indestructible;

    /// <summary>
    /// Object`s hitpoints on starrt
    /// </summary>
    [SerializeField] private int m_HitPoints;

    /// <summary>
    /// Object`s current hitpoints
    /// </summary>
    private int m_CurrentHitPoints;
    public int HitPoints => m_CurrentHitPoints;
    #endregion

    #region Unity Events

    protected virtual void Start()
    {
        m_CurrentHitPoints = m_HitPoints;
    }

    #endregion

    #region Public API

    /// <summary>
    /// Object getting damage
    /// </summary>
    /// <param name="damage"> damage points </param>
    public void ApplyDamage(int damage)
    {
        if (m_Indestructible) return;

        m_CurrentHitPoints -= damage;

        if (m_CurrentHitPoints <= 0)
            OnDeath();
    }

    #endregion

    /// <summary>
    /// Override event. Base: Destroy object if HitPoits <= zero
    /// </summary>
    protected virtual void OnDeath()
    {
        m_EventOnDeath?.Invoke();
        Destroy(gameObject);        
    }

    private static HashSet<Destructible> m_AllDestructibles;
    public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

    protected virtual void OnEnable()
    {
        if (m_AllDestructibles == null)
            m_AllDestructibles = new HashSet<Destructible>();

        m_AllDestructibles.Add(this);
    }

    protected virtual void OnDestroy()
    {
        m_AllDestructibles.Remove(this);
    }

    public const int TeamIDNeutral = 0;

    [SerializeField] private int m_TeamID;
    public int TeamID => m_TeamID;

    [SerializeField] private UnityEvent m_EventOnDeath;
    public UnityEvent EventOnDeath => m_EventOnDeath;

    #region Score

    [SerializeField] private int m_ScoreValue;
    public int ScoreValue => m_ScoreValue;



    #endregion

    protected void Use(EnemyAsset asset)
    {
        m_HitPoints = asset.hitpoints;
        m_ScoreValue = asset.score;
    }
}

