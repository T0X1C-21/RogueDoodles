using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "GameData/WeaponData")]
public class WeaponData : ScriptableObject {
    
    [System.Serializable]
    public struct Pencil {

        public GameObject pencilPrefab;
        public int damage;
        public int piercing;
        public float attackCooldown;
        public float attackRange;
        [Tooltip("The higher this number, the larger the attack arc.")]
        [Range(-1f, 1f)]
        public float attackArcThreshold;
        public float preAnimationTime;
        public float animationTime;

    }

    [System.Serializable]
    public struct ChalkShot {

        public GameObject chalkShotPrefab;
        public GameObject chalkShotBulletPrefab;
        public int damage;
        public int piercing;
        public float attackCooldown;
        public float preAnimationTime;
        public float animationTime;

        [Space(10)]
        [Header("----- PROJECTILE SETTINGS -----")]
        public float moveSpeed;
        public float damageAmount;
        public float targetDetectionRadius;
        public float autoDestroySelfTimer;

    }

    [System.Serializable]
    public struct InkSplash {

        public GameObject inkSplashPrefab;
        public GameObject inkSplashProjectilePrefab;
        public int damage;
        public float attackCooldown;

        [Space(10)]
        [Header("----- PROJECTILE SETTINGS -----")]
        public float playerDetectionRadius;
        public float minimumAttackRange;
        public float maximumAttackRange;
        public float spawnAnimationDuration;
        public AnimationCurve heightAnimationCurve;

    }

    [Space(10)]
    [Header("----- BASIC SETTINGS -----")]
    public WeaponType startingWeaponType;
    public float aimWeaponRadius;
    [Tooltip("SLerp speed of the weapon")]
    [Range(15f, 25f)]
    public float weaponRotationSpeed;

    [Space(10)]
    [Header("----- WEAPON STATS -----")]
    public Pencil pencil;
    public ChalkShot chalkShot;
    public InkSplash inkSplash;

}
