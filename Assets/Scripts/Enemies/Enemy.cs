using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable {

    [SerializeField] protected bool drawGizmos;

    protected Transform playerTarget;
    protected float moveSpeed;
    protected float attackCooldown;
    protected float attackRange;
    protected float attackDamage;
    protected float attackPointOffsetMultiplier;
    protected float movementStopThreshold;
    protected bool canAttack;

    private float attackTimer;
    private Vector3 moveDirection;
    private Vector3 targetPosition;
    private bool stopMovement;
    private LayerMask playerLayerMask;

    public virtual void OnSpawnFromPool() {
        if(this.TryGetComponent(out EnemyHealth enemyHealth)) {
            enemyHealth.ResetHealth();
        }
    }

    public virtual void OnReturnToPool() {

    }

    protected virtual void Awake() {
        playerTarget = RuntimeGameData.Instance.GetPlayerTargetTransform();
        playerLayerMask = RuntimeGameData.Instance.GetPlayerData().playerLayerMask;

        canAttack = true;
    }

    protected virtual void Update() {
        StopMovement();
        Movement();
        DetectAndAttackPlayer();
        FlipEnemy();
    }

    protected virtual void StopMovement() {
        float distance = Vector2.Distance(this.transform.position, targetPosition);
        stopMovement = (distance <= movementStopThreshold) ? true : false;
    }

    protected virtual void Movement() {
        Vector3 targetPositionOffset = this.transform.position - playerTarget.position;
        targetPosition = playerTarget.position + (targetPositionOffset.normalized * attackPointOffsetMultiplier);

        if (stopMovement) {
            return;
        }

        moveDirection = targetPosition - this.transform.position;
        this.transform.position += moveDirection.normalized * Time.deltaTime * moveSpeed;
    }

    protected virtual void DetectAndAttackPlayer() {
        if (!canAttack) {
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, attackRange, playerLayerMask);
        foreach(Collider2D hit in hits) {
            if(hit.TryGetComponent(out PlayerMovement player)) {
                if(attackTimer <= 0f) {
                    hit.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                    attackTimer = attackCooldown;
                } else {
                    attackTimer -= Time.deltaTime;
                }
            }
        }
    }

    protected virtual void FlipEnemy() {
        float dotProduct = Vector2.Dot(moveDirection.normalized, Vector2.right);
        int direction = (dotProduct > 0) ? 1 : -1;
        Vector3 scale = this.transform.localScale;
        this.transform.localScale = new Vector3(direction, scale.y, scale.z);
    }

    protected virtual void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(targetPosition, 0.2f);
    }
}