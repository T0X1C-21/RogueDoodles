using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField] private GameObject weaponSlot;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private float weaponRotationSpeed;
    [SerializeField] private bool drawGizmos;

    private WeaponType currentWeaponType;
    private float aimWeaponRadius = 1f;
    private int weaponDamage;
    private float attackCooldown;
    private float attackRange;
    private float attackArcThreshold;
    private float preAnimationTime;
    private float animationTime;

    private Vector3 aimDirection;
    private Vector3 aimPosition;
    private Vector3 previousAimPosition;
    private float attackTimer;
    private bool isAnimating;

    private void Awake() {
        currentWeaponType = DataManager.Instance.GetPlayerData().startingWeapon;
        aimWeaponRadius = DataManager.Instance.GetPlayerData().aimWeaponRadius;

        switch (currentWeaponType) {
            case WeaponType.Pencil:
                weaponSlot.GetComponent<SpriteRenderer>().sprite = DataManager.Instance.GetPlayerData().pencil.sprite;

                PlayerData playerData = DataManager.Instance.GetPlayerData();
                weaponDamage = playerData.pencil.damage;
                attackCooldown = playerData.pencil.attackCooldown;
                attackRange = playerData.pencil.attackRange;
                attackArcThreshold = playerData.pencil.attackArcThreshold;
                preAnimationTime = playerData.pencil.preAnimationTime;
                animationTime = playerData.pencil.animationTime;

                break;
        }

        attackTimer = attackCooldown;
    }

    private void Update() {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f) {
            StartCoroutine(Attack());
            AnimateWeapon();
            attackTimer = attackCooldown;
        }

        AimWeapon();
    }

    private IEnumerator Attack() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(aimPoint.position, attackRange);

        foreach(Collider2D hit in hits) {
            if(hit == null) {
                yield break;
            }

            if(hit.TryGetComponent<Enemy>(out Enemy enemy)) {
                switch (currentWeaponType) {
                    case WeaponType.Pencil:
                        Vector3 enemyDirection = aimPosition - enemy.transform.position;
                        float dotProduct = Vector2.Dot(aimDirection.normalized, enemyDirection.normalized);
                        if(dotProduct < attackArcThreshold) {
                            yield return new WaitForSeconds(preAnimationTime + animationTime);
                            enemy.gameObject.GetComponent<Health>().TakeDamage(weaponDamage);
                        }
                        break;
                }
            }
        }
    }

    private void AimWeapon() {
        // position weapon
        Vector3 circleCenter = this.transform.position;

        Vector3 mousePositionInScreen = Mouse.current.position.ReadValue();
        mousePositionInScreen.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionInScreen);
        aimDirection = mousePositionInWorld - circleCenter;
        aimPosition = aimDirection.normalized * aimWeaponRadius;

        if (isAnimating) {
            weaponSlot.transform.position = this.transform.position + previousAimPosition;
            return;
        }

        weaponSlot.transform.position = this.transform.position + aimPosition;
        previousAimPosition = aimPosition;

        // rotate weapon
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        weaponSlot.transform.rotation = Quaternion.Slerp(weaponSlot.transform.rotation, 
            targetRotation, Time.deltaTime * weaponRotationSpeed);
    }

    private void AnimateWeapon() {
        isAnimating = true;
        Tween goUp = weaponSlot.transform.DORotate(new Vector3(0f, 0f, 20f), preAnimationTime, RotateMode.WorldAxisAdd);
        Tween slash = weaponSlot.transform.DORotate(new Vector3(0f, 0f, -40f), animationTime, RotateMode.WorldAxisAdd).SetEase(Ease.InOutBack);
        Tween reset = weaponSlot.transform.DORotate(new Vector3(0f, 0f, 20f), preAnimationTime, RotateMode.WorldAxisAdd);

        Sequence attackAnimationSequence = DOTween.Sequence();
        attackAnimationSequence.Append(goUp);
        attackAnimationSequence.Append(slash);
        attackAnimationSequence.Append(reset);
        attackAnimationSequence.OnComplete(() => {
            isAnimating = false;
        });

    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
        Gizmos.color = new Color(0f, 0f, 0f, 0.2f);
        Gizmos.DrawWireSphere(aimPoint.position, attackRange);
        Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - this.transform.position;

        float angle = Mathf.Acos(attackArcThreshold) * Mathf.Rad2Deg;
        Gizmos.color = Color.yellow;
        
        int segments = 30;
        float startAngle = -angle;
        float endAngle = angle;

        Vector3 previousPoint = this.transform.position + Quaternion.Euler(0, 0, startAngle) * aimDirection.normalized * attackRange;

        for(int i = 1; i <= segments; i++) {
            float t = Mathf.Lerp(startAngle, endAngle, i / (float)segments);
            Vector3 nextPoint = this.transform.position + Quaternion.Euler(0, 0, t) * aimDirection.normalized * attackRange;

            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
    }

}