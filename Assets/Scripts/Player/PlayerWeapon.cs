using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField] private GameObject weaponSlot;

    private WeaponType currentWeaponType;
    private int weaponDamage;
    private float attackCooldown;
    private float attackRange;

    private float attackTimer;

    private void Awake() {
        currentWeaponType = DataManager.Instance.GetPlayerData().startingWeapon;

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
    }

    private void Attack() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(weaponSlot.transform.position, attackRange);

        foreach(Collider2D hit in hits) {
            if(hit.TryGetComponent<Enemy>(out Enemy enemy)) {
                enemy.gameObject.GetComponent<Health>().TakeDamage(weaponDamage);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(weaponSlot.transform.position, attackRange);
    }

}
