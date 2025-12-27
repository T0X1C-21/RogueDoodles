using UnityEngine;

public class InkSplash : PlayerWeapon {

    private GameObject inkSplashProjectilePrefab;

    protected override void Awake() {
        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        attackCooldown = weaponData.inkSplash.attackCooldown;
        inkSplashProjectilePrefab = weaponData.inkSplash.inkSplashProjectilePrefab;

        playerTransform = DataManager.Instance.GetPlayerTargetTransform();
        enemyLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;

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
        UpgradeManager.OnGearCogUpgrade += UpgradeManager_OnGearCogUpgrade;
    }

    private void OnDisable() {
        UpgradeManager.OnGearCogUpgrade -= UpgradeManager_OnGearCogUpgrade;
    }

    private void UpgradeManager_OnGearCogUpgrade(object sender, UpgradeManager.OnGearCogUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedBuffAmount;
    }

}
