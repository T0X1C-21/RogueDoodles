using UnityEngine;

public class InkSplash : PlayerWeapon {

    private WeaponData_Runtime weaponData;
    private GameObject inkSplashProjectilePrefab;

    protected override void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();

        attackCooldown = weaponData.inkSplash.attackCooldown;
        inkSplashProjectilePrefab = weaponData.inkSplash.inkSplashProjectilePrefab;

        playerTransform = RuntimeGameData.Instance.GetPlayerTargetTransform();
        enemyLayerMask = RuntimeGameData.Instance.GetEnemyData().enemyLayerMask;

        attackTimer = attackCooldown;
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
