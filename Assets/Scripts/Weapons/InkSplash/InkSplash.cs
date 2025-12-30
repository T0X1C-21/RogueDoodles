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
        UpgradeManager.OnAttackSpeedPlusPlusUpgrade += UpgradeManager_OnAttackSpeedPlusPlusUpgrade;
        UpgradeManager.OnSizePlusPlusUpgrade += UpgradeManager_OnSizePlusPlusUpgrade;
        UpgradeManager.OnAttackDamagePlusPlusUpgrade += UpgradeManager_OnAttackDamagePlusPlusUpgrade;
    }

    private void OnDisable() {
        UpgradeManager.OnAttackSpeedPlusPlusUpgrade -= UpgradeManager_OnAttackSpeedPlusPlusUpgrade;
        UpgradeManager.OnSizePlusPlusUpgrade -= UpgradeManager_OnSizePlusPlusUpgrade;
        UpgradeManager.OnAttackDamagePlusPlusUpgrade -= UpgradeManager_OnAttackDamagePlusPlusUpgrade;
    }

    private void UpgradeManager_OnAttackSpeedPlusPlusUpgrade(object sender, 
        UpgradeManager.OnAttackSpeedPlusPlusUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;

        weaponData.inkSplash.attackCooldown /= e.attackSpeedToMultiply;
    }
    
    private void UpgradeManager_OnSizePlusPlusUpgrade(object sender, UpgradeManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.inkSplash.size *= e.sizeToMultiply;
        weaponData.inkSplash.targetDetectionRadius *= e.sizeToMultiply;
    }
    
    private void UpgradeManager_OnAttackDamagePlusPlusUpgrade(object sender, 
        UpgradeManager.OnAttackDamagePlusPlusUpgradeEventArgs e) {
        weaponData.inkSplash.damage *= e.attackDamageToMultiply;
    }

}
