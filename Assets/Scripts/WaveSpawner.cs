using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

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
        List<string> enemies = new List<string>();
        int randomEnemyCost = 0;
        float currentWaveCost = waveCost;
        while(currentWaveCost - randomEnemyCost > 0) {
            EnemyType randomEnemy = GetRandomEnemy();
        
            switch (randomEnemy) {
                case EnemyType.Balloon:
                    randomEnemyCost = enemyCostDictionary[EnemyType.Balloon];
                    currentWaveCost -= randomEnemyCost;
                    enemies.Add("balloon");
                    break;
                case EnemyType.X:
                    randomEnemyCost = enemyCostDictionary[EnemyType.X];
                    currentWaveCost -= randomEnemyCost;
                    enemies.Add("X");
                    break;
                case EnemyType.Y:
                    randomEnemyCost = enemyCostDictionary[EnemyType.Y];
                    currentWaveCost -= randomEnemyCost;
                    enemies.Add("Y");
                    break;
            }
        }
        
        foreach(string enemy in enemies) {
            Debug.Log(enemy);
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

}
