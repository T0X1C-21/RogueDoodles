using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GameData/PlayerData")]
public class PlayerData : ScriptableObject {

    [System.Serializable]
    public struct ScribbleKid {

        public float moveSpeed;
        public int maxHealthPoints;
        public PrimaryWeaponType startingWeapon;

    }

    [Space(10)]
    [Header("----- PLAYER STATS -----")]
    public LayerMask playerLayerMask;
    public ScribbleKid scribbleKid;

}