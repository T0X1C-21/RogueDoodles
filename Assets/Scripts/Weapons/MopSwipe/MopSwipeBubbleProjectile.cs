using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopSwipeBubbleProjectile : Projectile {

    private WeaponData_Runtime weaponData;
    private int piercing;
    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();

    private int currentPiercing;

    private void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();

        moveSpeed = weaponData.mopSwipe.moveSpeed;
        autoDestroySelfTimer = weaponData.mopSwipe.autoDestroySelfTimer;

        projectileHitType = ProjectileHitType.Enemy;
        targetLayerMask = RuntimeGameData.Instance.GetEnemyData().enemyLayerMask;
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

    private void OnEnable() {
        piercing = weaponData.mopSwipe.piercing;
        currentPiercing = piercing;
        this.transform.localScale = weaponData.mopSwipe.size;
        targetDetectionRadius = weaponData.mopSwipe.targetDetectionRadius;
        damageAmount = weaponData.mopSwipe.damageAmount;
    }

}
