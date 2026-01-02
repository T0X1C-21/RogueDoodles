using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ChalkShot : PlayerWeapon {

    [SerializeField] private GameObject spriteGameObject;

    private WeaponData_Runtime weaponData;
    private GameObject chalkShotBulletPrefab;
    private float preAnimationTime;
    private float animationTime;
    private float recoilStrength;
    private int numberOfBullets;
    private int currentNumberOfBullets;
    private float reloadTime;

    private bool isReloading;

    protected override void Awake() {
        base.Awake();

        weaponData = RuntimeGameData.Instance.GetWeaponData();
        chalkShotBulletPrefab = weaponData.chalkShot.chalkShotBulletPrefab;
        piercing = weaponData.chalkShot.piercing;
        attackCooldown = weaponData.chalkShot.attackCooldown;
        preAnimationTime = weaponData.chalkShot.preAnimationTime;
        animationTime = weaponData.chalkShot.animationTime;
        recoilStrength = weaponData.chalkShot.recoilStrength;
        numberOfBullets = weaponData.chalkShot.numberOfBullets;
        currentNumberOfBullets = numberOfBullets;
        reloadTime = weaponData.chalkShot.reloadTime;

        this.transform.localScale = weaponData.chalkShot.size;
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Update() {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f && !isReloading) {
            StartCoroutine(AnimateWeapon());
            attackTimer = attackCooldown;
        }

        AimWeapon();
    }

    private new IEnumerator AnimateWeapon() {
        if(currentNumberOfBullets <= 0) {
            ReloadGun();
            yield break;
        }
        StartCoroutine(backAnimation());
        IEnumerator backAnimation() {
            Vector3 playerDirectionNormalized = (playerTransform.position - this.transform.position).normalized;
            Vector3 startPosition = spriteGameObject.transform.localPosition;
            float pushForce = recoilStrength;
            Vector3 endPosition = playerDirectionNormalized * pushForce;
            float t = 0f;
            while(t < preAnimationTime) {
                t += Time.deltaTime / preAnimationTime;
                spriteGameObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
            Attack();
            StartCoroutine(frontAnimation());
        }
        IEnumerator frontAnimation() {
            Vector3 startPosition = spriteGameObject.transform.localPosition;
            Vector3 endPosition = Vector3.zero;
            float t = 0f;
            while(t < animationTime) {
                t += Time.deltaTime / animationTime;
                spriteGameObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
            spriteGameObject.transform.localPosition = Vector3.zero;
        }
        yield return null;
    }

    protected override void Attack() {
        GameObject bullet = ObjectPoolManager.GetObjectFromPool(PoolType.ChalkShotProjectile, chalkShotBulletPrefab,
            GetAimPointPosition(), Quaternion.identity);
        bullet.TryGetComponent(out ChalkShotBulletProjectile chalkShotBulletProjectile);
        chalkShotBulletProjectile.InitializeChalkShotBullet(this.transform.position + aimDirection);
        currentNumberOfBullets--;
    }

    private void ReloadGun() {
        isReloading = true;
        isAnimating = true;
        spriteRenderer.DOKill();
        Tween rotate = spriteGameObject.transform.DORotate(new Vector3(0f, 0f, 360f), reloadTime, RotateMode.WorldAxisAdd);

        Sequence reloadAnimationSequence = DOTween.Sequence();
        reloadAnimationSequence.Append(rotate);
        reloadAnimationSequence.OnComplete(() => {
            currentNumberOfBullets = numberOfBullets;
            attackTimer = attackCooldown;
            isReloading = false;
            isAnimating = false;
        });
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

        weaponData.chalkShot.attackCooldown /= e.attackSpeedToMultiply;
        weaponData.chalkShot.preAnimationTime /= e.attackSpeedToMultiply;
        weaponData.chalkShot.animationTime /= e.attackSpeedToMultiply;
    }

    private void PassiveManager_OnProjectileCountPlusPlusUpgrade(object sender, PassiveManager.OnProjectileCountPlusPlusUpgradeEventArgs e) {
        numberOfBullets += e.projectileCountToAdd;

        weaponData.chalkShot.numberOfBullets += e.projectileCountToAdd;
    }

    private void PassiveManager_OnPiercingPlusPlusUpgrade(object sender, PassiveManager.OnPiercingPlusPlusUpgradeEventArgs e) {
        weaponData.chalkShot.piercing += e.piercingToAdd;
    }
    
    private void PassiveManager_OnSizePlusPlusUpgrade(object sender, PassiveManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.chalkShot.size *= e.sizeToMultiply;
        this.transform.localScale = weaponData.chalkShot.size;

        weaponData.chalkShot.targetDetectionRadius *= e.sizeToMultiply;

        weaponData.aimWeaponRadius *= e.sizeToMultiply;
        aimWeaponRadius = weaponData.aimWeaponRadius;
    }
    
    private void PassiveManager_OnAttackDamagePlusPlusUpgrade(object sender, PassiveManager.OnAttackDamagePlusPlusUpgradeEventArgs e) {
        weaponData.chalkShot.damageAmount *= e.attackDamageToMultiply;
    }

}