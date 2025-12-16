using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "GameData/WeaponData")]
public class WeaponData : ScriptableObject {
    
    [System.Serializable]
    public struct Pencil {

        public GameObject pencilPrefab;
        public Sprite sprite;
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

    [Space(10)]
    [Header("----- BASIC SETTINGS -----")]
    public WeaponType startingWeaponType;
    [Tooltip("SLerp speed of the weapon")]
    [Range(15f, 25f)]
    public float weaponRotationSpeed;

    [Space(10)]
    [Header("----- WEAPON STATS -----")]
    public Pencil pencil;

}
