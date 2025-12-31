using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager> {

    [SerializeField] private PassiveType upgradeType;

    private Dictionary<PassiveType, int> upgradeTypeAndLevelDictionary = new Dictionary<PassiveType, int>();
    private UpgradeData_Runtime upgradeData;
    private UserInterfaceData_Runtime userInterfaceData;

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

    private HashSet<UserInterfaceUpgradePool> userInterfaceUpgradePoolHashSet = new HashSet<UserInterfaceUpgradePool>();
    private Dictionary<int, UserInterfaceUpgradePool> upgradeLevelsDictionary = 
        new Dictionary<int, UserInterfaceUpgradePool>();

    protected override void Awake() {
        base.Awake();

        upgradeData = RuntimeGameData.Instance.GetUpgradeData();
        userInterfaceData = RuntimeGameData.Instance.GetUserInterfaceData();

        

        upgradeLevelsDictionary.Add(0, UserInterfaceUpgradePool.InkSplash);

        ResetUpgradePool();
    }

    public void ResetUpgradePool() {
        for (int i = 0; i < Enum.GetValues(typeof(UserInterfaceUpgradePool)).Length; i++) {
            Array array = Enum.GetValues(typeof(UserInterfaceUpgradePool));
            userInterfaceUpgradePoolHashSet.Add((UserInterfaceUpgradePool) array.GetValue(i));
        }
    }

    public int NumberOfItemsInUpgradePool() {
        return userInterfaceUpgradePoolHashSet.Count;
    }

    public bool CheckItemInUpgradePool(UserInterfaceUpgradePool poolItem) {
        return userInterfaceUpgradePoolHashSet.Contains(poolItem);
    }

    public bool AddToUpgradePool(UserInterfaceUpgradePool poolItem) {
        return userInterfaceUpgradePoolHashSet.Add(poolItem);
    }

    public bool RemoveFromUpgradePool(UserInterfaceUpgradePool poolItem) {
        return userInterfaceUpgradePoolHashSet.Remove(poolItem);
    }

    public void TriggerUpgrade(PassiveType passiveType) {
        int levelNumber;
        if (!upgradeTypeAndLevelDictionary.ContainsKey(passiveType)) {
            upgradeTypeAndLevelDictionary.Add(passiveType, 1);
            levelNumber = upgradeTypeAndLevelDictionary[passiveType];
        } else {
            if (upgradeTypeAndLevelDictionary[passiveType] == 3) {
                Debug.LogWarning($"Cannot upgrade {passiveType.ToString()} past 3!");
                return;
            }
            upgradeTypeAndLevelDictionary[passiveType] += 1;
            levelNumber = upgradeTypeAndLevelDictionary[passiveType];
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

    [ContextMenu("Trigger Selected Upgrade")]
    private void Editor_TriggerUpgrade() {
        TriggerUpgrade(upgradeType);
    }

}
