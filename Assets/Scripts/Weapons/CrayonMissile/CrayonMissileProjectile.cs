using System.Collections;
using UnityEngine;

public class CrayonMissileProjectile : Projectile {

    private WeaponData_Runtime weaponData;
    private Transform playerTransform;

    private void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();
        
        moveSpeed = weaponData.crayonMissile.moveSpeed;
        autoDestroySelfTimer = weaponData.crayonMissile.autoDestroySelfTimer;
        targetLayerMask = RuntimeGameData.Instance.GetEnemyData().enemyLayerMask;
        playerTransform = RuntimeGameData.Instance.GetPlayerTargetTransform();
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

    private void OnEnable() {
        float xValue = Random.Range(-1f, 1f);
        float yValue = Random.Range(-1f, 1f);
        this.transform.position = playerTransform.position;
        moveDirection = new Vector3(xValue, yValue).normalized;
        StartCoroutine(AutoDestroySelf());

        this.transform.localScale = weaponData.crayonMissile.size;
        targetDetectionRadius = weaponData.crayonMissile.targetDetectionRadius;
        damageAmount = weaponData.crayonMissile.damageAmount;
    }

}
