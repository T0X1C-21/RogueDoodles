using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookTear : PlayerWeapon {

    private float revolutionSpeed;
    private float revolutionRadius;
    private float rotationSpeed;
    private float autoStartRevolutionTime;
    private float autoEndRevolutionTime;
    private float attackRadius;
    private float animationTime;
    private bool canAttack;
    private bool isIdle;

    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();
    private int currentPiercing;
    private float revolutionAngle;

    protected override void Awake() {
        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        playerTransform = DataManager.Instance.GetPlayerTargetTransform();

        weaponDamage = weaponData.notebookTear.attackDamage;
        piercing = weaponData.notebookTear.piercing;
        currentPiercing = piercing;
        revolutionSpeed = weaponData.notebookTear.revolutionSpeed;
        revolutionRadius = weaponData.notebookTear.revolutionRadius;
        rotationSpeed = weaponData.notebookTear.rotationSpeed;
        autoStartRevolutionTime = weaponData.notebookTear.autoStartRevolutionTime;
        autoEndRevolutionTime = weaponData.notebookTear.autoEndRevolutionTime;
        attackRadius = weaponData.notebookTear.attackRadius;
        animationTime = weaponData.notebookTear.animationTime;
        fadeOutTime = weaponData.notebookTear.fadeOutTime;
        attackCooldown = weaponData.notebookTear.attackCooldown;
        attackTimer = attackCooldown;
        enemyLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        AnimateWeapon();
        StartCoroutine(ClearAttackHitHashSet());
    }

    protected override void Update() {
        if (canAttack) {
            Attack();
        }

        if (isIdle) {
            this.transform.position = playerTransform.position;
        }
    }

    protected override void Attack() {
        if(currentPiercing <= 0) {
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

        if(hits == null) {
            return;
        }

        foreach(Collider2D hit in hits) {
            if (attackHitsHashSet.Contains(hit)) {
                continue;
            }
            attackHitsHashSet.Add(hit);
            if(hit.TryGetComponent(out EnemyHealth enemyHealth)) {
                currentPiercing--;
                enemyHealth.TakeDamage(weaponDamage);
            }
        }
    }

    protected override void AnimateWeapon() {
        StopAllCoroutines();
        StartCoroutine(RevolutionStartAnimation());
        StartCoroutine(AutoEndRevolution());
    }

    private IEnumerator AutoEndRevolution() {
        yield return new WaitForSeconds(autoEndRevolutionTime);
        currentPiercing = piercing;
        canAttack = false;
        StartCoroutine(RevolutionEndAnimation());
    }

    private IEnumerator RevolutionStartAnimation() {
        isIdle = false;

        spriteRenderer?.DOFade(1f, fadeOutTime);

        float t = 0f;
        while(t <= animationTime) {
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
        attackHitsHashSet.Clear();

        spriteRenderer?.DOFade(0f, fadeOutTime);

        float t = 0f;
        while(t <= animationTime) {
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

        isIdle = true;
        yield return new WaitForSeconds(autoStartRevolutionTime);
        StartCoroutine(RevolutionStartAnimation());
    }

    private IEnumerator ClearAttackHitHashSet() {
        attackHitsHashSet.Clear();
        yield return new WaitForSeconds(attackCooldown);
        StartCoroutine(ClearAttackHitHashSet());
    }

    private void OnEnable() {
        UpgradeManager.OnGearCogUpgrade += UpgradeManager_OnGearCogUpgrade;
    }

    private void OnDisable() {
        UpgradeManager.OnGearCogUpgrade -= UpgradeManager_OnGearCogUpgrade;
    }

    private void UpgradeManager_OnGearCogUpgrade(object sender, UpgradeManager.OnGearCogUpgradeEventArgs e) {
        revolutionSpeed *= e.attackSpeedToMultiply;
        rotationSpeed *= e.attackSpeedToMultiply;
    }

}