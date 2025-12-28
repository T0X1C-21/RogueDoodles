using System.Collections;
using UnityEngine;

public class CrayonMissileProjectile : Projectile {

    private Transform playerTransform;

    private void Awake() {
        WeaponData_Runtime weaponData = RuntimeGameData.Instance.GetWeaponData();
        
        moveSpeed = weaponData.crayonMissile.moveSpeed;
        damageAmount = weaponData.crayonMissile.damageAmount;
        targetDetectionRadius = weaponData.crayonMissile.targetDetectionRadius;
        autoDestroySelfTimer = weaponData.crayonMissile.autoDestroySelfTimer;
        targetLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;
        playerTransform = DataManager.Instance.GetPlayerTargetTransform();
    }

    private void OnEnable() {
        float xValue = Random.Range(-1f, 1f);
        float yValue = Random.Range(-1f, 1f);
        this.transform.position = playerTransform.position;
        moveDirection = new Vector3(xValue, yValue).normalized;
        StartCoroutine(AutoDestroySelf());
    }

    protected override void DestroySelf() {
        ObjectPoolManager.SetObjectBackToPool(PoolType.CrayonMissileProjectile, this.gameObject);
    }

    protected override void DetectAndDamageTarget() {
        Collider2D hit = Physics2D.OverlapCircle(this.transform.position, targetDetectionRadius, targetLayerMask);

        if(hit == null) {
            return;
        }

        if(hit.TryGetComponent(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(damageAmount);
            DestroySelf();
        }
    }

    protected override IEnumerator AutoDestroySelf() {
        yield return new WaitForSeconds(autoDestroySelfTimer);
        DestroySelf();
    }

}
