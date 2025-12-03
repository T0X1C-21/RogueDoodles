using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "GameData/EnemyData")]
public class EnemyData : ScriptableObject {

    [System.Serializable]
    public struct EnemyCost {

        public EnemyType enemy;
        public int cost;

    }

    [System.Serializable]
    public struct Balloon {
        public float moveSpeed;
        public int maxHealthPoints;
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
    }

    [Header("----- WAVE SETTINGS -----")]
    [Tooltip("The higher the number, higher number of enemies spawn at the start")]
    public int firstWaveCost = 10;
    [Tooltip("Time to wait for the next wave at the first wave")]
    public int firstWaveTime = 10;
    [Tooltip("The wave cost increases each wave based on this curve")]
    public AnimationCurve waveCostIncreaseCurve;
    [Tooltip("The wave time increases each wave based on this curve")]
    public AnimationCurve waveTimeIncreaseCurve;
    public List<EnemyCost> enemyCostList;
    
    [Header("----- ENEMY STATS -----")]
    public Balloon balloon;
    
}
