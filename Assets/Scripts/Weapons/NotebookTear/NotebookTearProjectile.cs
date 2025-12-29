using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookTearProjectile : MonoBehaviour {
    
    private float revolutionSpeed;
    private float revolutionRadius;
    private float rotationSpeed;
    private float revolutionTime;
    private float animationTime;
    private float fadeOutTime;
    private float attackCooldown;
    private int attackDamage;
    private float attackRadius;
    private int piercing;

    private WeaponData_Runtime weaponData;
    private LayerMask enemyLayerMask;
    private int currentPiercing;
    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();
    private HashSet<Collider2D> cannotAttackHitsHashSet = new HashSet<Collider2D>();
    private bool canAttack;
    private Transform playerTransform;
    private float revolutionAngle;
    private SpriteRenderer spriteRenderer;
    private Coroutine attackCoroutine;

    private void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();

        revolutionSpeed = weaponData.notebookTear.revolutionSpeed;
        revolutionRadius = weaponData.notebookTear.revolutionRadius;
        rotationSpeed = weaponData.notebookTear.rotationSpeed;
        revolutionTime = weaponData.notebookTear.revolutionTime;
        animationTime = weaponData.notebookTear.animationTime;
        fadeOutTime = weaponData.notebookTear.fadeOutTime;
        attackCooldown = weaponData.notebookTear.attackHitCooldown;
        attackDamage = weaponData.notebookTear.attackDamage;
        attackRadius = weaponData.notebookTear.attackRadius;
        piercing = weaponData.notebookTear.piercing;

        currentPiercing = piercing;
        enemyLayerMask = RuntimeGameData.Instance.GetEnemyData().enemyLayerMask;
        playerTransform = RuntimeGameData.Instance.GetPlayerTargetTransform();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() {
        if (!canAttack) {
            return;
        }

        revolutionAngle += Time.deltaTime * revolutionSpeed;
        float xValue = revolutionRadius * Mathf.Cos(revolutionAngle);
        float yValue = revolutionRadius * Mathf.Sin(revolutionAngle);
        Vector3 targetPosition = playerTransform.position + new Vector3(xValue, yValue);
        this.transform.position = targetPosition;

        this.transform.Rotate(new Vector3(0f, 0f, 1f), rotationSpeed);

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, attackRadius, enemyLayerMask);

        if (hits == null) {
            return;
        }

        foreach (Collider2D hit in hits) {
            if (cannotAttackHitsHashSet.Contains(hit)) {
                continue;
            }

            if(currentPiercing > 0 && !attackHitsHashSet.Contains(hit)) {
                currentPiercing--;
                attackHitsHashSet.Add(hit);
                cannotAttackHitsHashSet.Add(hit);
            }
        }

        if(attackCoroutine == null) {
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack() {
        yield return new WaitForSeconds(attackCooldown);
        Debug.Log($"Detecting Enemies!");

        foreach (Collider2D hit in attackHitsHashSet) {
            if (hit.TryGetComponent(out EnemyHealth enemyHealth)) {
                Debug.Log($"Hit {hit.name}");
                enemyHealth.TakeDamage(attackDamage);
            }
        }

        if (currentPiercing <= 0) {
            currentPiercing = piercing;
            canAttack = false;
            StartCoroutine(RevolutionEndAnimation());
        }

        attackHitsHashSet.Clear();
        attackCoroutine = null;
    }

    private IEnumerator RevolutionStartAnimation() {
        spriteRenderer.DOFade(1f, fadeOutTime);

        float t = 0f;
        while (t <= animationTime) {
            t += Time.deltaTime / animationTime;
            float radius = Mathf.Lerp(0f, revolutionRadius, t);
            revolutionAngle += Time.deltaTime * revolutionSpeed;
            float xValue = radius * Mathf.Cos(revolutionAngle);
            float yValue = radius * Mathf.Sin(revolutionAngle);
            Vector3 targetPosition = playerTransform.position + new Vector3(xValue, yValue);
            this.transform.position = targetPosition;

            this.transform.Rotate(new Vector3(0f, 0f, 1f), rotationSpeed);

            yield return null;
        }
        canAttack = true;
    }

    private IEnumerator RevolutionEndAnimation() {
        cannotAttackHitsHashSet.Clear();

        canAttack = false;

        spriteRenderer.DOFade(0f, fadeOutTime);

        float t = 0f;
        while (t <= animationTime) {
            t += Time.deltaTime / animationTime;
            float radius = Mathf.Lerp(revolutionRadius, 0, t);
            revolutionAngle += Time.deltaTime * revolutionSpeed;
            float xValue = radius * Mathf.Cos(revolutionAngle);
            float yValue = radius * Mathf.Sin(revolutionAngle);
            Vector3 targetPosition = playerTransform.position + new Vector3(xValue, yValue);
            this.transform.position = targetPosition;

            this.transform.Rotate(new Vector3(0f, 0f, 1f), rotationSpeed);

            yield return null;
        }

        yield return new WaitForSeconds(revolutionTime);
        ObjectPoolManager.SetObjectBackToPool(PoolType.NotebookTearProjectile, this.gameObject);
    }

    private IEnumerator EndRevolutionAutomatically() {
        yield return new WaitForSeconds(animationTime + revolutionTime);
        StartCoroutine(RevolutionEndAnimation());
    }

    public float GetRevolutionAngle() {
        Vector3 projectileDirectionNormalized = (this.transform.position - playerTransform.position).normalized;
        float angle = Mathf.Atan2(projectileDirectionNormalized.y, projectileDirectionNormalized.x) * Mathf.Rad2Deg;
        if(angle < 0f) {
            return 180f + Mathf.Abs(angle + 180f);
        } else {
            return angle;
        }
    }

    private void OnEnable() {
        UpgradeManager.OnGearCogUpgrade += UpgradeManager_OnGearCogUpgrade;
        attackCooldown = weaponData.notebookTear.attackHitCooldown;
        revolutionSpeed = weaponData.notebookTear.revolutionSpeed;
        rotationSpeed = weaponData.notebookTear.rotationSpeed;
        UpgradeManager.OnRulerEdgeUpgrade += UpgradeManager_OnRulerEdgeUpgrade;

        StopAllCoroutines();
        StartCoroutine(RevolutionStartAnimation());
        StartCoroutine(EndRevolutionAutomatically());
    }
    private void OnDisable() {
        UpgradeManager.OnGearCogUpgrade -= UpgradeManager_OnGearCogUpgrade;
        UpgradeManager.OnRulerEdgeUpgrade -= UpgradeManager_OnRulerEdgeUpgrade;

        StopAllCoroutines();
        revolutionAngle = 0f;
    }

    private void UpgradeManager_OnGearCogUpgrade(object sender, UpgradeManager.OnGearCogUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;
        revolutionSpeed *= e.attackSpeedToMultiply;
        rotationSpeed *= e.attackSpeedToMultiply;
    }

    private void UpgradeManager_OnRulerEdgeUpgrade(object sender, UpgradeManager.OnRulerEdgeUpgradeEventArgs e) {
        piercing += e.piercingToAdd;
        currentPiercing += e.piercingToAdd;
    }


}