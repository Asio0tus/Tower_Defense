using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[RequireComponent(typeof(TDPatrolController))]
public class Enemy : MonoBehaviour
{
    public void Use(EnemyAsset asset)
    {
        var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        sr.color = asset.color;
        sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);
        sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

        GetComponent<Spaceship>().Use(asset);

        var col = GetComponentInChildren<CircleCollider2D>();
        col.radius = asset.colliderRadius;
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
