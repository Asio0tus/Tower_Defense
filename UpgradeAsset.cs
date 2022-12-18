using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeAsset : ScriptableObject
{
    public Sprite spriteImageUpgrade;
    public string textStatUpgrade;
    public int[] costUpgrade = { 3 };
}
