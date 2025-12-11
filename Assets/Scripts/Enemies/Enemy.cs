using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable {

    [SerializeField] private bool drawGizmos;

    private Transform playerTarget;
    protected float moveSpeed;
    protected float attackCooldown;
    protected float attackRange;
    protected float attackDamage;
    protected float attackPointOffsetMultiplier;
    protected float movementStopThreshold;

    private float attackTimer;
    private Vector3 moveDirection;
    private Vector3 targetPosition;
    private bool stopMovement;

    public virtual void OnSpawnFromPool() {
        if(this.TryGetComponent(out EnemyHealth enemyHealth)) {
            enemyHealth.ResetHealth();
        }
    }

    public virtual void OnReturnToPool() {

    }

    protected virtual void Awake() {
        playerTarget = DataManager.Instance.GetPlayerTargetTransform();
    }

    private void Update() {
        StopMovement();
        Movement();
        DetectAndAttackPlayer();
        FlipEnemy();
    }

    private void StopMovement() {
        float distance = Vector2.Distance(this.transform.position, targetPosition);
        stopMovement = (distance <= movementStopThreshold) ? true : false;
    }

    private void Movement() {
        Vector3 targetPositionOffset = this.transform.position - playerTarget.position;
        targetPosition = playerTarget.position + (targetPositionOffset.normalized * attackPointOffsetMultiplier);

        if (stopMovement) {
            return;
        }

        moveDirection = targetPosition - this.transform.position;
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
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(targetPosition, 0.2f);
    }
}