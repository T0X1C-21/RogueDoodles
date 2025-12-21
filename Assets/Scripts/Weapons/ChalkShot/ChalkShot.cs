using DG.Tweening;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class ChalkShot : PlayerWeapon {

    [SerializeField] private GameObject spriteGameObject;

    private GameObject chalkShotBulletPrefab;
    private float preAnimationTime;
    private float animationTime;
    private int numberOfBullets;
    private int currentNumberOfBullets;
    private float reloadTime;

    private float fadeOutTime = 0.2f;
    private SpriteRenderer spriteRenderer;
    private bool isReloading;

    protected override void Awake() {
        base.Awake();

        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        chalkShotBulletPrefab = weaponData.chalkShot.chalkShotBulletPrefab;
        weaponDamage = weaponData.chalkShot.damage;
        piercing = weaponData.chalkShot.piercing;
        attackCooldown = weaponData.chalkShot.attackCooldown;
        preAnimationTime = weaponData.chalkShot.preAnimationTime;
        animationTime = weaponData.chalkShot.animationTime;
        numberOfBullets = weaponData.chalkShot.numberOfBullets;
        currentNumberOfBullets = numberOfBullets;
        reloadTime = weaponData.chalkShot.reloadTime;

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
        Vector3 playerDirectionNormalized = (playerTransform.position - this.transform.position).normalized;
        Vector3 startPosition = spriteGameObject.transform.localPosition;
        float pushForce = 0.3f;
        Vector3 endPosition = playerDirectionNormalized * pushForce;
        Tween appear = spriteRenderer.DOFade(1f, fadeOutTime).SetEase(Ease.Linear);
        appear.Play().OnComplete(() => {
            StartCoroutine(backAnimation());
            StartCoroutine(frontAnimation());
        });
        IEnumerator backAnimation() {
            float t = 0f;
            while(t < preAnimationTime) {
                t += Time.deltaTime / preAnimationTime;
                spriteGameObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
            Attack();
        }
        IEnumerator frontAnimation() {
            if(currentNumberOfBullets <= 0) {
                ReloadGun();
                yield break;
            }
            float t = 0f;
            startPosition = spriteGameObject.transform.localPosition;
            endPosition = Vector3.zero;
            while(t < animationTime) {
                t += Time.deltaTime / animationTime;
                spriteGameObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
            Tween disappear = spriteRenderer.DOFade(0f, fadeOutTime).SetEase(Ease.Linear);
            disappear.Play();
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
        Tween appear = spriteRenderer.DOFade(1f, fadeOutTime);
        Tween rotate = spriteGameObject.transform.DORotate(new Vector3(0f, 0f, 360f), reloadTime, RotateMode.WorldAxisAdd);
        Tween disappear = spriteRenderer.DOFade(0f, fadeOutTime);

        Sequence reloadAnimationSequence = DOTween.Sequence();
        reloadAnimationSequence.Append(appear);
        reloadAnimationSequence.Append(rotate);
        reloadAnimationSequence.Append(disappear);
        reloadAnimationSequence.OnComplete(() => {
            currentNumberOfBullets = numberOfBullets;
            Debug.Log(currentNumberOfBullets);
            attackTimer = attackCooldown;
            isReloading = false;
            isAnimating = false;
        });
    }

}
