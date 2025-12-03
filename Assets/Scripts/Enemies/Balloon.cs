using UnityEngine;

public class Balloon : Enemy {

    protected override void Awake() {
        base.Awake();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        moveSpeed = enemyData.balloon.moveSpeed;
        attackCooldown = enemyData.balloon.attackCooldown;
        attackRange = enemyData.balloon.attackRange;
        attackDamage = enemyData.balloon.attackDamage;
    }

}
