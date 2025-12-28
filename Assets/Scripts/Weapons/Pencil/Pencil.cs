using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : PlayerWeapon {

    private WeaponData_Runtime weaponData;
    private float preAnimationTime;
    private float animationTime;

    private HashSet<Collider2D> attackHitsHashSet = new HashSet<Collider2D>();

    protected override void Awake() {
        base.Awake();

        weaponData = RuntimeGameData.Instance.GetWeaponData();
        weaponDamage = weaponData.pencil.damage;
        piercing = weaponData.pencil.piercing;
        attackCooldown = weaponData.pencil.attackCooldown;
        preAnimationTime = weaponData.pencil.preAnimationTime;
        animationTime = weaponData.pencil.animationTime;
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
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
            piercing = RuntimeGameData.Instance.GetWeaponData().pencil.piercing;
        });
    }

    private void OnEnable() {
        UpgradeManager.OnGearCogUpgrade += UpgradeManager_OnGearCogUpgrade;
    }

    private void OnDisable() {
        UpgradeManager.OnGearCogUpgrade -= UpgradeManager_OnGearCogUpgrade;
    }

    private void UpgradeManager_OnGearCogUpgrade(object sender, UpgradeManager.OnGearCogUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;
        preAnimationTime /= e.attackSpeedToMultiply;
        animationTime /= e.attackSpeedToMultiply;

        weaponData.pencil.attackCooldown /= e.attackSpeedToMultiply;
        weaponData.pencil.preAnimationTime /= e.attackSpeedToMultiply;
        weaponData.pencil.animationTime /= e.attackSpeedToMultiply;
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
    }

}
