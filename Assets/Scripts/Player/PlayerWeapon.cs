using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerWeapon : MonoBehaviour {

    [SerializeField] private Transform aimPoint;
    [SerializeField] protected bool drawGizmos;

    protected int weaponDamage;
    protected int piercing;
    protected float attackCooldown;
    protected Vector3 aimDirection;
    protected Vector3 aimPosition;
    protected Vector3 previousAimPosition;
    protected float attackTimer;
    protected bool isAnimating;
    protected LayerMask enemyLayerMask;
    protected Transform playerTransform;
    protected float fadeOutTime;

    private float aimWeaponRadius;
    private float weaponRotationSpeed;

    protected virtual void Awake() {
        playerTransform = DataManager.Instance.GetPlayerTargetTransform();
        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        enemyLayerMask = DataManager.Instance.GetEnemyData().enemyLayerMask;
        weaponRotationSpeed = weaponData.weaponRotationSpeed;
        aimWeaponRadius = weaponData.aimWeaponRadius;
        fadeOutTime = weaponData.fadeOutTime;

        attackTimer = attackCooldown;
    }

    protected Vector3 GetAimPointPosition() {
        return aimPoint.position;
    }

    protected virtual void Update() {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f) {
            AnimateWeapon();
            Attack();
            attackTimer = attackCooldown;
        }

        AimWeapon();
    }

    protected void AimWeapon() {
        // position weapon
        Vector3 circleCenter = playerTransform.position;

        Vector3 mousePositionInScreen = Mouse.current.position.ReadValue();
        mousePositionInScreen.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionInScreen);
        aimDirection = mousePositionInWorld - circleCenter;
        aimPosition = aimDirection.normalized * aimWeaponRadius;

        Vector3 targetPosition;
        if (isAnimating) {
            targetPosition = playerTransform.position + previousAimPosition;
            this.transform.position = targetPosition;
            return;
        }

        targetPosition = playerTransform.position + aimPosition;
        this.transform.position = targetPosition;
        previousAimPosition = aimPosition;

        // rotate weapon
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
            targetRotation, Time.deltaTime * weaponRotationSpeed);
    }

    protected virtual void AnimateWeapon() {
        Debug.LogWarning("Implement AnimateWeapon() in the child weapon class!");
    }

    protected abstract void Attack();

}