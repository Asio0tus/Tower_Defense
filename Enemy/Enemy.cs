using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;

[RequireComponent(typeof(Destructible))]
[RequireComponent(typeof(TDPatrolController))]
public class Enemy : MonoBehaviour
{
    public enum ArmorType
    {
        Base = 0,
        Magic = 1
    }

    [SerializeField] private int damage = 1;
    [SerializeField] private int gold = 1;
    [SerializeField] private int armor = 0;
    [SerializeField] private ArmorType armorType;

    private Destructible destructible;

    private SpriteRenderer sr;
    private Color spriteColor;
    public Color SpriteColor => spriteColor;

    public event Action OnDead;

    private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
    {
        //ArmorType.Base
        (int damage, TDProjectile.DamageType damageType, int armor) =>
        {            
            switch (damageType)
            {
                case TDProjectile.DamageType.Base: return Mathf.Max(damage - armor, 1);
                default: return damage;                
            }            
        },

        //ArmorType.Magic
        (int damage, TDProjectile.DamageType damageType, int armor) =>
        {
            switch (damageType)
            {
                case TDProjectile.DamageType.Magic: return damage / 2;
                default: return damage;
            }
        }
    };

    private void Awake()
    {
        destructible = GetComponent<Destructible>();
    }

    private void OnDestroy()
    {        
        OnDead?.Invoke();
    }

    public void Use(EnemyAsset asset)
    {
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        sr.color = asset.color;
        spriteColor = sr.color;
        sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);
        sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

        GetComponent<Spaceship>().Use(asset);

        var col = GetComponentInChildren<CircleCollider2D>();
        col.radius = asset.colliderRadius;

        damage = asset.damage;
        gold = asset.gold;
        armor = asset.armor;
        armorType = asset.armorType;
    }

    public void DamagePlayer()
    {
        TDPlayer.Instance.ChangeHealth(damage);
    }

    public void GivePlayerGold()
    {
        TDPlayer.Instance.ChangeGold(gold);
    }

    public void ChangeSpriteColor(Color newcolor)
    {
        sr.color = newcolor;
    }

    public void TakeDamage(int damage, TDProjectile.DamageType damageType)
    {
        destructible.ApplyDamage(ArmorDamageFunctions[(int)armorType](damage, damageType, armor));
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
    }

#endif

}
