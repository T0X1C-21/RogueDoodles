using UnityEngine;

public enum PrimaryWeaponType {
    Pencil,
    ChalkShot,
    MopSwipe
}

public enum SecondaryWeaponType {
    InkSplash,
    CrayonMissile,
    NotebookTear
}

public enum PlayerType {
    ScribbleKid
}

public enum EnemyType {
    Balloon,
    CursedChalkStick,
    SadSandCastle,
    SketchyWorm,
    AngryBench
}

public enum InkBlobSize {
    Small,
    Medium,
    Large
}

public enum PoolType {
    SmallInkBlob,
    MediumInkBlob,
    LargeInkBlob,
    Balloon,
    CursedChalkStick,
    SadSandCastle,
    SketchyWorm,
    SketchyWormProjectile,
    LargeAngryBench,
    MediumAngryBench,
    SmallAngryBench,
    ChalkShotProjectile,
    InkSplashProjectile,
    CrayonMissileProjectile,
    MopSwipeBubbleProjectile,
    NotebookTearProjectile
}

public enum ProjectileHitType {
    Enemy,
    Player
}

public enum AngryBenchVariant {
    Large,
    Medium,
    Small
}

public enum PassiveType {
    AttackSpeedPlusPlus,
    ProjectileCountPlusPlus,
    PiercingPlusPlus,
    SizePlusPlus,
    AttackDamagePlusPlus,
    RerollPlusPlus,
    LuckPlusPlus
}

public enum UserInterfaceUpgradePool {
    Pencil,
    ChalkShot,
    MopSwipe,
    InkSplash,
    CrayonMissile,
    NotebookTear,
    AttackSpeedPlusPlus,
    ProjectileCountPlusPlus,
    PiercingPlusPlus,
    SizePlusPlus,
    AttackDamagePlusPlus,
    RerollPlusPlus,
    LuckPlusPlus
}

public class DataManager : Singleton<DataManager> {

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private ExperienceData experienceData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UpgradeData upgradeData;
    [SerializeField] private UserInterfaceData userInterfaceData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform playerTargetTransform;

    public EnemyData GetEnemyData() {
        return enemyData;
    }

    public ExperienceData GetExperienceData() {
        return experienceData;
    }

    public PlayerData GetPlayerData() {
        return playerData;
    }

    public UpgradeData GetUpgradeData() {
        return upgradeData;
    }

    public UserInterfaceData GetUserInterfaceData() {
        return userInterfaceData;
    }

    public WeaponData GetWeaponData() {
        return weaponData;
    }

    public Transform GetPlayerTargetTransform() {
        return playerTargetTransform;
    }
    
}