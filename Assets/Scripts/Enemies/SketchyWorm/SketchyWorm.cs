using System.Collections;
using UnityEngine;

public class SketchyWorm : Enemy {
    
    private float timeToStayHidden;
    private float timeToStayHiddenOffset;
    private float timeToDisappear;
    private float timeToDisappearOffset;

    protected override void Awake() {
        base.Awake();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        attackCooldown = enemyData.sketchyWorm.attackCooldown;
        attackDamage = enemyData.sketchyWorm.attackDamage;
        attackPointOffsetMultiplier = enemyData.sketchyWorm.attackPointOffsetMultiplier;

        timeToStayHidden = enemyData.sketchyWorm.timeToStayHidden;
        timeToStayHiddenOffset = enemyData.sketchyWorm.timeToStayHiddenOffset;
        timeToDisappear = enemyData.sketchyWorm.timeToDisappear;
        timeToDisappearOffset = enemyData.sketchyWorm.timeToDisappearOffset;
    }

    private void OnEnable() {
        this.transform.position = new Vector3(999f, 999f);
        StartCoroutine(PopUpNearPlayer());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    protected override void Update() {
        
    }

    private IEnumerator PopUpNearPlayer() {
        float waitTime = timeToStayHidden + Random.Range(-timeToStayHiddenOffset, timeToStayHiddenOffset);
        yield return new WaitForSeconds(waitTime);

        float randomAngle = Random.Range(0f, 2 * Mathf.PI);
        float xValue = attackPointOffsetMultiplier * Mathf.Cos(randomAngle);
        float yValue = attackPointOffsetMultiplier * Mathf.Sin(randomAngle);

        Vector3 targetPosition = playerTarget.position + new Vector3(xValue, yValue);
        this.transform.position = targetPosition;

        StartCoroutine(ShootProjectile());
    }

    private IEnumerator ShootProjectile() {
        yield return new WaitForSeconds(attackCooldown);

        ObjectPoolManager.GetObjectFromPool(PoolType.SketchyWormProjectile, 
            DataManager.Instance.GetEnemyData().sketchyWorm.sketchyWormProjectilePrefab,
            this.transform.position, Quaternion.identity);

        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear() {
        float waitTime = timeToDisappear + Random.Range(-timeToDisappearOffset, timeToDisappearOffset);
        yield return new WaitForSeconds(waitTime);

        // Faking despawn
        this.transform.position = new Vector3(999f, 999f);

        StartCoroutine(PopUpNearPlayer());
    }

    bool doOnce = false;
    float xValue;
    float yValue;

    protected override void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }

        base.OnDrawGizmos();

        if (!doOnce) {
            float randomAngle = Random.Range(0f, 2 * Mathf.PI);
            xValue = attackPointOffsetMultiplier * Mathf.Cos(randomAngle);
            yValue = attackPointOffsetMultiplier * Mathf.Sin(randomAngle);
            doOnce = true;
            StartCoroutine(ResetDoOnce());
        }
            Vector3 targetPosition = playerTarget.position + new Vector3(xValue, yValue);
            Gizmos.DrawWireSphere(targetPosition, 0.3f);
    }

    private IEnumerator ResetDoOnce() {
        yield return new WaitForSeconds(3f);
        doOnce = true;
    }

    // 1. take time to hide
    // 2. pop up near player
    // 3. wait for some more time
    // 4. shoot projectile
    // 5. wait for some more time
    // 6. disappear

}
