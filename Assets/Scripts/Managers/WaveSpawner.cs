using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [SerializeField] private bool drawGizmos;

    private float minimumSpawnCircleRadius;
    private float maximumSpawnCircleRadius;
    private float waveCost;
    private float waveTime;
    private int waveNumber = 0;
    private AnimationCurve waveCostIncreaseCurve;
    private AnimationCurve waveTimeIncreaseCurve;
    private Dictionary<EnemyType, int> enemyCostDictionary = new Dictionary<EnemyType, int>();

    private float timer = 0f;

    private void Awake() {
        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        minimumSpawnCircleRadius = enemyData.minimumSpawnCircleRadius;
        maximumSpawnCircleRadius = enemyData.maximumSpawnCircleRadius;
        waveCost = enemyData.firstWaveCost;
        waveTime = enemyData.firstWaveTime;
        waveCostIncreaseCurve = enemyData.waveCostIncreaseCurve;
        waveTimeIncreaseCurve = enemyData.waveTimeIncreaseCurve;

        for(int i = 0; i < enemyData.enemyCostList.Count; i++) {
            EnemyData.EnemyCost enemyCostList = enemyData.enemyCostList[i];
            enemyCostDictionary.Add(enemyCostList.enemy, enemyCostList.cost);
        }
    }

    private void Update() {
        if(timer <= 0f) {
            SpawnWave();
            timer = waveTime;
            waveNumber += 1;
            waveTime *= waveTimeIncreaseCurve.Evaluate(waveNumber);
            waveCost *= waveCostIncreaseCurve.Evaluate(waveNumber);
        } else {
            timer -= Time.deltaTime;
        }
    }

    private void SpawnWave() {
        EnemyType randomEnemy = GetRandomEnemy();
        float currentWaveCost = waveCost;
        int randomEnemyCost = enemyCostDictionary[randomEnemy];

        if(currentWaveCost - randomEnemyCost <= 0) {
            randomEnemyCost = enemyCostDictionary[EnemyType.Balloon];
            randomEnemy = EnemyType.Balloon; 
        }

        while(currentWaveCost - randomEnemyCost > 0) {
            switch (randomEnemy) {
                case EnemyType.Balloon:
                    randomEnemyCost = enemyCostDictionary[EnemyType.Balloon];
                    currentWaveCost -= randomEnemyCost;
                    SpawnEnemy(EnemyType.Balloon);
                    break;
                case EnemyType.CursedChalkStick:
                    randomEnemyCost = enemyCostDictionary[EnemyType.CursedChalkStick];
                    currentWaveCost -= randomEnemyCost;
                    SpawnEnemy(EnemyType.CursedChalkStick);
                    break;
                case EnemyType.SadSandCastle:
                    randomEnemyCost = enemyCostDictionary[EnemyType.SadSandCastle];
                    currentWaveCost -= randomEnemyCost;
                    SpawnEnemy(EnemyType.SadSandCastle);
                    break;
                case EnemyType.SketchyWorm:
                    randomEnemyCost = enemyCostDictionary[EnemyType.SketchyWorm];
                    currentWaveCost -= randomEnemyCost;
                    SpawnEnemy(EnemyType.SketchyWorm);
                    break;
                case EnemyType.AngryBench:
                    randomEnemyCost = enemyCostDictionary[EnemyType.AngryBench];
                    currentWaveCost -= randomEnemyCost;
                    SpawnEnemy(EnemyType.AngryBench);
                    break;
            }

            randomEnemy = GetRandomEnemy();
            randomEnemyCost = enemyCostDictionary[randomEnemy];
        }
    }

    private EnemyType GetRandomEnemy() {
        int randomNumber = Random.Range(0, enemyCostDictionary.Count);
        int i = 0;

        foreach(EnemyType enemy in enemyCostDictionary.Keys) {
            if(i == randomNumber) {
                return enemy;
            }
            i++;
        }
        return EnemyType.Balloon;
    }

    private void SpawnEnemy(EnemyType enemyType) {
        float randomDistance = Random.Range(minimumSpawnCircleRadius, maximumSpawnCircleRadius);

        float randomAngle = Random.Range(0f, 2 * Mathf.PI);
        float x = randomDistance * Mathf.Cos(randomAngle);
        float y = randomDistance * Mathf.Sin(randomAngle);

        Vector2 cameraPosition = Camera.main.transform.position;
        Vector2 spawnPosition = cameraPosition + (new Vector2(x, y));

        GameObject enemyPrefab;
        GameObject spawnedEnemy = null;
        switch (enemyType) {
            case EnemyType.Balloon:
                enemyPrefab = DataManager.Instance.GetEnemyData().balloon.balloonPrefab;
                spawnedEnemy = ObjectPoolManager.GetObjectFromPool(PoolType.Balloon, enemyPrefab, spawnPosition,
                    Quaternion.identity);
                break;
            case EnemyType.CursedChalkStick:
                enemyPrefab = DataManager.Instance.GetEnemyData()
                    .cursedChalkStick.cursedChalkStickPrefab;
                spawnedEnemy = ObjectPoolManager.GetObjectFromPool(PoolType.CursedChalkStick, enemyPrefab,
                    spawnPosition, Quaternion.identity);
                break;
            case EnemyType.SadSandCastle:
                enemyPrefab = DataManager.Instance.GetEnemyData()
                    .sadSandCastle.sadSandCastlePrefab;
                spawnedEnemy = ObjectPoolManager.GetObjectFromPool(PoolType.SadSandCastle, enemyPrefab,
                    spawnPosition, Quaternion.identity);
                break;
            case EnemyType.SketchyWorm:
                enemyPrefab = DataManager.Instance.GetEnemyData()
                    .sketchyWorm.sketchyWormPrefab;
                spawnedEnemy = ObjectPoolManager.GetObjectFromPool(PoolType.SketchyWorm, enemyPrefab,
                    spawnPosition, Quaternion.identity);
                break;
            case EnemyType.AngryBench:
                enemyPrefab = DataManager.Instance.GetEnemyData()
                    .angryBench.largeAngryBenchPrefab;
                spawnedEnemy = ObjectPoolManager.GetObjectFromPool(PoolType.LargeAngryBench, enemyPrefab,
                    spawnPosition, Quaternion.identity);
                break;
        }
    }

    public void Editor_SpawnEnemy(EnemyType enemyType) {
        switch (enemyType) {
            case EnemyType.Balloon:
                SpawnEnemy(EnemyType.Balloon);
                break;
            case EnemyType.CursedChalkStick:
                SpawnEnemy(EnemyType.CursedChalkStick);
                break;
            case EnemyType.SadSandCastle:
                SpawnEnemy(EnemyType.SadSandCastle);
                break;
            case EnemyType.SketchyWorm:
                SpawnEnemy(EnemyType.SketchyWorm);
                break;
            case EnemyType.AngryBench:
                SpawnEnemy(EnemyType.AngryBench);
                break;
        }
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Camera.main.transform.position, minimumSpawnCircleRadius);
        Gizmos.color = Color.crimson;
        Gizmos.DrawWireSphere(Camera.main.transform.position, maximumSpawnCircleRadius);
    }

}