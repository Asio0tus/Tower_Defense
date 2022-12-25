using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Abilities : SingletonBase<Abilities>
{
    

    [Serializable]
    public class FireAbility 
    {
        [SerializeField] private int cost = 15;
        public int Cost => cost;
        [SerializeField] private float coolDown = 20f;
        [SerializeField] private float radiusExplosion = 5f;
        [SerializeField] private Image damageZoneCircle;
        [SerializeField] private Button fireButton;        
        private bool coolDownFinish = true;

        public int damage = 2;

        public void ChangeFireCoolDownUpgrade(int upgradeLevel)
        {
            coolDown /= upgradeLevel;
        }

        public void CheckButtonIntaractable(int mana)
        {
            if (cost <= mana && coolDownFinish) fireButton.interactable = true;
            else fireButton.interactable = false;
        }

        public void Use()
        {
            ClickProtection.Instance.Activate((Vector2 v) =>
            {
                Vector3 position = v;
                position.z = -Camera.main.transform.position.z;
                position = Camera.main.ScreenToWorldPoint(position);

                //Debug.Log(position);

                foreach(var collider in Physics2D.OverlapCircleAll(position, radiusExplosion))
                {
                    if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                    {
                        enemy.TakeDamage(damage, TDProjectile.DamageType.Magic);
                    }
                }
            });

            IEnumerator TimeAbilityButton()
            {
                coolDownFinish = false;
                yield return new WaitForSeconds(coolDown);
                coolDownFinish = true;
                CheckButtonIntaractable(TDPlayer.Instance.CurrentMana);
            }

            Instance.StartCoroutine(TimeAbilityButton());
        }       
    }

    [Serializable]
    public class TimeAbility 
    {
        [SerializeField] private int cost = 10;
        public int Cost => cost;
        [SerializeField] private float coolDown = 15f;
        [SerializeField] private float duration = 5;
        [SerializeField] private Button timeButton;        
        private bool coolDownFinish = true;

        public void ChangeFreezCoolDownUpgrade(int upgradeLevel)
        {
            coolDown /= upgradeLevel;
        }

        public void CheckButtonIntaractable(int mana)
        {
            if (cost <= mana && coolDownFinish) timeButton.interactable = true;
            else timeButton.interactable = false;
        }

        public void Use() 
        { 
            void Slow(Enemy e) { e.GetComponent<Spaceship>().HalfMaxLinearVelosity(); }
            
            IEnumerator Restore()
            {
                yield return new WaitForSeconds(duration);
                foreach (var ship in FindObjectsOfType<Spaceship>())
                {
                    ship.RestoreMaxLinearVelosity();
                }
                EnemyWavesManager.OnEnemySpawn -= Slow;
            }

            IEnumerator TimeAbilityButton()
            {
                coolDownFinish = false;
                yield return new WaitForSeconds(coolDown);
                coolDownFinish = true;
                CheckButtonIntaractable(TDPlayer.Instance.CurrentMana);
            }

            foreach (var ship in FindObjectsOfType<Spaceship>())
            {                
                ship.HalfMaxLinearVelosity();
            }

            EnemyWavesManager.OnEnemySpawn += Slow;
            Instance.StartCoroutine(Restore());
            Instance.StartCoroutine(TimeAbilityButton());                
        }
    }

    [SerializeField] private FireAbility fireAbility;
    [SerializeField] private TimeAbility timeAbility;      
      

    public void UseFireAbility()
    {      
        if(fireAbility.Cost <= TDPlayer.Instance.CurrentMana)
        {
            fireAbility.Use();
            TDPlayer.Instance.ChangeMana(-fireAbility.Cost);
        }
    }
    public void UseTimeAbility()
    {
        if (timeAbility.Cost <= TDPlayer.Instance.CurrentMana)
        {
            timeAbility.Use();
            TDPlayer.Instance.ChangeMana(-timeAbility.Cost);
        }
    }

    public void CheckButtonIntaractable(int mana)
    {
        timeAbility.CheckButtonIntaractable(mana);
        fireAbility.CheckButtonIntaractable(mana);
    }

    public void ChangeFireCoolDownTimeUpgrade(int upgradeLevel) => fireAbility.ChangeFireCoolDownUpgrade(upgradeLevel);
    public void ChangeFreezCoolDownTimeUpgrade(int upgradeLevel) => timeAbility.ChangeFreezCoolDownUpgrade(upgradeLevel);


    [SerializeField] private Text timeText;
    [SerializeField] private Text fireText;


    private void Start()
    {
        TDPlayer.OnManaUpdate += CheckButtonIntaractable;
        timeText.text = timeAbility.Cost.ToString();
        fireText.text = fireAbility.Cost.ToString();
    }

    private void OnDestroy()
    {
        TDPlayer.OnManaUpdate -= CheckButtonIntaractable;
    }

}
