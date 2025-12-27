using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager> {

    [SerializeField] private UpgradeType upgradeType;

    private Dictionary<UpgradeType, int> upgradeTypeAndLevelDictionary = new Dictionary<UpgradeType, int>();
    private UpgradeData upgradeData;

    public static event EventHandler<OnGearCogUpgradeEventArgs> OnGearCogUpgrade;
    public class OnGearCogUpgradeEventArgs {
        
        public float attackSpeedBuffAmount;

    }

    protected override void Awake() {
        base.Awake();

        upgradeData = DataManager.Instance.GetUpgradeData();
    }

    public void TriggerUpgrade(UpgradeType upgradeType) {
        int levelNumber;
        if (!upgradeTypeAndLevelDictionary.ContainsKey(upgradeType)) {
            upgradeTypeAndLevelDictionary.Add(upgradeType, 1);
            levelNumber = upgradeTypeAndLevelDictionary[upgradeType];
        } else {
            if (upgradeTypeAndLevelDictionary[upgradeType] == 3) {
                Debug.LogWarning($"Cannot upgrade {upgradeType.ToString()} past 3!");
                return;
            }
            upgradeTypeAndLevelDictionary[upgradeType] += 1;
            levelNumber = upgradeTypeAndLevelDictionary[upgradeType];
        }
        Debug.Log($"Added to dictionary {upgradeType.ToString()} with level {levelNumber}");

        switch (upgradeType) {
            case UpgradeType.GearCog:
                switch (levelNumber) {
                    case 1:
                        OnGearCogUpgrade?.Invoke(this, new OnGearCogUpgradeEventArgs {
                            attackSpeedBuffAmount = upgradeData.gearCog.LevelOne_AttackSpeedBuff
                        });
                        break;
                    case 2:
                        OnGearCogUpgrade?.Invoke(this, new OnGearCogUpgradeEventArgs {
                            attackSpeedBuffAmount = upgradeData.gearCog.LevelTwo_AttackSpeedBuff
                        });
                        break;
                    case 3:
                        OnGearCogUpgrade?.Invoke(this, new OnGearCogUpgradeEventArgs {
                            attackSpeedBuffAmount = upgradeData.gearCog.LevelThree_AttackSpeedBuff
                        });
                        break;
                }
                break;
        }
    }

    [ContextMenu("Trigger Selected Upgrade")]
    private void Editor_TriggerUpgrade() {
        switch (upgradeType) {
            case UpgradeType.GearCog:
                TriggerUpgrade(upgradeType);
                break;
        }
    }

}
