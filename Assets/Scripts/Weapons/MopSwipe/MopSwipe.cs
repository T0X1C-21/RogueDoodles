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
        UpgradeManager.OnGearCogUpgrade += UpgradeManager_OnGearCogUpgrade;
        UpgradeManager.OnInkOverflowUpgrade += UpgradeManager_OnInkOverflowUpgrade;
    }

    private void OnDisable() {
        UpgradeManager.OnGearCogUpgrade -= UpgradeManager_OnGearCogUpgrade;
        UpgradeManager.OnInkOverflowUpgrade -= UpgradeManager_OnInkOverflowUpgrade;
    }

    private void UpgradeManager_OnGearCogUpgrade(object sender, UpgradeManager.OnGearCogUpgradeEventArgs e) {
        attackCooldown /= e.attackSpeedToMultiply;
        preAnimationTime /= e.attackSpeedToMultiply;
        animationTime /= e.attackSpeedToMultiply;

        weaponData.mopSwipe.attackCooldown /= e.attackSpeedToMultiply;
        weaponData.mopSwipe.preAnimationTime /= e.attackSpeedToMultiply;
        weaponData.mopSwipe.animationTime /= e.attackSpeedToMultiply;
    }

    private void UpgradeManager_OnInkOverflowUpgrade(object sender, UpgradeManager.OnInkOverflowUpgradeEventArgs e) {
        numberOfBubbles += e.projectileCountToAdd;

        weaponData.mopSwipe.numberOfBubbles += e.projectileCountToAdd;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(this.transform.position, this.transform.right);
    }

}
