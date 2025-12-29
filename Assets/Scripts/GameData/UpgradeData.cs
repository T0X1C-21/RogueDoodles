using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "GameData/UpgradeData")]
public class UpgradeData : ScriptableObject {

    [System.Serializable]
    public struct GearCog {

        public float LevelOne_AttackSpeedToMultiply;
        public float LevelTwo_AttackSpeedToMultiply;
        public float LevelThree_AttackSpeedToMultiply;

    }

    [System.Serializable]
    public struct InkOverflow {

        public int LevelOne_ProjectileCountToAdd;
        public int LevelTwo_ProjectileCountToAdd;
        public int LevelThree_ProjectileCountToAdd;

    }

    [System.Serializable]
    public struct RulerEdge {

        public int LevelOne_PiercingToAdd;
        public int LevelTwo_PiercingToAdd;
        public int LevelThree_PiercingToAdd;

    }

    [Space(10)]
    [Header("----- UPGRADES STATS -----")]
    public GearCog gearCog;
    public InkOverflow inkOverflow;
    public RulerEdge rulerEdge;

}