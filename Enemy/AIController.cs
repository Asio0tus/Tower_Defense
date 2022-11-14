using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spaceship))]
public class AIController : MonoBehaviour
{
    public enum AIBehavior
    {
        Null,
        PatrolArea,
        PatrolPoint
    }

    [SerializeField] private AIBehavior m_AIBehavior;
    [SerializeField] private AIPatrolArea m_PatrolArea;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_NavigationLinear;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_NavigationAngular;

    [SerializeField] private float m_RandomSelectMovePointTime;
    [SerializeField] private float m_FindNewTargetTime;
    [SerializeField] private float m_ShootDelay;
    [SerializeField] private float m_EvadeRayLenght;
    private Spaceship m_Spaceship;
    private Vector3 m_MovePosition;
    private Destructible m_SelectedTarget;

    [SerializeField] private AIPatrolArea[] m_PatrolPoints;
    private bool m_PatrolPointForward = true;

    [SerializeField] private float m_LeadTime;

    private Timer m_RandomizeDirectionTimer;
    private Timer m_FireTimer;
    private Timer m_FindNewTargetTimer;

    private void Start()
    {
        m_Spaceship = GetComponent<Spaceship>();

        InitTimers();

        if (m_AIBehavior == AIBehavior.PatrolPoint) PatrolPointStart();
    }

    private void Update()
    {
        UpdateTimers();

        UpdateAI();
    }

    private void UpdateAI()
    {        
        if (m_AIBehavior == AIBehavior.PatrolArea || m_AIBehavior == AIBehavior.PatrolPoint)
        {
            UpdateBehaviourPatrol();
        }
    }

    private void UpdateBehaviourPatrol()
    {
        ActionFindNewAttackTarget();
        ActionFindNewMovePosition();
        ActionControlShip();        
        ActionFire();
        ActionEvadeCollision();
    }

    private void ActionFindNewMovePosition()
    {
        if(m_AIBehavior == AIBehavior.PatrolArea || m_AIBehavior == AIBehavior.PatrolPoint)
        {
            if(m_SelectedTarget != null)
            {
                m_MovePosition = m_SelectedTarget.transform.position;  
            }
            else
            {
                if(m_AIBehavior == AIBehavior.PatrolArea && m_PatrolArea != null)
                {
                    bool isInsidePatrolZone = (m_PatrolArea.transform.position - transform.position).sqrMagnitude < m_PatrolArea.Radius * m_PatrolArea.Radius;

                    if(isInsidePatrolZone == true)
                    {
                        GetNewPoint();
                    }
                    else
                    {
                        m_MovePosition = m_PatrolArea.transform.position;
                    }
                }

                if(m_AIBehavior == AIBehavior.PatrolPoint)
                {
                    PatrolPointSwitch();
                }
            }
        }
    }

    protected virtual void GetNewPoint()
    {
        if (m_RandomizeDirectionTimer.IsFinished)
        {
            Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolArea.Radius + m_PatrolArea.transform.position;
            m_MovePosition = newPoint;

            m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
        }
    }

    protected virtual void PatrolPointSwitch()
    {
        if(m_PatrolPoints != null)
        {
            if (m_PatrolPointForward)
            {
                for (int i = 0; i < m_PatrolPoints.Length; i++)
                {
                    if ((m_PatrolPoints[i].transform.position - transform.position).sqrMagnitude < m_PatrolPoints[i].Radius * m_PatrolPoints[i].Radius && i != m_PatrolPoints.Length - 1)
                    {
                        m_MovePosition = m_PatrolPoints[i + 1].transform.position;
                    }

                    if ((m_PatrolPoints[i].transform.position - transform.position).sqrMagnitude < m_PatrolPoints[i].Radius * m_PatrolPoints[i].Radius && i == m_PatrolPoints.Length - 1)
                    {
                        m_MovePosition = m_PatrolPoints[i - 1].transform.position;
                        m_PatrolPointForward = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_PatrolPoints.Length; i++)
                {
                    if ((m_PatrolPoints[i].transform.position - transform.position).sqrMagnitude < m_PatrolPoints[i].Radius * m_PatrolPoints[i].Radius && i != 0)
                    {
                        m_MovePosition = m_PatrolPoints[i - 1].transform.position;
                    }

                    if ((m_PatrolPoints[i].transform.position - transform.position).sqrMagnitude < m_PatrolPoints[i].Radius * m_PatrolPoints[i].Radius && i == 0)
                    {
                        m_MovePosition = m_PatrolPoints[i + 1].transform.position;
                        m_PatrolPointForward = true;
                    }
                }
            }            
        }
    }

    private void PatrolPointStart()
    {
        if (m_PatrolPoints != null)
            m_MovePosition = m_PatrolPoints[0].transform.position;
    }

    private void ActionEvadeCollision()
    {
        if(Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLenght) == true)
        {
            m_MovePosition = transform.position + transform.right * 100.0f;
        }
    }


    private void ActionControlShip()
    {
        m_Spaceship.ThrustControl = m_NavigationLinear;

        m_Spaceship.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_Spaceship.transform) * m_NavigationAngular;
    }

    private const float MAX_ANGLE = 45.0f;

    private static float ComputeAliginTorqueNormalized(Vector3 targetPostion, Transform ship)
    {
        Vector2 localTargetPosition = ship.InverseTransformPoint(targetPostion);
        float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);        
        angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;        
        return -angle;
    }

    private void ActionFindNewAttackTarget()
    {
        if (m_FindNewTargetTimer.IsFinished == true)
        {
            m_SelectedTarget = FindNearestDestructibleTarget();            
            m_FindNewTargetTimer.Start(m_FindNewTargetTime);
        }
    }
    private void ActionFire()
    {
        if(m_SelectedTarget != null)
        {
            if(m_FireTimer.IsFinished == true)
            {
                m_Spaceship.Fire(TurretMode.Primary);
                m_FireTimer.Start(m_ShootDelay);
            }
        }
    }

    private Destructible FindNearestDestructibleTarget()
    {
        float minDist = float.MaxValue;

        Destructible potentialTarget = null;

        foreach(var v in Destructible.AllDestructibles)
        {
            if (v.GetComponent<Spaceship>() == m_Spaceship) continue;
            if (v.TeamID == Destructible.TeamIDNeutral) continue;
            if (v.TeamID == m_Spaceship.TeamID) continue;

            float dist = Vector2.Distance(m_Spaceship.transform.position, v.transform.position);

            if(dist < minDist)
            {
                minDist = dist;
                potentialTarget = v;
            }
        }

        return potentialTarget;
    }

    private Vector3 MakeLead(Destructible dest)
    {
        Spaceship targetShip = dest.GetComponentInParent<Spaceship>();

        if(targetShip != null)
        {
            float tempo = targetShip.ThrustControl * m_LeadTime;
            var target = dest.transform.position + (dest.transform.up * tempo);
            return target;
        }
        else
        {
            return dest.transform.position;
        }
    }

    public void SetPatrolBehaviour(AIPatrolArea point)
    {
        m_AIBehavior = AIBehavior.PatrolArea;
        m_PatrolArea = point;
    }
    

    #region Timers

    private void InitTimers()
    {
        m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
        m_FireTimer = new Timer(m_ShootDelay);
        m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
    }

    private void UpdateTimers()
    {
        m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
        m_FireTimer.RemoveTime(Time.deltaTime);
        m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
    }

    #endregion

}
