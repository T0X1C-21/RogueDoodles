using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField] private GameObject weaponSlot;
    [SerializeField] private bool drawGizmos;

    private WeaponType currentWeaponType;
    private int weaponDamage;
    private float attackCooldown;
    private float attackRange;
    private float aimWeaponRadius = 1f;
    private Vector3 aimDirection;
    private Vector3 aimPosition;
    private float attackArcThreshold;

    private float attackTimer;

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

                break;
        }

        attackTimer = attackCooldown;
    }

    private void Update() {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f) {
            Attack();
            attackTimer = attackCooldown;
        }

        AimWeapon();
    }

    private void Attack() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(weaponSlot.transform.position, attackRange);

        foreach(Collider2D hit in hits) {
            if(hit.TryGetComponent<Enemy>(out Enemy enemy)) {
                switch (currentWeaponType) {
                    case WeaponType.Pencil:
                        Vector3 enemyDirection = aimPosition - enemy.transform.position;
                        float dotProduct = Vector2.Dot(aimDirection.normalized, enemyDirection.normalized);
                        if(dotProduct < attackArcThreshold) {
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
        weaponSlot.transform.position = this.transform.position + aimPosition;

        // temporary rotation
        Quaternion lookRotation = Quaternion.FromToRotation(weaponSlot.transform.position, aimDirection);
        weaponSlot.transform.rotation = lookRotation;
        //float dotProduct = Vector2.Dot(mousePositionInWorld, Vector2.right);
        //int direction = (dotProduct > 0) ? 1 : -1;
        //Vector3 scale = weaponSlot.transform.localScale;
        //weaponSlot.transform.localScale = new Vector3(direction, scale.y, scale.z);
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
        Gizmos.color = new Color(0f, 0f, 0f, 0.2f);
        Gizmos.DrawWireSphere(weaponSlot.transform.position, attackRange);
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
