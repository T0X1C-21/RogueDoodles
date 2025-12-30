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
    private float attackDamage;
    private float targetDetectionRadius;
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

        revolutionRadius = weaponData.notebookTear.revolutionRadius;
        revolutionTime = weaponData.notebookTear.revolutionTime;
        animationTime = weaponData.notebookTear.animationTime;
        fadeOutTime = weaponData.notebookTear.fadeOutTime;
        targetDetectionRadius = weaponData.notebookTear.targetDetectionRadius;
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

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, targetDetectionRadius, enemyLayerMask);

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
        UpgradeManager.OnAttackSpeedPlusPlusUpgrade += UpgradeManager_OnAttackSpeedPlusPlusUpgrade;
        attackCooldown = weaponData.notebookTear.attackHitCooldown;
        revolutionSpeed = weaponData.notebookTear.revolutionSpeed;
        rotationSpeed = weaponData.notebookTear.rotationSpeed;
        UpgradeManager.OnPiercingPlusPlusUpgrade += UpgradeManager_OnPiercingPlusPlusUpgrade;
        UpgradeManager.OnSizePlusPlusUpgrade += UpgradeManager_OnSizePlusPlusUpgrade;
        UpgradeManager.OnAttackDamagePlusPlusUpgrade += UpgradeManager_OnAttackDamagePlusPlusUpgrade;
        attackDamage = weaponData.notebookTear.attackDamage;

        StopAllCoroutines();
        StartCoroutine(RevolutionStartAnimation());
        StartCoroutine(EndRevolutionAutomatically());

        this.transform.localScale = weaponData.notebookTear.size;
    }

    private void OnDisable() {
        UpgradeManager.OnAttackSpeedPlusPlusUpgrade -= UpgradeManager_OnAttackSpeedPlusPlusUpgrade;
        UpgradeManager.OnPiercingPlusPlusUpgrade -= UpgradeManager_OnPiercingPlusPlusUpgrade;
        UpgradeManager.OnSizePlusPlusUpgrade -= UpgradeManager_OnSizePlusPlusUpgrade;
        UpgradeManager.OnAttackDamagePlusPlusUpgrade -= UpgradeManager_OnAttackDamagePlusPlusUpgrade;

        StopAllCoroutines();
        revolutionAngle = 0f;
    }

    private void UpgradeManager_OnAttackSpeedPlusPlusUpgrade(object sender, 
        UpgradeManager.OnAttackSpeedPlusPlusUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;
        revolutionSpeed *= e.attackSpeedToMultiply;
        rotationSpeed *= e.attackSpeedToMultiply;
    }

    private void UpgradeManager_OnPiercingPlusPlusUpgrade(object sender, 
        UpgradeManager.OnPiercingPlusPlusUpgradeEventArgs e) {
        piercing += e.piercingToAdd;
        currentPiercing += e.piercingToAdd;
    }
    
    private void UpgradeManager_OnSizePlusPlusUpgrade(object sender, UpgradeManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.notebookTear.size *= e.sizeToMultiply;
        this.transform.localScale = weaponData.notebookTear.size;

        targetDetectionRadius = weaponData.notebookTear.targetDetectionRadius;
    }
    
    private void UpgradeManager_OnAttackDamagePlusPlusUpgrade(object sender, 
        UpgradeManager.OnAttackDamagePlusPlusUpgradeEventArgs e) {
        attackDamage *= e.attackDamageToMultiply;
    }

}