using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField] private GameObject weaponSlot;
    [SerializeField] private bool drawGizmos;

    private WeaponType currentWeaponType;
    private int weaponDamage;
    private float attackCooldown;
    private float attackRange;
    private float aimWeaponRadius = 1f;

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
                        //Vector2 mouseDirection = Input.mousePosition - this.transform.position;
                        //float dotProduct = Vector2.Dot();
                        enemy.gameObject.GetComponent<Health>().TakeDamage(weaponDamage);
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
        Vector3 mouseDirection = mousePositionInWorld - circleCenter;

        Vector3 aimPosition = Vector3.Normalize(mouseDirection) * aimWeaponRadius;
        weaponSlot.transform.position = this.transform.position + aimPosition;

        // temporary rotation
        float dotProduct = Vector2.Dot(mousePositionInWorld, Vector2.right);
        int direction = (dotProduct > 0) ? 1 : -1;
        Vector3 scale = weaponSlot.transform.localScale;
        weaponSlot.transform.localScale = new Vector3(direction, scale.y, scale.z);
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
        Gizmos.DrawWireSphere(weaponSlot.transform.position, attackRange);
        Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - this.transform.position;
        Gizmos.DrawRay(this.transform.position, mouseDirection);
    }

}
