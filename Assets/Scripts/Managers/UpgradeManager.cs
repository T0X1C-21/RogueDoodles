using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager> {

    [SerializeField] private UpgradeType upgradeType;

    private Dictionary<UpgradeType, int> upgradeTypeAndLevelDictionary = new Dictionary<UpgradeType, int>();
    private UpgradeData_Runtime upgradeData;

    public static event EventHandler<OnAttackSpeedPlusPlusUpgradeEventArgs> OnAttackSpeedPlusPlusUpgrade;
    public class OnAttackSpeedPlusPlusUpgradeEventArgs {
        
        public float attackSpeedToMultiply;

    }
    public static event EventHandler<OnProjectileCountPlusPlusUpgradeEventArgs> OnProjectileCountPlusPlusUpgrade;
    public class OnProjectileCountPlusPlusUpgradeEventArgs {
        
        public int projectileCountToAdd;

    }
    public static event EventHandler<OnPiercingPlusPlusUpgradeEventArgs> OnPiercingPlusPlusUpgrade;
    public class OnPiercingPlusPlusUpgradeEventArgs {
        
        public int piercingToAdd;

    }
    public static event EventHandler<OnSizePlusPlusUpgradeEventArgs> OnSizePlusPlusUpgrade;
    public class OnSizePlusPlusUpgradeEventArgs {
        
        public float sizeToMultiply;

    }

    protected override void Awake() {
        base.Awake();

        upgradeData = RuntimeGameData.Instance.GetUpgradeData();
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
            case UpgradeType.AttackSpeedPlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnAttackSpeedPlusPlusUpgrade?.Invoke(this, new OnAttackSpeedPlusPlusUpgradeEventArgs {
                            attackSpeedToMultiply = upgradeData.AttackSpeedPlusPlus.LevelOne_AttackSpeedToMultiply
                        });
                        break;
                    case 2:
                        OnAttackSpeedPlusPlusUpgrade?.Invoke(this, new OnAttackSpeedPlusPlusUpgradeEventArgs {
                            attackSpeedToMultiply = upgradeData.AttackSpeedPlusPlus.LevelTwo_AttackSpeedToMultiply
                        });
                        break;
                    case 3:
                        OnAttackSpeedPlusPlusUpgrade?.Invoke(this, new OnAttackSpeedPlusPlusUpgradeEventArgs {
                            attackSpeedToMultiply = upgradeData.AttackSpeedPlusPlus.LevelThree_AttackSpeedToMultiply
                        });
                        break;
                }
                break;
            case UpgradeType.ProjectileCountPlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnProjectileCountPlusPlusUpgrade?.Invoke(this, new OnProjectileCountPlusPlusUpgradeEventArgs {
                            projectileCountToAdd = upgradeData.ProjectileCountPlusPlus.LevelOne_ProjectileCountToAdd
                        });
                        break;
                    case 2:
                        OnProjectileCountPlusPlusUpgrade?.Invoke(this, new OnProjectileCountPlusPlusUpgradeEventArgs {
                            projectileCountToAdd = upgradeData.ProjectileCountPlusPlus.LevelTwo_ProjectileCountToAdd
                        });
                        break;
                    case 3:
                        OnProjectileCountPlusPlusUpgrade?.Invoke(this, new OnProjectileCountPlusPlusUpgradeEventArgs {
                            projectileCountToAdd = upgradeData.ProjectileCountPlusPlus.LevelThree_ProjectileCountToAdd
                        });
                        break;
                }
                break;
            case UpgradeType.PiercingPlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnPiercingPlusPlusUpgrade?.Invoke(this, new OnPiercingPlusPlusUpgradeEventArgs {
                            piercingToAdd = upgradeData.ProjectileCountPlusPlus.LevelOne_ProjectileCountToAdd
                        });
                        break;
                    case 2:
                        OnPiercingPlusPlusUpgrade?.Invoke(this, new OnPiercingPlusPlusUpgradeEventArgs {
                            piercingToAdd = upgradeData.ProjectileCountPlusPlus.LevelTwo_ProjectileCountToAdd
                        });
                        break;
                    case 3:
                        OnPiercingPlusPlusUpgrade?.Invoke(this, new OnPiercingPlusPlusUpgradeEventArgs {
                            piercingToAdd = upgradeData.ProjectileCountPlusPlus.LevelThree_ProjectileCountToAdd
                        });
                        break;
                }
                break;
            case UpgradeType.SizePlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnSizePlusPlusUpgrade?.Invoke(this, new OnSizePlusPlusUpgradeEventArgs {
                            sizeToMultiply = upgradeData.SizePlusPlus.LevelOne_SizeToMultiply
                        });
                        break;
                    case 2:
                        OnSizePlusPlusUpgrade?.Invoke(this, new OnSizePlusPlusUpgradeEventArgs {
                            sizeToMultiply = upgradeData.SizePlusPlus.LevelTwo_SizeToMultiply
                        });
                        break;
                    case 3:
                        OnSizePlusPlusUpgrade?.Invoke(this, new OnSizePlusPlusUpgradeEventArgs {
                            sizeToMultiply = upgradeData.SizePlusPlus.LevelThree_SizeToMultiply
                        });
                        break;
                }
                break;
        }
    }

    [ContextMenu("Trigger Selected Upgrade")]
    private void Editor_TriggerUpgrade() {
        TriggerUpgrade(upgradeType);
    }

}
