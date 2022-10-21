using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonBase<Player>
{
    [SerializeField] private int m_NumLives;
    [SerializeField] private Spaceship m_Ship;
    [SerializeField] private GameObject m_PlayerShipPrefab;

    public Spaceship ActiveShip => m_Ship;

    //[SerializeField] private CameraController m_CameraController;
    //[SerializeField] private MovementController m_MovementController;

    private void Start()
    {
        m_Ship.EventOnDeath.AddListener(OnShipDeath);
    }

    private void OnShipDeath()
    {
        m_NumLives--;

        if (m_NumLives > 0) 
            Invoke("Respawn", 1.0f);
    }

    private void Respawn()
    {
        var newPlayerShip = Instantiate(m_PlayerShipPrefab);

        m_Ship = newPlayerShip.GetComponent<Spaceship>();
        m_Ship.EventOnDeath.AddListener(OnShipDeath);

        //m_CameraController.SetTarget(m_Ship.transform);
        //m_MovementController.SetTargetShip(m_Ship);

    }

    #region Score

    public int Score { get; private set; }
    public int NumKills { get; private set; }

    public void AddKill()
    {
        NumKills++;
    }

    public void AddScore(int num)
    {
        Score += num;
    }

    #endregion
}
