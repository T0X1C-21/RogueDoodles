using UnityEngine;

public enum PrimaryWeaponType {
    Pencil,
    ChalkShot
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
    CrayonMissileProjectile
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

public class DataManager : Singleton<DataManager> {

    [SerializeField] private PlayerData playerData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private ExperienceData experienceData;
    [SerializeField] private Transform playerTargetTransform;

    public PlayerData GetPlayerData() {
        return playerData;
    }

    public WeaponData GetWeaponData() {
        return weaponData;
    }

    public EnemyData GetEnemyData() {
        return enemyData;
    }

    public ExperienceData GetExperienceData() {
        return experienceData;
    }

    public Transform GetPlayerTargetTransform() {
        return playerTargetTransform;
    }

}