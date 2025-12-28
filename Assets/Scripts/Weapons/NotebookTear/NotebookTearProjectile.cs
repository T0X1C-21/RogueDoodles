using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookTearProjectile : MonoBehaviour {
    
    private static float revolutionSpeed;
    private float revolutionRadius;
    private static float rotationSpeed;
    private float revolutionTime;
    private float animationTime;
    private float fadeOutTime;
    private static float attackCooldown;
    private int attackDamage;
    private float attackRadius;
    private int piercing;

    private WeaponData_Runtime weaponData;
    private LayerMask enemyLayerMask;
    private int currentPiercing;
    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();
    private bool canAttack;
    private Transform playerTransform;
    private float revolutionAngle;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();

        revolutionSpeed = weaponData.notebookTear.revolutionSpeed;
        revolutionRadius = weaponData.notebookTear.revolutionRadius;
        rotationSpeed = weaponData.notebookTear.rotationSpeed;
        revolutionTime = weaponData.notebookTear.revolutionTime;
        animationTime = weaponData.notebookTear.animationTime;
        fadeOutTime = weaponData.notebookTear.fadeOutTime;
        attackCooldown = weaponData.notebookTear.attackCooldown;
        attackDamage = weaponData.notebookTear.attackDamage;
        attackRadius = weaponData.notebookTear.attackRadius;
        piercing = weaponData.notebookTear.piercing;

        currentPiercing = piercing;
        enemyLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;
        playerTransform = DataManager.Instance.GetPlayerTargetTransform();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() {
        if (!canAttack) {
            return;
        }

        if (currentPiercing <= 0) {
            currentPiercing = piercing;
            canAttack = false;
            StartCoroutine(RevolutionEndAnimation());
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
            if (hit.TryGetComponent(out EnemyHealth enemyHealth) && !attackHitsHashSet.Contains(hit)) {
                attackHitsHashSet.Add(hit);
                Debug.Log($"Hit {hit.name}");
                currentPiercing--;
                enemyHealth.TakeDamage(attackDamage);
            }
        }
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
        canAttack = false;
        attackHitsHashSet.Clear();

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

    private IEnumerator ClearAttackHitHashSet() {
        attackHitsHashSet.Clear();
        yield return new WaitForSeconds(attackCooldown);
        StartCoroutine(ClearAttackHitHashSet());
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

        StartCoroutine(RevolutionStartAnimation());
        StartCoroutine(EndRevolutionAutomatically());
        StartCoroutine(ClearAttackHitHashSet());
    }

    private void OnDisable() {
        UpgradeManager.OnGearCogUpgrade -= UpgradeManager_OnGearCogUpgrade;

        StopAllCoroutines();
        revolutionAngle = 0f;
    }

    private void UpgradeManager_OnGearCogUpgrade(object sender, UpgradeManager.OnGearCogUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;
        revolutionSpeed *= e.attackSpeedToMultiply;
        rotationSpeed *= e.attackSpeedToMultiply;
    }

}