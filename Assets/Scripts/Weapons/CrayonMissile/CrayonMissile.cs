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

        weaponData.crayonMissile.attackCooldown /= e.attackSpeedToMultiply;
    }

    private void PassiveManager_OnSizePlusPlusUpgrade(object sender, PassiveManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.crayonMissile.size *= e.sizeToMultiply;
        weaponData.crayonMissile.targetDetectionRadius *= e.sizeToMultiply;
    }
    
    private void PassiveManager_OnAttackDamagePlusPlusUpgrade(object sender, 
        PassiveManager.OnAttackDamagePlusPlusUpgradeEventArgs e) {
        weaponData.crayonMissile.damageAmount *= e.attackDamageToMultiply;
    }

}
