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
        [Space(10)]
        [Header("----- BASIC SETTIGNS -----")]
        public GameObject balloonPrefab;
        public float moveSpeed;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public float moveSpeedOffset;
        public int maxHealthPoints;
        [Tooltip("This number determines at how much distance the enemy should stop its movement")]
        [Range(0f, 1f)]
        public float movementStopThreshold;

        [Space(10)]
        [Header("----- ATTACK SETTINGS -----")]
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
        [Tooltip("0 - Player position, 0 to 1 - Attack point shifts towards enemy position")]
        [Range(0f, 1f)]
        public float attackPointOffsetMultiplier;
    }

    [System.Serializable]
    public struct CursedChalkStick {
        [Space(10)]
        [Header("----- BASIC SETTIGNS -----")]
        public GameObject cursedChalkStickPrefab;
        public float moveSpeed;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public float moveSpeedOffset;
        public int maxHealthPoints;
        [Tooltip("This number determines at how much distance the enemy should stop its movement")]
        [Range(0f, 1f)]
        public float movementStopThreshold;

        [Space(10)]
        [Header("----- ATTACK SETTINGS -----")]
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
        [Tooltip("0 - Player position, 0 to 1 - Attack point shifts towards enemy position")]
        [Range(0f, 1f)]
        public float attackPointOffsetMultiplier;

        [Space(10)]
        [Header("----- DAMAGE TRAIL SETTINGS -----")]
        public float lifeTimePerDamageTrailPoint;
        public int trailDamageAmount;
        public float trailDamageCooldown;
    }

    [Space(10)]
    [Header("----- WAVE SETTINGS -----")]
    public float minimumSpawnCircleRadius;
    public float maximumSpawnCircleRadius;
    [Tooltip("The higher the number, higher number of enemies spawn at the start")]
    public int firstWaveCost = 10;
    [Tooltip("Time to wait for the next wave at the first wave")]
    public int firstWaveTime = 10;
    [Tooltip("The wave cost increases each wave based on this curve")]
    public AnimationCurve waveCostIncreaseCurve;
    [Tooltip("The wave time increases each wave based on this curve")]
    public AnimationCurve waveTimeIncreaseCurve;
    public List<EnemyCost> enemyCostList;
    
    [Space(10)]
    [Header("----- ENEMY STATS -----")]
    public Balloon balloon;
    public CursedChalkStick cursedChalkStick;
    
}