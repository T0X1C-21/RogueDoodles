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
        int randomCost = 0;
        float currentWaveCost = waveCost;
        while(currentWaveCost - randomCost > 0) {
            int randomNumber = Random.Range(0, 3);
        
            switch (randomNumber) {
                case 0:
                    randomCost = enemyCostDictionary[EnemyType.Balloon];
                    currentWaveCost -= randomCost;
                    enemies.Add("balloon");
                    break;
                case 1:
                    randomCost = enemyCostDictionary[EnemyType.X];
                    currentWaveCost -= randomCost;
                    enemies.Add("X");
                    break;
                case 2:
                    randomCost = enemyCostDictionary[EnemyType.Y];
                    currentWaveCost -= randomCost;
                    enemies.Add("Y");
                    break;
            }
        }
        
        foreach(string enemy in enemies) {
            Debug.Log(enemy);
        }
    }

}
