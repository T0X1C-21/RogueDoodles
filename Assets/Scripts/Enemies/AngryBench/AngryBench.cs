using UnityEngine;

public class AngryBench : Enemy {
    
    protected override void Awake() {
        base.Awake();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        moveSpeed = enemyData.angryBench.moveSpeed + Random.Range
            (-enemyData.angryBench.moveSpeedOffset, enemyData.angryBench.moveSpeedOffset);
        attackCooldown = enemyData.angryBench.attackCooldown;
        attackRange = enemyData.angryBench.attackRange;
        attackDamage = enemyData.angryBench.attackDamage;
        attackPointOffsetMultiplier = enemyData.angryBench.attackPointOffsetMultiplier;
        movementStopThreshold = enemyData.angryBench.movementStopThreshold;
    }

}
