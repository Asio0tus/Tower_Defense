using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    [SerializeField] private float m_Velocity;
    public float ProjectileSpeed => m_Velocity;
    [SerializeField] private float m_Lifetime;
    [SerializeField] protected int m_Damage;
    [SerializeField] private ImpactEffect m_ImpactEffect;
    private float m_Timer;

    
    private void Update()
    {
        float stepLenght = Time.deltaTime * m_Velocity;
        Vector2 step = transform.up * stepLenght;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

        if (hit)
        {
            OnHit(hit);

            OnProjectileLifeEnd(hit.collider, hit.point);
        }

        m_Timer += Time.deltaTime;

        if (m_Timer > m_Lifetime)
            Destroy(gameObject);

        transform.position += new Vector3(step.x, step.y, 0);
    }


    


        protected virtual void OnHit(RaycastHit2D hit)
        {
            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null && dest != m_Parent)
            {
                ActivateDamage(dest);

                /*if(m_Parent != null && m_Parent == Player.Instance.ActiveShip)
                {
                    Player.Instance.AddScore(dest.ScoreValue);
                }*/

            }
        }

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
    {
        Destroy(gameObject);
    }

    private Destructible m_Parent;

    public void SetParentShooter(Destructible parent)
    {
        m_Parent = parent;
    }

    protected virtual void ActivateDamage(Destructible dest)
    {
        dest.ApplyDamage(m_Damage);
    }
        
}
