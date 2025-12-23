using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookTear : PlayerWeapon {

    private float revolutionSpeed;
    private float revolutionRadius;
    private float autoStartRevolutionTime;
    private float autoEndRevolutionTime;
    private float attackRadius;
    private float animationTime;
    private bool canAttack;
    private bool isIdle;

    private SpriteRenderer spriteRenderer;
    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();
    private int currentPiercing;
    private float angle;

    protected override void Awake() {
        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        playerTransform = DataManager.Instance.GetPlayerTargetTransform();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        weaponDamage = weaponData.notebookTear.attackDamage;
        piercing = weaponData.notebookTear.piercing;
        currentPiercing = piercing;
        revolutionSpeed = weaponData.notebookTear.revolutionSpeed;
        revolutionRadius = weaponData.notebookTear.revolutionRadius;
        autoStartRevolutionTime = weaponData.notebookTear.autoStartRevolutionTime;
        autoEndRevolutionTime = weaponData.notebookTear.autoEndRevolutionTime;
        attackRadius = weaponData.notebookTear.attackRadius;
        animationTime = weaponData.notebookTear.animationTime;
        fadeOutTime = weaponData.notebookTear.fadeOutTime;
        attackCooldown = weaponData.notebookTear.attackCooldown;
        attackTimer = attackCooldown;
        enemyLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;

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

        angle += Time.deltaTime * revolutionSpeed;
        float xValue = revolutionRadius * Mathf.Cos(angle);
        float yValue = revolutionRadius * Mathf.Sin(angle);
        Vector3 targetPosition = playerTransform.position + new Vector3(xValue, yValue);
        this.transform.position = targetPosition;

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

        spriteRenderer.DOFade(1f, fadeOutTime);

        float t = 0f;
        while(t <= animationTime) {
            t += Time.deltaTime / animationTime;
            float radius = Mathf.Lerp(0f, revolutionRadius, t);
            angle += Time.deltaTime * revolutionSpeed;
            float xValue = radius * Mathf.Cos(angle);
            float yValue = radius * Mathf.Sin(angle);
            Vector3 targetPosition = playerTransform.position + new Vector3(xValue, yValue);
            this.transform.position = targetPosition;
            yield return null;
        }
        canAttack = true;
    }

    private IEnumerator RevolutionEndAnimation() {
        attackHitsHashSet.Clear();

        spriteRenderer.DOFade(0f, fadeOutTime);

        float t = 0f;
        while(t <= animationTime) {
            t += Time.deltaTime / animationTime;
            float radius = Mathf.Lerp(revolutionRadius, 0, t);
            angle += Time.deltaTime * revolutionSpeed;
            float xValue = radius * Mathf.Cos(angle);
            float yValue = radius * Mathf.Sin(angle);
            Vector3 targetPosition = playerTransform.position + new Vector3(xValue, yValue);
            this.transform.position = targetPosition;
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

}
