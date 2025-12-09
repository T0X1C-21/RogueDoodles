using UnityEngine;

public class Balloon : Enemy {

    protected override void Awake() {
        base.Awake();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        moveSpeed = enemyData.balloon.moveSpeed + Random.Range
            (-enemyData.balloon.moveSpeedOffset, enemyData.balloon.moveSpeedOffset);
        attackCooldown = enemyData.balloon.attackCooldown;
        attackRange = enemyData.balloon.attackRange;
        attackDamage = enemyData.balloon.attackDamage;
        attackPointOffsetMultiplier = enemyData.balloon.attackPointOffsetMultiplier;
        movementStopThreshold = enemyData.balloon.movementStopThreshold;
    }

}