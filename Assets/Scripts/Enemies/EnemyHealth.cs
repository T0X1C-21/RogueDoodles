using UnityEngine;

public class EnemyHealth : Health {

    private EnemyType enemyType;

    protected override void Awake() {
        enemyType = this.GetComponent<TypeOfEnemy>().GetEnemyType();
        EnemyData enemyData = DataManager.Instance.GetEnemyData();

        switch (enemyType) {
            case EnemyType.Balloon:
                maxHealthPoints = enemyData.balloon.maxHealthPoints;
                break;
            case EnemyType.CursedChalkStick:
                maxHealthPoints = enemyData.balloon.maxHealthPoints;
                break;
            case EnemyType.SadSandCastle:
                maxHealthPoints = enemyData.sadSandCastle.maxHealthPoints;
                break;
        }

        base.Awake();
    }

    public override void TakeDamage(float damageAmount) {
        if(healthPoints - damageAmount <= 0f) {
            this.GetComponent<DropExperience>().SpawnExperienceOrbs();
            healthPoints = 0f;

            switch (enemyType) {
                case EnemyType.Balloon:
                    ObjectPoolManager.SetObjectBackToPool(PoolType.Balloon, this.gameObject);
                    break;
                case EnemyType.CursedChalkStick:
                    ObjectPoolManager.SetObjectBackToPool(PoolType.CursedChalkStick, this.gameObject);
                    break;
                case EnemyType.SadSandCastle:
                    ObjectPoolManager.SetObjectBackToPool(PoolType.SadSandCastle, this.gameObject);
                    break;
            }

            return;
        }

        base.TakeDamage(damageAmount);
    }

    // For pooling enemies
    public void ResetHealth() {
        healthPoints = maxHealthPoints;
    }

}
