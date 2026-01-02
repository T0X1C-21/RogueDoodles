using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MopSwipe : PlayerWeapon {

    private WeaponData_Runtime weaponData;
    private float arcSwipeAmount;
    private float preAnimationTime;
    private float animationTime;
    private int numberOfBubbles;
    private GameObject mopSwipeBubbleProjectilePrefab;

    protected override void Awake() {
        base.Awake();

        weaponData = RuntimeGameData.Instance.GetWeaponData();
        attackCooldown = weaponData.mopSwipe.attackCooldown;
        arcSwipeAmount = weaponData.mopSwipe.arcSwipeAmount;
        preAnimationTime = weaponData.mopSwipe.preAnimationTime;
        animationTime = weaponData.mopSwipe.animationTime;
        numberOfBubbles = weaponData.mopSwipe.numberOfBubbles;
        mopSwipeBubbleProjectilePrefab = weaponData.mopSwipe.mopSwipeBubbleProjectilePrefab;
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        this.transform.localScale = weaponData.mopSwipe.size;
        attackTimer = attackCooldown;
    }

    protected override void Update() {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f) {
            AnimateWeapon();
            attackTimer = attackCooldown;
        }

        AimWeapon();
    }

    protected override void AnimateWeapon() {
        isAnimating = true;

        Tween goUp = this.transform.DORotate(new Vector3(0f, 0f, arcSwipeAmount / 2), preAnimationTime, 
            RotateMode.WorldAxisAdd);
        Tween slash = this.transform.DORotate(new Vector3(0f, 0f, -arcSwipeAmount), animationTime, 
            RotateMode.WorldAxisAdd).SetEase(Ease.Linear).OnStart(() => {
                StartCoroutine(ShootBubbles());
            });
        Tween reset = this.transform.DORotate(new Vector3(0f, 0f, arcSwipeAmount / 2), preAnimationTime, 
            RotateMode.WorldAxisAdd);

        Sequence attackAnimationSequence = DOTween.Sequence();
        attackAnimationSequence.Append(goUp);
        attackAnimationSequence.Append(slash);
        attackAnimationSequence.Append(reset);
        attackAnimationSequence.OnComplete(() => {
            isAnimating = false;
        });
    }

    private IEnumerator ShootBubbles() {
        float timeToSpawnBubble = animationTime / numberOfBubbles;
        int currentBubbleNumber = numberOfBubbles;
        GameObject bubbleObject = null;
        while(currentBubbleNumber > 0) {
            Vector3 spawnPosition = GetAimPointPosition();
            Vector3 moveDirectionNormalized = this.transform.right;
            bubbleObject = ObjectPoolManager.GetObjectFromPool(PoolType.MopSwipeBubbleProjectile, mopSwipeBubbleProjectilePrefab,
                spawnPosition, Quaternion.identity);
            bubbleObject.GetComponent<MopSwipeBubbleProjectile>().InitializeBubble(moveDirectionNormalized);
            currentBubbleNumber--;
            yield return new WaitForSeconds(timeToSpawnBubble);
        }
    }

    private void OnEnable() {
        PassiveManager.OnAttackSpeedPlusPlusUpgrade += PassiveManager_OnAttackSpeedPlusPlusUpgrade;
        PassiveManager.OnProjectileCountPlusPlusUpgrade += PassiveManager_OnProjectileCountPlusPlusUpgrade;
        PassiveManager.OnPiercingPlusPlusUpgrade += PassiveManager_OnPiercingPlusPlusUpgrade;
        PassiveManager.OnSizePlusPlusUpgrade += PassiveManager_OnSizePlusPlusUpgrade;
        PassiveManager.OnAttackDamagePlusPlusUpgrade += PassiveManager_OnAttackDamagePlusPlusUpgrade;
    }

    private void OnDisable() {
        PassiveManager.OnAttackSpeedPlusPlusUpgrade -= PassiveManager_OnAttackSpeedPlusPlusUpgrade;
        PassiveManager.OnProjectileCountPlusPlusUpgrade -= PassiveManager_OnProjectileCountPlusPlusUpgrade;
        PassiveManager.OnPiercingPlusPlusUpgrade -= PassiveManager_OnPiercingPlusPlusUpgrade;
        PassiveManager.OnSizePlusPlusUpgrade -= PassiveManager_OnSizePlusPlusUpgrade;
        PassiveManager.OnAttackDamagePlusPlusUpgrade -= PassiveManager_OnAttackDamagePlusPlusUpgrade;
    }

    private void PassiveManager_OnAttackSpeedPlusPlusUpgrade(object sender, PassiveManager.OnAttackSpeedPlusPlusUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;
        preAnimationTime /= e.attackSpeedToMultiply;
        animationTime /= e.attackSpeedToMultiply;

        weaponData.mopSwipe.attackCooldown /= e.attackSpeedToMultiply;
        weaponData.mopSwipe.preAnimationTime /= e.attackSpeedToMultiply;
        weaponData.mopSwipe.animationTime /= e.attackSpeedToMultiply;
    }

    private void PassiveManager_OnProjectileCountPlusPlusUpgrade(object sender, 
        PassiveManager.OnProjectileCountPlusPlusUpgradeEventArgs e) {
        numberOfBubbles += e.projectileCountToAdd;

        weaponData.mopSwipe.numberOfBubbles += e.projectileCountToAdd;
    }

    private void PassiveManager_OnPiercingPlusPlusUpgrade(object sender, PassiveManager.OnPiercingPlusPlusUpgradeEventArgs e) {
        weaponData.mopSwipe.piercing += e.piercingToAdd; 
    }
    
    private void PassiveManager_OnSizePlusPlusUpgrade(object sender, PassiveManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.mopSwipe.size *= e.sizeToMultiply;
        this.transform.localScale = weaponData.mopSwipe.size;

        weaponData.mopSwipe.targetDetectionRadius *= e.sizeToMultiply;

        weaponData.aimWeaponRadius *= e.sizeToMultiply;
        aimWeaponRadius = weaponData.aimWeaponRadius;
    }
    
    private void PassiveManager_OnAttackDamagePlusPlusUpgrade(object sender, 
        PassiveManager.OnAttackDamagePlusPlusUpgradeEventArgs e) {
        weaponData.mopSwipe.damageAmount *= e.attackDamageToMultiply;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(this.transform.position, this.transform.right);
    }

}
