using UnityEngine;

public class CrayonMissile : PlayerWeapon {

    private GameObject crayonMissileProjectile;

    protected override void Awake() {
        WeaponData weaponData = DataManager.Instance.GetWeaponData();

        crayonMissileProjectile = weaponData.crayonMissile.crayonMissileProjectilePrefab;
        attackCooldown = weaponData.crayonMissile.attackCooldown;
        
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
        ObjectPoolManager.GetObjectFromPool(PoolType.CrayonMissileProjectile, crayonMissileProjectile,
            this.transform.position, Quaternion.identity);
    }

    private void OnEnable() {
        UpgradeManager.OnGearCogUpgrade += UpgradeManager_OnGearCogUpgrade;
    }

    private void OnDisable() {
        UpgradeManager.OnGearCogUpgrade -= UpgradeManager_OnGearCogUpgrade;
    }

    private void UpgradeManager_OnGearCogUpgrade(object sender, UpgradeManager.OnGearCogUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;
    }

}
