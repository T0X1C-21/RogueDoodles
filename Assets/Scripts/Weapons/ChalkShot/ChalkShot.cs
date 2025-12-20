using DG.Tweening;
using UnityEngine;

public class ChalkShot : PlayerWeapon {

    private GameObject chalkShotBulletPrefab;
    private float preAnimationTime;
    private float animationTime;

    protected override void Awake() {
        base.Awake();

        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        chalkShotBulletPrefab = weaponData.chalkShot.chalkShotBulletPrefab;
        weaponDamage = weaponData.chalkShot.damage;
        piercing = weaponData.chalkShot.piercing;
        attackCooldown = weaponData.chalkShot.attackCooldown;
        preAnimationTime = weaponData.chalkShot.preAnimationTime;
        animationTime = weaponData.chalkShot.animationTime;
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
        SpriteRenderer spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        Tween appear = spriteRenderer.DOFade(1f, 0.2f);
        Vector3 playerDirectionNormalized = (playerTransform.position - this.transform.position).normalized;
        float pushForce = 0.1f;
        Vector3 currentPosition = this.transform.position;
        Tween pushBack = this.transform.DOMove(currentPosition + (playerDirectionNormalized * pushForce),
            preAnimationTime).SetEase(Ease.OutQuad).OnComplete(() => { Attack(); });
        Tween pushForward = this.transform.DOMove(currentPosition + (-playerDirectionNormalized * pushForce), 
            animationTime).SetEase(Ease.InQuad);
        this.transform.position += playerTransform.position;
        Tween disappear = spriteRenderer.DOFade(0f, 0.2f);

        Sequence recoilAnimationSequence = DOTween.Sequence();
        recoilAnimationSequence.Append(appear);
        recoilAnimationSequence.Append(pushBack);
        recoilAnimationSequence.Append(pushForward);
        recoilAnimationSequence.Append(disappear);
        recoilAnimationSequence.OnComplete(() => { isAnimating = false; });
    }

    protected override void Attack() {
        GameObject bullet = ObjectPoolManager.GetObjectFromPool(PoolType.ChalkShotProjectile, chalkShotBulletPrefab,
            GetAimPointPosition(), Quaternion.identity);
        bullet.TryGetComponent(out ChalkShotBulletProjectile chalkShotBulletProjectile);
        chalkShotBulletProjectile.InitializeChalkShotBullet(this.transform.position + aimDirection);
    }

}
