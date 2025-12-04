using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [SerializeField] private float minimumSpawnCircleRadius = 1f;
    [SerializeField] private float maximumSpawnCircleRadius = 2f;
    [SerializeField] private bool drawGizmos;

    private float waveCost;
    private float waveTime;
    private int waveNumber = 0;
    private AnimationCurve waveCostIncreaseCurve;
    private AnimationCurve waveTimeIncreaseCurve;
    private Dictionary<EnemyType, int> enemyCostDictionary = new Dictionary<EnemyType, int>();

    private float timer = 0f;

    private void Awake() {
        EnemyData enemyData = DataManager.Instance.GetEnemyData();
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
        int randomEnemyCost = 0;
        float currentWaveCost = waveCost;
        while(currentWaveCost - randomEnemyCost > 0) {
            EnemyType randomEnemy = GetRandomEnemy();
        
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
            }
        }
    }

    private EnemyType GetRandomEnemy() {
        int randomNumber = UnityEngine.Random.Range(0, enemyCostDictionary.Count);
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
        float randomAngle = UnityEngine.Random.Range(0f, 2 * Mathf.PI);
        float x = Mathf.Cos(randomAngle);
        float y = Mathf.Cos(randomAngle);

        float randomDistance = UnityEngine.Random.Range(minimumSpawnCircleRadius, maximumSpawnCircleRadius);

        Vector2 cameraPosition = Camera.main.transform.position;
        Vector2 spawnPosition = cameraPosition + (new Vector2(x, y) * randomDistance);

        GameObject enemyPrefab;
        switch (enemyType) {
            case EnemyType.Balloon:
                enemyPrefab = DataManager.Instance.GetEnemyData().balloon.balloonPrefab;
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                break;
            case EnemyType.CursedChalkStick:
                enemyPrefab = DataManager.Instance.GetEnemyData()
                    .cursedChalkStick.cursedChalkStickPrefab;
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
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
