using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : PlayerWeapon {

    private WeaponData_Runtime weaponData;
    private float attackRange;
    private float preAnimationTime;
    private float animationTime;

    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();

    protected override void Awake() {
        base.Awake();

        weaponData = RuntimeGameData.Instance.GetWeaponData();
        weaponDamage = weaponData.pencil.damage;
        piercing = weaponData.pencil.piercing;
        attackCooldown = weaponData.pencil.attackCooldown;
        attackRange = weaponData.pencil.attackRange;
        preAnimationTime = weaponData.pencil.preAnimationTime;
        animationTime = weaponData.pencil.animationTime;
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        this.transform.localScale = weaponData.pencil.size;
    }

    // Collect enemies to hit at slash
    protected override void Attack() {
        Vector2 boxSize = new Vector2(1f, 0.25f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(aimPoint.position, attackRange, enemyLayerMask);

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
        Tween goUp = this.transform.DORotate(new Vector3(0f, 0f, 20f), preAnimationTime, RotateMode.WorldAxisAdd);
        Tween slash = this.transform.DORotate(new Vector3(0f, 0f, -40f), animationTime, 
            RotateMode.WorldAxisAdd).SetEase(Ease.InOutBack).OnComplete(() => {
            ApplyDamage();
        });
        Tween reset = this.transform.DORotate(new Vector3(0f, 0f, 20f), preAnimationTime, RotateMode.WorldAxisAdd);

        Sequence attackAnimationSequence = DOTween.Sequence();
        attackAnimationSequence.Append(goUp);
        attackAnimationSequence.Append(slash);
        attackAnimationSequence.Append(reset);
        attackAnimationSequence.OnComplete(() => {
            isAnimating = false;

            // Reset Piercing
            piercing = weaponData.pencil.piercing;
        });
    }

    private void OnEnable() {
        UpgradeManager.OnAttackSpeedPlusPlusUpgrade += UpgradeManager_OnAttackSpeedPlusPlusUpgrade;
        UpgradeManager.OnPiercingPlusPlusUpgrade += UpgradeManager_OnPiercingPlusPlusUpgrade;
        UpgradeManager.OnSizePlusPlusUpgrade += UpgradeManager_OnSizePlusPlusUpgrade;
    }

    private void OnDisable() {
        UpgradeManager.OnAttackSpeedPlusPlusUpgrade -= UpgradeManager_OnAttackSpeedPlusPlusUpgrade;
        UpgradeManager.OnPiercingPlusPlusUpgrade -= UpgradeManager_OnPiercingPlusPlusUpgrade;
        UpgradeManager.OnSizePlusPlusUpgrade -= UpgradeManager_OnSizePlusPlusUpgrade;
    }

    private void UpgradeManager_OnAttackSpeedPlusPlusUpgrade(object sender, UpgradeManager.OnAttackSpeedPlusPlusUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;
        preAnimationTime /= e.attackSpeedToMultiply;
        animationTime /= e.attackSpeedToMultiply;

        weaponData.pencil.attackCooldown /= e.attackSpeedToMultiply;
        weaponData.pencil.preAnimationTime /= e.attackSpeedToMultiply;
        weaponData.pencil.animationTime /= e.attackSpeedToMultiply;
    }

    private void UpgradeManager_OnPiercingPlusPlusUpgrade(object sender, UpgradeManager.OnPiercingPlusPlusUpgradeEventArgs e) {
        piercing += e.piercingToAdd;

        weaponData.pencil.piercing += e.piercingToAdd;
    }
    
    private void UpgradeManager_OnSizePlusPlusUpgrade(object sender, UpgradeManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.pencil.size *= e.sizeToMultiply;
        this.transform.localScale = weaponData.pencil.size;

        attackRange *= e.sizeToMultiply;
        weaponData.pencil.attackRange *= e.sizeToMultiply;

        weaponData.aimWeaponRadius *= e.sizeToMultiply;
        aimWeaponRadius = weaponData.aimWeaponRadius;
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
    }

}
