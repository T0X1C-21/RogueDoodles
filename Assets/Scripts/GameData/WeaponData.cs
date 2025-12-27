using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "GameData/WeaponData")]
public class WeaponData : ScriptableObject {
    
    [System.Serializable]
    public struct Pencil {

        public GameObject pencilPrefab;
        public int damage;
        public int piercing;
        public float attackCooldown;
        [Tooltip("The higher this number, the larger the attack arc.")]
        public float preAnimationTime;
        public float animationTime;

    }

    [System.Serializable]
    public struct ChalkShot {

        public GameObject chalkShotPrefab;
        public GameObject chalkShotBulletPrefab;
        public int numberOfBullets;
        public float reloadTime;
        public int piercing;
        public float attackCooldown;
        public float preAnimationTime;
        public float animationTime;
        public float recoilStrength;

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

        [Space(10)]
        [Header("----- PROJECTILE SETTINGS -----")]
        public int damage;
        public float minimumAttackRange;
        public float maximumAttackRange;
        public float spawnAnimationDuration;

        [Space(10)]
        [Header("----- FLAMES SETTINGS -----")]
        public Sprite inkSplashFlamesSprite;
        public float enemyDetectionRadius;
        public float flamesDuration;
        public float attackCooldown;

    }

    [System.Serializable]
    public struct CrayonMissile {

        public GameObject crayonMissilePrefab;
        public GameObject crayonMissileProjectilePrefab;
        public float attackCooldown;

        [Space(10)]
        [Header("----- PROJECTILE SETTINGS -----")]
        public float moveSpeed;
        public int damageAmount;
        public float targetDetectionRadius;
        public float autoDestroySelfTimer;

    }

    [System.Serializable]
    public struct NotebookTear {

        public GameObject notebookTearPrefab;
        public float revolutionSpeed;
        public float revolutionRadius;
        public float rotationSpeed;
        public float autoStartRevolutionTime;
        public float autoEndRevolutionTime;
        public float animationTime;
        public float fadeOutTime;

        [Space(10)]
        [Header("----- ATTACK SETTINGS -----")]
        public float attackCooldown;
        public int attackDamage;
        public float attackRadius;
        public int piercing;

    }

    [System.Serializable]
    public struct MopSwipe{

        public GameObject mopSwipePrefab;
        public GameObject mopSwipeBubbleProjectilePrefab;
        public float attackCooldown;
        public float arcSwipeAmount;
        public int numberOfBubbles;

        [Space(10)]
        [Header("----- ANIMATION SETTINGS -----")]
        public float preAnimationTime;
        public float animationTime;
        
        [Space(10)]
        [Header("----- PROJECTILE SETTINGS -----")]
        public float moveSpeed;
        public int damageAmount;
        public int piercing;
        public float targetDetectionRadius;
        public float autoDestroySelfTimer;

    }

    [Space(10)]
    [Header("----- BASIC SETTINGS -----")]
    public PrimaryWeaponType startingPrimaryWeaponType;
    public SecondaryWeaponType[] startingSecondaryWeaponTypes;
    public float aimWeaponRadius;
    [Tooltip("SLerp speed of the weapon")]
    public float weaponRotationSpeed;

    [Space(10)]
    [Header("----- PRIMARY WEAPON STATS -----")]
    public Pencil pencil;
    public ChalkShot chalkShot;
    public MopSwipe mopSwipe;

    [Space(10)]
    [Header("----- SECONDARY WEAPON STATS -----")]
    public InkSplash inkSplash;
    public CrayonMissile crayonMissile;
    public NotebookTear notebookTear;

}
