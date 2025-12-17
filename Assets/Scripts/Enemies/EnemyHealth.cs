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
            case EnemyType.SketchyWorm:
                maxHealthPoints = enemyData.sketchyWorm.maxHealthPoints;
                break;
            case EnemyType.AngryBench:
                this.TryGetComponent(out AngryBench angryBench);
                switch (angryBench.GetAngryBenchVariant()) {
                    case AngryBenchVariant.Large:
                        maxHealthPoints = enemyData.angryBench.largeAngryBenchMaxHealthPoints;
                        break;
                    case AngryBenchVariant.Medium:
                        maxHealthPoints = enemyData.angryBench.mediumAngryBenchMaxHealthPoints;
                        break;
                    case AngryBenchVariant.Small:
                        maxHealthPoints = enemyData.angryBench.smallAngryBenchMaxHealthPoints;
                        break;
                }
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
                case EnemyType.SketchyWorm:
                    ObjectPoolManager.SetObjectBackToPool(PoolType.SketchyWorm, this.gameObject);
                    break;
                case EnemyType.AngryBench:
                    this.TryGetComponent(out AngryBench angryBench);
                    EnemyData enemyData = DataManager.Instance.GetEnemyData();
                    switch (angryBench.GetAngryBenchVariant()) {
                        case AngryBenchVariant.Large:
                            ObjectPoolManager.SetObjectBackToPool(PoolType.LargeAngryBench, this.gameObject);
                            int minimumSplitCount = enemyData.angryBench.mediumAngryBenchMinimumSplitCount;
                            int maximumSplitCount = enemyData.angryBench.mediumAngryBenchMaximumSplitCount;
                            int splitCount = Random.Range(minimumSplitCount, maximumSplitCount + 1);
                            angryBench.SpawnLowerVariant(AngryBenchVariant.Medium, splitCount);
                            break;
                        case AngryBenchVariant.Medium:
                            ObjectPoolManager.SetObjectBackToPool(PoolType.MediumAngryBench, this.gameObject);
                            minimumSplitCount = enemyData.angryBench.smallAngryBenchMinimumSplitCount;
                            maximumSplitCount = enemyData.angryBench.smallAngryBenchMaximumSplitCount;
                            splitCount = Random.Range(minimumSplitCount, maximumSplitCount + 1);
                            angryBench.SpawnLowerVariant(AngryBenchVariant.Small, splitCount);
                            break;
                        case AngryBenchVariant.Small:
                            ObjectPoolManager.SetObjectBackToPool(PoolType.SmallAngryBench, this.gameObject);
                            break;
                    }
                    break;
            }

            return;
        }

        healthPoints -= damageAmount;
    }

    // For pooling enemies
    public void ResetHealth() {
        healthPoints = maxHealthPoints;
    }

}
