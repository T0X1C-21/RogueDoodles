using System;
using System.Collections.Generic;
using UnityEngine;

public class PassiveManager : Singleton<PassiveManager> {

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
    public static event EventHandler<OnAttackDamagePlusPlusUpgradeEventArgs> OnAttackDamagePlusPlusUpgrade;
    public class OnAttackDamagePlusPlusUpgradeEventArgs {
        
        public float attackDamageToMultiply;

    }

    [SerializeField] private PassiveType upgradeType;

    private UpgradeData_Runtime upgradeData;
    private Dictionary<PassiveType, int> equippedPassiveAndLevelDictionary = new Dictionary<PassiveType, int>();

    protected override void Awake() {
        base.Awake();

        upgradeData = RuntimeGameData.Instance.GetUpgradeData();

    }

    public void TriggerPassiveUpgrade(PassiveType passiveType) {
        int levelNumber;
        if (!equippedPassiveAndLevelDictionary.ContainsKey(passiveType)) {
            equippedPassiveAndLevelDictionary.Add(passiveType, 1);
            levelNumber = equippedPassiveAndLevelDictionary[passiveType];
        } else {
            if (equippedPassiveAndLevelDictionary[passiveType] == 3) {
                Debug.LogWarning($"Cannot upgrade {passiveType.ToString()} past 3!");
                return;
            }
            equippedPassiveAndLevelDictionary[passiveType] += 1;
            levelNumber = equippedPassiveAndLevelDictionary[passiveType];
        }
        Debug.Log($"Added to dictionary {passiveType.ToString()} with level {levelNumber}");

        switch (passiveType) {
            case PassiveType.AttackSpeedPlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnAttackSpeedPlusPlusUpgrade?.Invoke(this, new OnAttackSpeedPlusPlusUpgradeEventArgs {
                            attackSpeedToMultiply = upgradeData.attackSpeedPlusPlus.AttackSpeedToMultiply_LevelOne
                        });
                        break;
                    case 2:
                        OnAttackSpeedPlusPlusUpgrade?.Invoke(this, new OnAttackSpeedPlusPlusUpgradeEventArgs {
                            attackSpeedToMultiply = upgradeData.attackSpeedPlusPlus.AttackSpeedToMultiply_LevelTwo
                        });
                        break;
                    case 3:
                        OnAttackSpeedPlusPlusUpgrade?.Invoke(this, new OnAttackSpeedPlusPlusUpgradeEventArgs {
                            attackSpeedToMultiply = upgradeData.attackSpeedPlusPlus.AttackSpeedToMultiply_LevelThree
                        });
                        break;
                }
                break;
            case PassiveType.ProjectileCountPlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnProjectileCountPlusPlusUpgrade?.Invoke(this, new OnProjectileCountPlusPlusUpgradeEventArgs {
                            projectileCountToAdd = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelOne
                        });
                        break;
                    case 2:
                        OnProjectileCountPlusPlusUpgrade?.Invoke(this, new OnProjectileCountPlusPlusUpgradeEventArgs {
                            projectileCountToAdd = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelTwo
                        });
                        break;
                    case 3:
                        OnProjectileCountPlusPlusUpgrade?.Invoke(this, new OnProjectileCountPlusPlusUpgradeEventArgs {
                            projectileCountToAdd = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelThree
                        });
                        break;
                }
                break;
            case PassiveType.PiercingPlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnPiercingPlusPlusUpgrade?.Invoke(this, new OnPiercingPlusPlusUpgradeEventArgs {
                            piercingToAdd = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelOne
                        });
                        break;
                    case 2:
                        OnPiercingPlusPlusUpgrade?.Invoke(this, new OnPiercingPlusPlusUpgradeEventArgs {
                            piercingToAdd = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelTwo
                        });
                        break;
                    case 3:
                        OnPiercingPlusPlusUpgrade?.Invoke(this, new OnPiercingPlusPlusUpgradeEventArgs {
                            piercingToAdd = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelThree
                        });
                        break;
                }
                break;
            case PassiveType.SizePlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnSizePlusPlusUpgrade?.Invoke(this, new OnSizePlusPlusUpgradeEventArgs {
                            sizeToMultiply = upgradeData.sizePlusPlus.SizeToMultiply_LevelOne
                        });
                        break;
                    case 2:
                        OnSizePlusPlusUpgrade?.Invoke(this, new OnSizePlusPlusUpgradeEventArgs {
                            sizeToMultiply = upgradeData.sizePlusPlus.SizeToMultiply_LevelTwo
                        });
                        break;
                    case 3:
                        OnSizePlusPlusUpgrade?.Invoke(this, new OnSizePlusPlusUpgradeEventArgs {
                            sizeToMultiply = upgradeData.sizePlusPlus.SizeToMultiply_LevelThree
                        });
                        break;
                }
                break;
            case PassiveType.AttackDamagePlusPlus:
                switch (levelNumber) {
                    case 1:
                        OnAttackDamagePlusPlusUpgrade?.Invoke(this, new OnAttackDamagePlusPlusUpgradeEventArgs {
                            attackDamageToMultiply = upgradeData.attackDamagePlusPlus.AttackDamageToMultiply_LevelOne
                        });
                        break;
                    case 2:
                        OnAttackDamagePlusPlusUpgrade?.Invoke(this, new OnAttackDamagePlusPlusUpgradeEventArgs {
                            attackDamageToMultiply = upgradeData.attackDamagePlusPlus.AttackDamageToMultiply_LevelTwo
                        });
                        break;
                    case 3:
                        OnAttackDamagePlusPlusUpgrade?.Invoke(this, new OnAttackDamagePlusPlusUpgradeEventArgs {
                            attackDamageToMultiply = upgradeData.attackDamagePlusPlus.AttackDamageToMultiply_LevelThree
                        });
                        break;
                }
                break;
        }
    }

    public int GetLevelOfPassive(PassiveType passiveType) {
        if(equippedPassiveAndLevelDictionary.TryGetValue(passiveType, out int level)) {
            return level;
        } else {
            return 0;
        }
    }

    [ContextMenu("Trigger Selected Upgrade")]
    private void Editor_TriggerUpgrade() {
        TriggerPassiveUpgrade(upgradeType);
    }
}
