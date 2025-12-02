using UnityEngine;

public enum WeaponType {
    Pencil
}

public enum CharacterType {
    Player,
    Balloon
}

public class DataManager : Singleton<DataManager> {

    [SerializeField] private PlayerData playerData;
    [SerializeField] private EnemyData enemyData;

    public PlayerData GetPlayerData() {
        return playerData;
    }

    public EnemyData GetEnemyData() {
        return enemyData;
    }

}
