using System.Collections;
using UnityEngine;

public class SketchyWormProjectile : Projectile {

    private void Awake() {
        EnemyData_Runtime enemyData = RuntimeGameData.Instance.GetEnemyData();

        moveSpeed = enemyData.sketchyWorm.moveSpeed;
        damageAmount = enemyData.sketchyWorm.attackDamage;
        targetDetectionRadius = enemyData.sketchyWorm.targetDetectionRadius;
        autoDestroySelfTimer = enemyData.sketchyWorm.autoDestroySelfTimer;
        projectileHitType = ProjectileHitType.Player;
        targetLayerMask = RuntimeGameData.Instance.GetPlayerData().playerLayerMask;
    }

    private void OnEnable() {
        targetPosition = RuntimeGameData.Instance.GetPlayerTargetTransform().position;
        currentPosition = this.transform.position;
        moveDirection = (targetPosition - currentPosition).normalized;

        StartCoroutine(AutoDestroySelf());
    }

    protected override void DetectAndDamageTarget() {
        Collider2D hit = Physics2D.OverlapCircle(this.transform.position, targetDetectionRadius, 
            targetLayerMask);
        if(hit == null) {
            return;
        }

        if(hit.TryGetComponent(out PlayerHealth playerHealth)) {
            playerHealth.TakeDamage(damageAmount);
            DestroySelf();
        }
    }

    protected override void DestroySelf() {
        ObjectPoolManager.SetObjectBackToPool(PoolType.SketchyWormProjectile, this.gameObject);
    }

    protected override IEnumerator AutoDestroySelf() {
        yield return new WaitForSeconds(autoDestroySelfTimer);
        DestroySelf();
    }

}
