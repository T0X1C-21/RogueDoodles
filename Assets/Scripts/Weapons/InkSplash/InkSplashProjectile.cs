using System.Collections;
using UnityEngine;

public class InkSplashProjectile : Projectile, IPoolable {

    private float playerDetectionRadius;
    private float minimumAttackRange;
    private float maximumAttackRange;
    private float spawnAnimationDuration;
    private AnimationCurve heightAnimationCurve;

    private void Awake() {
        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        damageAmount = weaponData.inkSplash.damage;
        playerDetectionRadius = weaponData.inkSplash.playerDetectionRadius;
        minimumAttackRange = weaponData.inkSplash.minimumAttackRange;
        maximumAttackRange = weaponData.inkSplash.maximumAttackRange;
        spawnAnimationDuration = weaponData.inkSplash.spawnAnimationDuration;
        heightAnimationCurve = weaponData.inkSplash.heightAnimationCurve;
        targetLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;

        StartCoroutine(SpawnAnimation());
    }

    private IEnumerator SpawnAnimation() {
        float startPositionX = this.transform.position.x;
        float startPositionY = this.transform.position.y;
        int randomNumber = Random.Range(-1, 1);
        int xDirection = (randomNumber == -1) ? -1 : 1;
        randomNumber = Random.Range(-1, 1);
        int yDirection = (randomNumber == -1) ? -1 : 1;
        float targetPositionX = (Random.Range(minimumAttackRange, maximumAttackRange) * xDirection) + startPositionX;
        float targetPositionY = (Random.Range(minimumAttackRange, maximumAttackRange) * yDirection) + startPositionY;
        float t = 0f;
        while(t < 1) {
            t += Time.deltaTime / spawnAnimationDuration;
            float animationX = Mathf.Lerp(startPositionX, targetPositionX, t);
            float animationY = Mathf.LerpUnclamped(startPositionY, targetPositionY, heightAnimationCurve.Evaluate(t));
            this.transform.position = new Vector3(animationX, animationY);
            yield return null;
        }
    }

    protected override void DetectTarget() {
        Collider2D hit = Physics2D.OverlapCircle(this.transform.position, playerDetectionRadius, targetLayerMask);

        if(hit == null) {
            return;
        }

        if(hit.TryGetComponent(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(damageAmount);
            DestroySelf();
        }
    }

    protected override void Update() {
        DetectTarget();
    }

    protected override void DestroySelf() {
        ObjectPoolManager.SetObjectBackToPool(PoolType.InkSplashProjectile, this.gameObject);
    }

    public void OnSpawnFromPool() {
        StartCoroutine(SpawnAnimation());
    }

    public void OnReturnToPool() {
        StopAllCoroutines();
    }
}
