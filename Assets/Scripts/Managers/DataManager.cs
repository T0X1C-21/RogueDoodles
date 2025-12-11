using UnityEngine;

public enum WeaponType {
    Pencil
}

public enum PlayerType {
    ScribbleKid
}

public enum EnemyType {
    Balloon,
    CursedChalkStick
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
    CursedChalkStick
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