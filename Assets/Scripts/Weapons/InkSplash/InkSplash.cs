using System.Collections.Generic;
using UnityEngine;

public class InkSplash : PlayerWeapon {

    private UpgradeData_Runtime upgradeData;
    private WeaponData_Runtime weaponData;
    private GameObject inkSplashProjectilePrefab;

    protected override void Awake() {
        upgradeData = RuntimeGameData.Instance.GetUpgradeData();
        weaponData = RuntimeGameData.Instance.GetWeaponData();

        attackCooldown = weaponData.inkSplash.attackCooldown;
        inkSplashProjectilePrefab = weaponData.inkSplash.inkSplashProjectilePrefab;

        playerTransform = RuntimeGameData.Instance.GetPlayerTargetTransform();
        enemyLayerMask = RuntimeGameData.Instance.GetEnemyData().enemyLayerMask;

        attackTimer = attackCooldown;

        AddPassivesToWeapon();
    }

    private void AddPassivesToWeapon() {
        Dictionary<PassiveType, int> passivesDicationary = PassiveManager.Instance.GetEquippedPassivesAndLevelsDictionary();
        foreach(PassiveType passiveType in passivesDicationary.Keys) {
            for(int i = 1; i <= passivesDicationary[passiveType]; i++) {
                switch (passiveType) {
                    case PassiveType.AttackSpeedPlusPlus:
                        float attackSpeedToMultiply = default;
                        if(i == 1) {
                           attackSpeedToMultiply  = upgradeData.attackSpeedPlusPlus.AttackSpeedToMultiply_LevelOne;
                        } else if(i == 2) {
                            attackSpeedToMultiply  = upgradeData.attackSpeedPlusPlus.AttackSpeedToMultiply_LevelTwo;
                        } else if(i == 3) {
                            attackSpeedToMultiply  = upgradeData.attackSpeedPlusPlus.AttackSpeedToMultiply_LevelThree;
                        }
                        PassiveManager_OnAttackSpeedPlusPlusUpgrade(null, new PassiveManager.OnAttackSpeedPlusPlusUpgradeEventArgs {
                            attackSpeedToMultiply = attackSpeedToMultiply
                        });
                        break;
                    case PassiveType.SizePlusPlus:
                        float sizeToMultiply = default;
                        if(i == 1) {
                            sizeToMultiply  = upgradeData.sizePlusPlus.SizeToMultiply_LevelOne;
                        } else if(i == 2) {
                            sizeToMultiply  = upgradeData.sizePlusPlus.SizeToMultiply_LevelTwo;
                        } else if(i == 3) {
                            sizeToMultiply  = upgradeData.sizePlusPlus.SizeToMultiply_LevelThree;
                        }
                        PassiveManager_OnSizePlusPlusUpgrade(null, new PassiveManager.OnSizePlusPlusUpgradeEventArgs {
                            sizeToMultiply = sizeToMultiply
                        });
                        break;
                    case PassiveType.AttackDamagePlusPlus:
                        float attackDamageToMultiply = default;
                        if(i == 1) {
                            attackDamageToMultiply = upgradeData.attackDamagePlusPlus.AttackDamageToMultiply_LevelOne;
                        } else if(i == 2) {
                            attackDamageToMultiply = upgradeData.attackDamagePlusPlus.AttackDamageToMultiply_LevelTwo;
                        } else if(i == 3) {
                            attackDamageToMultiply = upgradeData.attackDamagePlusPlus.AttackDamageToMultiply_LevelThree;
                        }
                        PassiveManager_OnAttackDamagePlusPlusUpgrade(null, new PassiveManager.OnAttackDamagePlusPlusUpgradeEventArgs {
                            attackDamageToMultiply = attackDamageToMultiply
                        });
                        break;
                }
            }
        }
    }

    protected override void Update() {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f) {
            Attack();
            attackTimer = attackCooldown;
        }
    }

    protected override void Attack() {
        ObjectPoolManager.GetObjectFromPool(PoolType.InkSplashProjectile, inkSplashProjectilePrefab,
            playerTransform.position, Quaternion.identity);
    }

    private void OnEnable() {
        PassiveManager.OnAttackSpeedPlusPlusUpgrade += PassiveManager_OnAttackSpeedPlusPlusUpgrade;
        PassiveManager.OnSizePlusPlusUpgrade += PassiveManager_OnSizePlusPlusUpgrade;
        PassiveManager.OnAttackDamagePlusPlusUpgrade += PassiveManager_OnAttackDamagePlusPlusUpgrade;
    }

    private void OnDisable() {
        PassiveManager.OnAttackSpeedPlusPlusUpgrade -= PassiveManager_OnAttackSpeedPlusPlusUpgrade;
        PassiveManager.OnSizePlusPlusUpgrade -= PassiveManager_OnSizePlusPlusUpgrade;
        PassiveManager.OnAttackDamagePlusPlusUpgrade -= PassiveManager_OnAttackDamagePlusPlusUpgrade;
    }

    private void PassiveManager_OnAttackSpeedPlusPlusUpgrade(object sender, 
        PassiveManager.OnAttackSpeedPlusPlusUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;

        weaponData.inkSplash.attackCooldown /= e.attackSpeedToMultiply;
    }
    
    private void PassiveManager_OnSizePlusPlusUpgrade(object sender, PassiveManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.inkSplash.size *= e.sizeToMultiply;
        weaponData.inkSplash.targetDetectionRadius *= e.sizeToMultiply;
    }
    
    private void PassiveManager_OnAttackDamagePlusPlusUpgrade(object sender, 
        PassiveManager.OnAttackDamagePlusPlusUpgradeEventArgs e) {
        weaponData.inkSplash.damage *= e.attackDamageToMultiply;
    }

}
