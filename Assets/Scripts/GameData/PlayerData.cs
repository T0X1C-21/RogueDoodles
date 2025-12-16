using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/PlayerData")]
public class PlayerData : ScriptableObject {

    [System.Serializable]
    public struct ScribbleKid {
        public float moveSpeed;
        public int maxHealthPoints;
        public WeaponType startingWeapon;
        public float aimWeaponRadius;
    }

    [Space(10)]
    [Header("----- PLAYER STATS -----")]
    public LayerMask playerLayerMask;
    public ScribbleKid scribbleKid;

}