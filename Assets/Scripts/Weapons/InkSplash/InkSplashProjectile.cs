using DG.Tweening;
using System.Collections;
using UnityEngine;

public class InkSplashProjectile : Projectile, IPoolable {

    private float playerDetectionRadius;
    private float minimumAttackRange;
    private float maximumAttackRange;
    private float spawnAnimationDuration;
    private Sprite inkSplashFlamesSprite;
    private SpriteRenderer spriteRenderer;
    private float flamesDuration;
    private bool canAttack;
    private float attackCooldown;
    private float attackTimer;

    private void Awake() {
        WeaponData_Runtime weaponData = RuntimeGameData.Instance.GetWeaponData();
        damageAmount = weaponData.inkSplash.damage;
        playerDetectionRadius = weaponData.inkSplash.enemyDetectionRadius;
        minimumAttackRange = weaponData.inkSplash.minimumAttackRange;
        maximumAttackRange = weaponData.inkSplash.maximumAttackRange;
        spawnAnimationDuration = weaponData.inkSplash.spawnAnimationDuration;
        inkSplashFlamesSprite = weaponData.inkSplash.inkSplashFlamesSprite;
        flamesDuration = weaponData.inkSplash.flamesDuration;
        attackCooldown = weaponData.inkSplash.attackCooldown;
        attackTimer = attackCooldown;
        targetLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;

        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        // Temporary
        this.transform.localScale = Vector3.one;
        SpawnAnimation();
    }

    private void SpawnAnimation() {
        Vector3 startPosition = this.transform.position;
        int randomNumber = Random.Range(-1, 1);
        int xDirection = (randomNumber == -1) ? -1 : 1;
        randomNumber = Random.Range(-1, 1);
        int yDirection = (randomNumber == -1) ? -1 : 1;
        float targetPositionX = (Random.Range(minimumAttackRange, maximumAttackRange) * xDirection) + startPosition.x;
        float targetPositionY = (Random.Range(minimumAttackRange, maximumAttackRange) * yDirection) + startPosition.y;
        Vector3 targetPosition = new Vector3(targetPositionX, targetPositionY);
        this.transform.DOMove(targetPosition, spawnAnimationDuration).OnComplete(() => {
            StartCoroutine(InkSplashFlames());
        });
    }

    private IEnumerator InkSplashFlames() {
        canAttack = true;
        spriteRenderer.sprite = inkSplashFlamesSprite;
        // Temporary
        this.transform.localScale = Vector3.one * 5;
        yield return new WaitForSeconds(flamesDuration);
        DestroySelf();
    }

    protected override void DetectAndDamageTarget() {
        Collider2D hit = Physics2D.OverlapCircle(this.transform.position, playerDetectionRadius, targetLayerMask);

        if(hit == null) {
            return;
        }

        if(hit.TryGetComponent(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(damageAmount);
        }
    }

    protected override void Update() {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f && canAttack) {
            attackTimer = attackCooldown;
            DetectAndDamageTarget();
        }
    }

    protected override void DestroySelf() {
        ObjectPoolManager.SetObjectBackToPool(PoolType.InkSplashProjectile, this.gameObject);
    }

    public void OnSpawnFromPool() {
        SpawnAnimation();
    }

    public void OnReturnToPool() {
        canAttack = false;
        // Temporary
        this.transform.localScale = Vector3.one;
    }
}
