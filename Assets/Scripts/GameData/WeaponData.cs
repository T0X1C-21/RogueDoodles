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
        public int numberOfBullets;
        public float reloadTime;
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

        [Space(10)]
        [Header("----- PROJECTILE SETTINGS -----")]
        public float minimumAttackRange;
        public float maximumAttackRange;
        public float spawnAnimationDuration;

        [Space(10)]
        [Header("----- FLAMES SETTINGS -----")]
        public Sprite inkSplashFlamesSprite;
        public float playerDetectionRadius;
        public float flamesDuration;
        public float attackCooldown;

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
