using UnityEngine;

public class CursedChalkStick : Enemy {

    protected override void Awake() {
        base.Awake();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        moveSpeed = enemyData.cursedChalkStick.moveSpeed + Random.Range
            (-enemyData.cursedChalkStick.moveSpeedOffset, enemyData.cursedChalkStick.moveSpeedOffset);
        attackCooldown = enemyData.cursedChalkStick.attackCooldown;
        attackRange = enemyData.cursedChalkStick.attackRange;
        attackDamage = enemyData.cursedChalkStick.attackDamage;
        attackPointOffsetMultiplier = enemyData.cursedChalkStick.attackPointOffsetMultiplier;
        movementStopThreshold = enemyData.cursedChalkStick.movementStopThreshold;
    }

}
