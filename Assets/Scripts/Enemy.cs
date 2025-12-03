using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private bool drawGizmos;

    private Transform playerTarget;
    protected float moveSpeed;
    protected float attackCooldown;
    protected float attackRange;
    protected float attackDamage;

    private float attackTimer;
    private Vector3 moveDirection;

    protected virtual void Awake() {
        playerTarget = DataManager.Instance.GetPlayerTargetTransform();
    }

    private void Update() {
        Movement();
        DetectAndAttackPlayer();
        FlipEnemy();
    }

    private void Movement() {
        moveDirection = playerTarget.position - this.transform.position;
        this.transform.position += moveDirection.normalized * Time.deltaTime * moveSpeed;
    }

    private void DetectAndAttackPlayer() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, attackRange);
        foreach(Collider2D hit in hits) {
            if(hit.TryGetComponent(out PlayerMovement player)) {
                if(attackTimer <= 0f) {
                    hit.GetComponent<Health>().TakeDamage(attackDamage);
                    attackTimer = attackCooldown;
                } else {
                    attackTimer -= Time.deltaTime;
                }
            }
        }
    }

    private void FlipEnemy() {
        float dotProduct = Vector2.Dot(moveDirection.normalized, Vector2.right);
        int direction = (dotProduct > 0) ? 1 : -1;
        Vector3 scale = this.transform.localScale;
        this.transform.localScale = new Vector3(direction, scale.y, scale.z);
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
