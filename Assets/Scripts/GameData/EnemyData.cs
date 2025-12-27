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
        [Header("----- BASIC SETTINGS -----")]
        public GameObject balloonPrefab;
        public float moveSpeed;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public float moveSpeedOffset;
        public int maxHealthPoints;
        [Tooltip("This number determines at how much distance the enemy should stop its movement")]
        public float movementStopThreshold;

        [Space(10)]
        [Header("----- ATTACK SETTINGS -----")]
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
        [Tooltip("0 - Player position, 0 to 1 - Attack point shifts towards enemy position")]
        public float attackPointOffsetMultiplier;

    }

    [System.Serializable]
    public struct CursedChalkStick {

        [Space(10)]
        [Header("----- BASIC SETTINGS -----")]
        public GameObject cursedChalkStickPrefab;
        public float moveSpeed;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public float moveSpeedOffset;
        public int maxHealthPoints;
        [Tooltip("This number determines at how much distance the enemy should stop its movement")]
        public float movementStopThreshold;

        [Space(10)]
        [Header("----- ATTACK SETTINGS -----")]
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
        [Tooltip("0 - Player position, 0 to 1 - Attack point shifts towards enemy position")]
        public float attackPointOffsetMultiplier;

        [Space(10)]
        [Header("----- DAMAGE TRAIL SETTINGS -----")]
        public float lifeTimePerDamageTrailPoint;
        public int trailDamageAmount;
        public float trailDamageCooldown;

    }

    [System.Serializable]
    public struct SadSandCastle {

        [Space(10)]
        [Header("----- BASIC SETTINGS -----")]
        public GameObject sadSandCastlePrefab;
        public float moveSpeed;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public float moveSpeedOffset;
        public int maxHealthPoints;
        [Tooltip("This number determines at how much distance the enemy should stop its movement")]
        public float movementStopThreshold;

        [Space(10)]
        [Header("----- ATTACK SETTINGS -----")]
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
        [Tooltip("0 - Player position, 0 to 1 - Attack point shifts towards enemy position")]
        public float attackPointOffsetMultiplier;

    }

    [System.Serializable]
    public struct SketchyWorm {

        [Space(10)]
        [Header("----- BASIC SETTINGS -----")]
        public GameObject sketchyWormPrefab;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public int maxHealthPoints;

        [Space(10)]
        [Header("----- ATTACK SETTINGS -----")]
        [Tooltip("Time for the worm to stay hidden")]
        public float timeToStayHidden;
        [Tooltip("Offset for time to stay hidden")]
        public float timeToStayHiddenOffset;
        [Tooltip("Time for the worm to shoot projectile")]
        public float attackCooldown;
        [Tooltip("Time for the worm to disappear after shooting")]
        public float timeToDisappear;
        [Tooltip("Offset for time to disappear")]
        public float timeToDisappearOffset;
        public float popUpNearPlayerRadius;

        [Space(10)]
        [Header("----- PROJECTILE SETTINGS -----")]
        public GameObject sketchyWormProjectilePrefab;
        [Tooltip("Move speed of the projectile")]
        public float moveSpeed;
        [Tooltip("Damage of the projectile")]
        public float attackDamage;
        [Tooltip("Radius to detect the target")]
        public float targetDetectionRadius;
        public float autoDestroySelfTimer;

    }

    [System.Serializable]
    public struct AngryBench {

        [Space(10)]
        [Header("----- BASIC SETTIGNS -----")]
        public GameObject largeAngryBenchPrefab;
        public GameObject mediumAngryBenchPrefab;
        public GameObject smallAngryBenchPrefab;
        public float largeAngryBenchMoveSpeed;
        public float mediumAngryBenchMoveSpeed;
        public float smallAngryBenchMoveSpeed;
        [Tooltip("This will be added or subtracted to the MoveSpeed to add random move speed for each enemy")]
        public float moveSpeedOffset;
        public int largeAngryBenchMaxHealthPoints;
        public int mediumAngryBenchMaxHealthPoints;
        public int smallAngryBenchMaxHealthPoints;
        [Tooltip("This number determines at how much distance the enemy should stop its movement")]
        public float movementStopThreshold;

        [Space(10)]
        [Header("----- ATTACK SETTINGS -----")]
        public float attackCooldown;
        public float attackRange;
        public float attackDamage;
        [Tooltip("0 - Player position, 0 to 1 - Attack point shifts towards enemy position")]
        public float attackPointOffsetMultiplier;

        [Space(10)]
        [Header("----- SPAWN ANIMATION SETTINGS -----")]
        public float animationDuration;
        public AnimationCurve animationHeightCurve;

        [Space(10)]
        [Header("----- SPECIAL BEHAVIOUR SETTINGS -----")]
        public int mediumAngryBenchMinimumSplitCount;
        public int mediumAngryBenchMaximumSplitCount;
        public int smallAngryBenchMinimumSplitCount;
        public int smallAngryBenchMaximumSplitCount;

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
    public LayerMask enemyLayerMask;
    public Balloon balloon;
    public CursedChalkStick cursedChalkStick;
    public SadSandCastle sadSandCastle;
    public SketchyWorm sketchyWorm;
    public AngryBench angryBench;
    
}