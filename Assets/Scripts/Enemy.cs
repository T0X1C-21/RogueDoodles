using UnityEngine;

public class Enemy : MonoBehaviour {

    private Transform playerTarget;
    private float moveSpeed;
    private float attackCooldown;
    private float attackRange;
    private bool canAttackPlayer;

    private void Awake() {
        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        moveSpeed = enemyData.balloon.moveSpeed;
        attackCooldown = enemyData.balloon.attackCooldown;
        attackRange = enemyData.balloon.attackRange;

        playerTarget = DataManager.Instance.GetPlayerTargetTransform();
    }

    private void Update() {
        Movement();
    }

    private void Movement() {
        Vector3 moveDirection = playerTarget.position - this.transform.position;
        this.transform.position += moveDirection.normalized * Time.deltaTime * moveSpeed;
    }

    private void DetectPlayer() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, attackRange);
        //foreach(Collider2D hit in )
    }

    private void Attack() {

    }
}
