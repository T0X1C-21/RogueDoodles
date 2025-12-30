using UnityEngine;

public class CrayonMissile : PlayerWeapon {

    private WeaponData_Runtime weaponData;
    private GameObject crayonMissileProjectile;

    protected override void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();

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

        weaponData.crayonMissile.attackCooldown /= e.attackSpeedToMultiply;
    }

    private void UpgradeManager_OnSizePlusPlusUpgrade(object sender, UpgradeManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.crayonMissile.size *= e.sizeToMultiply;
        weaponData.crayonMissile.targetDetectionRadius *= e.sizeToMultiply;
    }
    
    private void UpgradeManager_OnAttackDamagePlusPlusUpgrade(object sender, 
        UpgradeManager.OnAttackDamagePlusPlusUpgradeEventArgs e) {
        weaponData.crayonMissile.damageAmount *= e.attackDamageToMultiply;
    }

}
