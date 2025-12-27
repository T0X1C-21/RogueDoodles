using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "GameData/UpgradeData")]
public class UpgradeData : ScriptableObject {

    [System.Serializable]
    public struct GearCog {

        public float LevelOne_AttackSpeedBuff;
        public float LevelTwo_AttackSpeedBuff;
        public float LevelThree_AttackSpeedBuff;

    }

    [Space(10)]
    [Header("----- UPGRADES STATS -----")]
    public GearCog gearCog;

}