using UnityEngine;

public enum WeaponType {
    Pencil
}

public enum CharacterType {
    Player,
    Balloon,
    CursedChalkStick
}

public enum EnemyType {
    Balloon,
    CursedChalkStick
}

public class DataManager : Singleton<DataManager> {

    [SerializeField] private PlayerData playerData;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Transform playerTargetTransform;

    public PlayerData GetPlayerData() {
        return playerData;
    }

    public EnemyData GetEnemyData() {
        return enemyData;
    }

    public Transform GetPlayerTargetTransform() {
        return playerTargetTransform;
    }

}
