using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopSwipeBubbleProjectile : Projectile {

    private int piercing;
    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();

    private int currentPiercing;

    private void Awake() {
        WeaponData_Runtime weaponData = RuntimeGameData.Instance.GetWeaponData();

        moveSpeed = weaponData.mopSwipe.moveSpeed;
        damageAmount = weaponData.mopSwipe.damageAmount;
        targetDetectionRadius = weaponData.mopSwipe.targetDetectionRadius;
        autoDestroySelfTimer = weaponData.mopSwipe.autoDestroySelfTimer;
        piercing = weaponData.mopSwipe.piercing;
        currentPiercing = piercing;

        projectileHitType = ProjectileHitType.Enemy;
        targetLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;
    }

    public void InitializeBubble(Vector3 direction) {
        moveDirection = direction;
        StartCoroutine(AutoDestroySelf());
    }

    protected override IEnumerator AutoDestroySelf() {
        yield return new WaitForSeconds(autoDestroySelfTimer);
        DestroySelf();
    }

    protected override void DestroySelf() {
        currentPiercing = piercing;
        attackHitsHashSet.Clear();
        ObjectPoolManager.SetObjectBackToPool(PoolType.MopSwipeBubbleProjectile, this.gameObject);
    }

    protected override void DetectAndDamageTarget() {
        if(currentPiercing <= 0) {
            DestroySelf();
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, targetDetectionRadius, targetLayerMask);

        if(hits == null) {
            return;
        }

        foreach(Collider2D hit in hits) {
            if (attackHitsHashSet.Contains(hit)) {
                continue;
            }
            attackHitsHashSet.Add(hit);
            if(hit.TryGetComponent(out EnemyHealth enemyHealth)) {
                currentPiercing--;
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }

}
