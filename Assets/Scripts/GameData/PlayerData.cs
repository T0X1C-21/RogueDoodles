using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/PlayerData")]
public class PlayerData : ScriptableObject {

    [Header("----- PLAYER STATS -----")]
    public float moveSpeed = 3f;
    public int maxHealthPoints = 100;
    [Tooltip("Health regeneration rate per second")]
    public float healthRegenRate = 0.2f;
    [Tooltip("Time until health regeneration")]
    public float timeToStartHealing = 10f;
    public WeaponType startingWeapon;

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
