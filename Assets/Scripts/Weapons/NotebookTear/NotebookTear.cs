using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookTear : PlayerWeapon {

    private UpgradeData_Runtime upgradeData;
    private WeaponData_Runtime weaponData;
    private GameObject notebookTearProjectilePrefab;
    private int numberOfProjectiles;
    private float cooldownTime;
    private float revolutionTime;
    private float animationTime;
    private float spawnAngle;
    private float currentSpawnAngle;

    protected override void Awake() {
        upgradeData = RuntimeGameData.Instance.GetUpgradeData();
        weaponData = RuntimeGameData.Instance.GetWeaponData();

        notebookTearProjectilePrefab = weaponData.notebookTear.notebookTearProjectilePrefab;
        numberOfProjectiles = weaponData.notebookTear.numberOfProjectiles;
        cooldownTime = weaponData.notebookTear.cooldownTime;
        revolutionTime = weaponData.notebookTear.revolutionTime;
        animationTime = weaponData.notebookTear.animationTime;
        
        spawnAngle = 360f / numberOfProjectiles;
        currentSpawnAngle = spawnAngle;

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
                    case PassiveType.ProjectileCountPlusPlus:
                        int projectileCountToAdd = default;
                        if(i == 1) {
                            projectileCountToAdd  = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelOne;
                        } else if(i == 2) {
                            projectileCountToAdd  = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelTwo;
                        } else if(i == 3) {
                            projectileCountToAdd  = upgradeData.projectileCountPlusPlus.ProjectileCountToAdd_LevelThree;
                        }
                        PassiveManager_OnProjectileCountPlusPlusUpgrade(null, new PassiveManager.OnProjectileCountPlusPlusUpgradeEventArgs {
                            projectileCountToAdd = projectileCountToAdd
                        });
                        break;
                    case PassiveType.PiercingPlusPlus:
                        int piercingToAdd = default;
                        if(i == 1) {
                            piercingToAdd  = upgradeData.piercingPlusPlus.PiercingToAdd_LevelOne;
                        } else if(i == 2) {
                            piercingToAdd  = upgradeData.piercingPlusPlus.PiercingToAdd_LevelTwo;
                        } else if(i == 3) {
                            piercingToAdd  = upgradeData.piercingPlusPlus.PiercingToAdd_LevelThree;
                        }
                        PassiveManager_OnPiercingPlusPlusUpgrade(null, new PassiveManager.OnPiercingPlusPlusUpgradeEventArgs {
                            piercingToAdd = piercingToAdd
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
        
    }

    private void Start() {
        StartCoroutine(ReleaseNotebookTearProjectiles());
    }

    private IEnumerator ReleaseNotebookTearProjectiles() {
        GameObject firstProjectileGameObject = null;    
        int currentNumberOfSpawnedProjectiles = 0;
        firstProjectileGameObject = ObjectPoolManager.GetObjectFromPool(PoolType.NotebookTearProjectile, 
            notebookTearProjectilePrefab, this.transform.position, Quaternion.identity);
        currentNumberOfSpawnedProjectiles++;
        firstProjectileGameObject.TryGetComponent(out NotebookTearProjectile firstNotebookTearProjectile);
        while (true) {
            if(firstNotebookTearProjectile.GetRevolutionAngle() >= currentSpawnAngle) {
                currentSpawnAngle += spawnAngle;
                ObjectPoolManager.GetObjectFromPool(PoolType.NotebookTearProjectile, notebookTearProjectilePrefab,
                    this.transform.position, Quaternion.identity);
                currentNumberOfSpawnedProjectiles++;
            }
            if(currentNumberOfSpawnedProjectiles == numberOfProjectiles) {
                currentSpawnAngle = spawnAngle;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(animationTime + revolutionTime + animationTime + cooldownTime);
        StartCoroutine(ReleaseNotebookTearProjectiles());
    }

    private void OnEnable() {
        PassiveManager.OnAttackSpeedPlusPlusUpgrade += PassiveManager_OnAttackSpeedPlusPlusUpgrade;
        PassiveManager.OnProjectileCountPlusPlusUpgrade += PassiveManager_OnProjectileCountPlusPlusUpgrade;
        PassiveManager.OnPiercingPlusPlusUpgrade += PassiveManager_OnPiercingPlusPlusUpgrade;
        PassiveManager.OnSizePlusPlusUpgrade += PassiveManager_OnSizePlusPlusUpgrade;
        PassiveManager.OnAttackDamagePlusPlusUpgrade += PassiveManager_OnAttackDamagePlusPlusUpgrade;
    }

    private void OnDisable() {
        PassiveManager.OnAttackSpeedPlusPlusUpgrade -= PassiveManager_OnAttackSpeedPlusPlusUpgrade;
        PassiveManager.OnProjectileCountPlusPlusUpgrade -= PassiveManager_OnProjectileCountPlusPlusUpgrade;
        PassiveManager.OnPiercingPlusPlusUpgrade -= PassiveManager_OnPiercingPlusPlusUpgrade;
        PassiveManager.OnSizePlusPlusUpgrade += PassiveManager_OnSizePlusPlusUpgrade;
        PassiveManager.OnAttackDamagePlusPlusUpgrade -= PassiveManager_OnAttackDamagePlusPlusUpgrade;
    }

    private void PassiveManager_OnAttackSpeedPlusPlusUpgrade(object sender, 
        PassiveManager.OnAttackSpeedPlusPlusUpgradeEventArgs e) {
        weaponData.notebookTear.attackHitCooldown /= e.attackSpeedToMultiply;
        weaponData.notebookTear.revolutionSpeed *= e.attackSpeedToMultiply;
        weaponData.notebookTear.rotationSpeed *= e.attackSpeedToMultiply;
    }

    private void PassiveManager_OnProjectileCountPlusPlusUpgrade(object sender, 
        PassiveManager.OnProjectileCountPlusPlusUpgradeEventArgs e) {
        numberOfProjectiles += e.projectileCountToAdd;
        spawnAngle = 360f / numberOfProjectiles;
        currentSpawnAngle = spawnAngle;

        weaponData.notebookTear.numberOfProjectiles += e.projectileCountToAdd;
    }

    private void PassiveManager_OnPiercingPlusPlusUpgrade(object sender, PassiveManager.OnPiercingPlusPlusUpgradeEventArgs e) {
        weaponData.notebookTear.piercing += e.piercingToAdd;
    }

    private void PassiveManager_OnSizePlusPlusUpgrade(object sender, PassiveManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.notebookTear.size *= e.sizeToMultiply;
        weaponData.notebookTear.targetDetectionRadius *= e.sizeToMultiply;
    }
    
    private void PassiveManager_OnAttackDamagePlusPlusUpgrade(object sender, 
        PassiveManager.OnAttackDamagePlusPlusUpgradeEventArgs e) {
        weaponData.notebookTear.attackDamage *= e.attackDamageToMultiply;
    }

}