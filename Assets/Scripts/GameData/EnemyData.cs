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
        public GameObject balloonPrefab;
        public float moveSpeed;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public float moveSpeedOffset;
        public int maxHealthPoints;
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
        [Tooltip("0 - Player position, 0 to 1 - Attack point shifts towards enemy position")]
        [Range(0f, 1f)]
        public float attackPointOffsetMultiplier;
        [Tooltip("This number determines at how much distance the enemy should stop its movement")]
        [Range(0f, 1f)]
        public float movementStopThreshold;
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
    }

    [System.Serializable]
    public struct CursedChalkStick {
        public GameObject cursedChalkStickPrefab;
        public float moveSpeed;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public float moveSpeedOffset;
        public int maxHealthPoints;
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
        [Tooltip("0 - Player position, 0 to 1 - Attack point shifts towards enemy position")]
        [Range(0f, 1f)]
        public float attackPointOffsetMultiplier;
        [Tooltip("This number determines at how much distance the enemy should stop its movement")]
        [Range(0f, 1f)]
        public float movementStopThreshold;
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
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
    public CursedChalkStick cursedChalkStick;
    
}
