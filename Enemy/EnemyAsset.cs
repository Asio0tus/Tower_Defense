using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[CreateAssetMenu]
public sealed class EnemyAsset : ScriptableObject
{
    [Header("Enemyes form")]
    public Color color = Color.white;
    public Vector2 spriteScale = new Vector2(3, 3);
    public RuntimeAnimatorController animations;

    [Header("Enemyes options")]
    public float moveSpeed = 1;
    public int hitpoints = 10;
    public int score = 10;
    public float colliderRadius = 0.28f;
    public int damage = 1;
    public int gold = 1;
}
