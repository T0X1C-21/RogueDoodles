using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/PlayerData")]
public class PlayerData : ScriptableObject {

    [Header("----- PLAYER STATS -----")]
    public float moveSpeed = 3f;
    public int maxHealthPoints = 100;
    public WeaponType startingWeapon;
    public float aimWeaponRadius;

    [System.Serializable]
    public struct Pencil {

        public Sprite sprite;
        public int damage;
        public float attackCooldown;
        public float attackRange;

    }

    [Space(10)]
    [Header("----- WEAPON STATS -----")]
    public Pencil pencil;

}
