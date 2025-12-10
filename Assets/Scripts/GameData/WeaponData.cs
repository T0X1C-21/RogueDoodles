using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "GameData/WeaponData")]
public class WeaponData : ScriptableObject {
    
    [System.Serializable]
    public struct Pencil {

        public Sprite sprite;
        public int damage;
        public float attackCooldown;
        public float attackRange;
        [Tooltip("The higher this number, the larger the attack arc.")]
        [Range(-1f, 1f)]
        public float attackArcThreshold;
        public float preAnimationTime;
        public float animationTime;

    }

    [Space(10)]
    [Header("----- WEAPON STATS -----")]
    public Pencil pencil;

}
