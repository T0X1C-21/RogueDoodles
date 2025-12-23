using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pencil : PlayerWeapon {

    private float attackRange;
    private float attackArcThreshold;
    private float preAnimationTime;
    private float animationTime;

    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();

    protected override void Awake() {
        base.Awake();

        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        weaponDamage = weaponData.pencil.damage;
        piercing = weaponData.pencil.piercing;
        attackCooldown = weaponData.pencil.attackCooldown;
        attackRange = weaponData.pencil.attackRange;
        attackArcThreshold = weaponData.pencil.attackArcThreshold;
        preAnimationTime = weaponData.pencil.preAnimationTime;
        animationTime = weaponData.pencil.animationTime;
        fadeOutTime = weaponData.pencil.fadeOutTime;
    }

    // Collect enemies to hit at slash
    protected override void Attack() {
        Vector2 boxSize = new Vector2(1f, 0.25f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(GetAimPointPosition(), boxSize, 
            this.transform.eulerAngles.z, enemyLayerMask);

        if(hits == null) {
            return;
        }
        
        foreach(Collider2D hit in hits) {
            attackHitsHashSet.Add(hit);
        }
    }

    private void ApplyDamage() {
        if(attackHitsHashSet.Count == 0) {
            return;
        }

        foreach(Collider2D hit in attackHitsHashSet) {
            if (hit.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth) && piercing > 0) {
                enemyHealth.TakeDamage(weaponDamage);
                piercing -= 1;
            }
        }

        attackHitsHashSet.Clear();
    }

    protected override void AnimateWeapon() {
        isAnimating = true;
        SpriteRenderer spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        Tween appear = spriteRenderer.DOFade(1f, fadeOutTime);
        Tween goUp = this.transform.DORotate(new Vector3(0f, 0f, 20f), preAnimationTime, RotateMode.WorldAxisAdd);
        Tween slash = this.transform.DORotate(new Vector3(0f, 0f, -40f), animationTime, 
            RotateMode.WorldAxisAdd).SetEase(Ease.InOutBack).OnComplete(() => {
            ApplyDamage();
        });
        Tween reset = this.transform.DORotate(new Vector3(0f, 0f, 20f), preAnimationTime, RotateMode.WorldAxisAdd);
        Tween disappear = spriteRenderer.DOFade(0f, fadeOutTime);

        Sequence attackAnimationSequence = DOTween.Sequence();
        attackAnimationSequence.Append(appear);
        attackAnimationSequence.Append(goUp);
        attackAnimationSequence.Append(slash);
        attackAnimationSequence.Append(reset);
        attackAnimationSequence.Append(disappear);
        attackAnimationSequence.OnComplete(() => {
            isAnimating = false;

            // Reset Piercing
            piercing = DataManager.Instance.GetWeaponData().pencil.piercing;
        });
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }

        Gizmos.color = new Color(0f, 0f, 0f, 0.2f);
        Gizmos.DrawWireCube(GetAimPointPosition(), new Vector2(1f, 0.25f));
        Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - this.transform.position;

        float angle = Mathf.Acos(attackArcThreshold) * Mathf.Rad2Deg;
        Gizmos.color = Color.yellow;
        
        int segments = 30;
        float startAngle = -angle;
        float endAngle = angle;

        Vector3 previousPoint = this.transform.position + 
            Quaternion.Euler(0, 0, startAngle) * aimDirection.normalized * attackRange;

        for(int i = 1; i <= segments; i++) {
            float t = Mathf.Lerp(startAngle, endAngle, i / (float)segments);
            Vector3 nextPoint = this.transform.position + Quaternion.Euler(0, 0, t) * aimDirection.normalized * attackRange;

            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
    }

}
