using UnityEngine;

[CreateAssetMenu]
public class TowerAsset : ScriptableObject
{
    public int goldCost = 15;
    public Sprite towerGUI;
    public Tower towerPrefab;
    [SerializeField] private UpgradeAsset upgradeAsset;
    [SerializeField] private int requiredUpgradeLevel;

    public bool IsAvailable() => !upgradeAsset || requiredUpgradeLevel <= UpgradesSaver.GetUpgradeLevel(upgradeAsset);

    public TowerAsset[] UpgradesTo;
}
