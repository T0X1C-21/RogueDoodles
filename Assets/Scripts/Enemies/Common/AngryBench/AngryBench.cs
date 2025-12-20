using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AngryBench : Enemy {

    private static int angryBenchesAnimationDirection = 1;
    private float animationDuration;
    private AnimationCurve animationHeightCurve;

    [SerializeField] private AngryBenchVariant angryBenchVariant;
    
    protected override void Awake() {
        base.Awake();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        attackCooldown = enemyData.angryBench.attackCooldown;
        attackRange = enemyData.angryBench.attackRange;
        attackDamage = enemyData.angryBench.attackDamage;
        attackPointOffsetMultiplier = enemyData.angryBench.attackPointOffsetMultiplier;
        movementStopThreshold = enemyData.angryBench.movementStopThreshold;
        switch (angryBenchVariant) {
            case AngryBenchVariant.Large:
                moveSpeed = enemyData.angryBench.largeAngryBenchMoveSpeed;
                break;
            case AngryBenchVariant.Medium:
                moveSpeed = enemyData.angryBench.mediumAngryBenchMoveSpeed;
                break;
            case AngryBenchVariant.Small:
                moveSpeed = enemyData.angryBench.smallAngryBenchMoveSpeed;
                break;
        }
        moveSpeed += Random.Range(-enemyData.angryBench.moveSpeedOffset, enemyData.angryBench.moveSpeedOffset);

        if(angryBenchVariant == AngryBenchVariant.Medium || angryBenchVariant == AngryBenchVariant.Small) {
            animationDuration = enemyData.angryBench.animationDuration;
            animationHeightCurve = enemyData.angryBench.animationHeightCurve;
            StartCoroutine(LowerAngryBenchesSpawnAnimation());
        }
    }

    private IEnumerator LowerAngryBenchesSpawnAnimation() {
        float originalOrbPositionX = this.transform.position.x;
        float originalOrbPositionY = this.transform.position.y;
        float targetOrbPositionX = (Random.Range(0.1f, 1f) * angryBenchesAnimationDirection) + originalOrbPositionX;
        angryBenchesAnimationDirection = (angryBenchesAnimationDirection == 1) ? -1 : 1;
        float randomHeightMultiplier = Random.Range(0.25f, 1.25f);
        float randomAnimationDurationMultiplier = Random.Range(0.8f, 1.2f);
        float t = 0f;
        while(t < 1) {
            t += Time.deltaTime / animationDuration * randomAnimationDurationMultiplier;
            float animationX = Mathf.Lerp(originalOrbPositionX, targetOrbPositionX, t);
            float animationY = (animationHeightCurve.Evaluate(t) * randomHeightMultiplier) + originalOrbPositionY;
            this.transform.position = new Vector3(animationX, animationY);
            yield return null;
        }
    }

    public AngryBenchVariant GetAngryBenchVariant() {
        return angryBenchVariant;
    }

    public void SpawnLowerVariant(AngryBenchVariant angryBenchVariant, int count) {
        if(angryBenchVariant == AngryBenchVariant.Large) {
            Debug.LogWarning("Cannot spawn LargeAngryBench as a lower variant!");
            return;
        }
        
        PoolType poolType = default;
        GameObject prefab = default;
        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        if(angryBenchVariant == AngryBenchVariant.Medium) {
            poolType = PoolType.MediumAngryBench;
            prefab = enemyData.angryBench.mediumAngryBenchPrefab;
        } else if(angryBenchVariant == AngryBenchVariant.Small) {
            poolType = PoolType.SmallAngryBench;
            prefab = enemyData.angryBench.smallAngryBenchPrefab;
        }

        for(int i = 0; i < count; i++) {
            ObjectPoolManager.GetObjectFromPool(poolType, prefab, this.transform.position, Quaternion.identity);
        }
    }

}
