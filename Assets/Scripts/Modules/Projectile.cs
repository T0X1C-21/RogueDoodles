using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    protected float moveSpeed;
    protected float damageAmount;
    protected Vector3 targetPosition;
    protected Vector3 currentPosition;
    protected Vector3 moveDirection;
    protected float targetDetectionRadius;
    protected float autoDestroySelfTimer;
    protected ProjectileHitType projectileHitType;
    protected LayerMask targetLayerMask;

    protected virtual void Update() {
        MoveTowardsTarget();
        DetectAndDamageTarget();
    }

    private void MoveTowardsTarget() {
        this.transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    protected abstract void DetectAndDamageTarget();
    protected abstract void DestroySelf();

    protected virtual IEnumerator AutoDestroySelf() {
        Debug.LogWarning("Implement AutoDestroySelf() in child Projectile class!");
        yield return null;
    }

    //private void DetectTarget() {
    //    if(projectileHitType == ProjectileHitType.Enemy) {
    //        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, targetDetectionRadius);
            
    //        foreach(Collider2D hit in hits) {
    //            hit.TryGetComponent(out EnemyHealth enemyHealth);
    //            enemyHealth.TakeDamage(damageAmount);
    //        }
    //    } else if(projectileHitType == ProjectileHitType.Player) {
    //        
    //    }

    //}

}
