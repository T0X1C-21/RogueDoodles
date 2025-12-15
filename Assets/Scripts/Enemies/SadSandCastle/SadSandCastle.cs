using UnityEngine;

public class SadSandCastle : Enemy {

    protected override void Awake() {
        base.Awake();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        moveSpeed = enemyData.sadSandCastle.moveSpeed + Random.Range
            (-enemyData.sadSandCastle.moveSpeedOffset, enemyData.sadSandCastle.moveSpeedOffset);
        attackCooldown = enemyData.sadSandCastle.attackCooldown;
        attackRange = enemyData.sadSandCastle.attackRange;
        attackDamage = enemyData.sadSandCastle.attackDamage;
        attackPointOffsetMultiplier = enemyData.sadSandCastle.attackPointOffsetMultiplier;
        movementStopThreshold = enemyData.sadSandCastle.movementStopThreshold;
    }
    
}
