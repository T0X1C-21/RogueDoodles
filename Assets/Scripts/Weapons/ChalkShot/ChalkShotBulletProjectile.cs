using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChalkShotBulletProjectile : Projectile {
    
    private WeaponData_Runtime weaponData;
    private int piercing;
    private HashSet<Collider2D> damagedEnemiesHashSet = new HashSet<Collider2D>();

    private void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();
        moveSpeed = weaponData.chalkShot.moveSpeed;
        damageAmount = weaponData.chalkShot.damageAmount;
        targetDetectionRadius = weaponData.chalkShot.targetDetectionRadius;
        autoDestroySelfTimer = weaponData.chalkShot.autoDestroySelfTimer;
        piercing = weaponData.chalkShot.piercing;
        projectileHitType = ProjectileHitType.Enemy;
        targetLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;
    }

    private void OnEnable() {
        piercing = weaponData.chalkShot.piercing;
        damagedEnemiesHashSet.Clear();
    }

    protected override IEnumerator AutoDestroySelf() {
        yield return new WaitForSeconds(autoDestroySelfTimer);
        DestroySelf();
    }

    protected override void DestroySelf() {
        ObjectPoolManager.SetObjectBackToPool(PoolType.ChalkShotProjectile, this.gameObject);
    }

    protected override void DetectAndDamageTarget() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, targetDetectionRadius,
            targetLayerMask);
        if(piercing <= 0) {
            DestroySelf();
        }

        foreach(Collider2D hit in hits) {
            if (damagedEnemiesHashSet.Contains(hit)) {
                continue;
            }

            if(hit.TryGetComponent(out EnemyHealth enemyHealth) && piercing > 0) {
                piercing -= 1;
                enemyHealth.TakeDamage(damageAmount);
                damagedEnemiesHashSet.Add(hit);
                if(piercing <= 0) {
                    DestroySelf();
                }
            }
        }
    }

    public void InitializeChalkShotBullet(Vector3 direction) {
        targetPosition = direction;
        currentPosition = this.transform.position;
        moveDirection = (targetPosition - currentPosition).normalized;

        StartCoroutine(AutoDestroySelf());
    }

}
