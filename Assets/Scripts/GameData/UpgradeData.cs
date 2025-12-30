using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "GameData/UpgradeData")]
public class UpgradeData : ScriptableObject {

    [System.Serializable]
    public struct AttackSpeedPlusPlus {

        public float LevelOne_AttackSpeedToMultiply;
        public float LevelTwo_AttackSpeedToMultiply;
        public float LevelThree_AttackSpeedToMultiply;

    }

    [System.Serializable]
    public struct ProjectileCountPlusPlus {

        public int LevelOne_ProjectileCountToAdd;
        public int LevelTwo_ProjectileCountToAdd;
        public int LevelThree_ProjectileCountToAdd;

    }

    [System.Serializable]
    public struct PiercingPlusPlus {

        public int LevelOne_PiercingToAdd;
        public int LevelTwo_PiercingToAdd;
        public int LevelThree_PiercingToAdd;

    }

    [System.Serializable]
    public struct SizePlusPlus {

        public float LevelOne_SizeToMultiply;
        public float LevelTwo_SizeToMultiply;
        public float LevelThree_SizeToMultiply;

    }

    [System.Serializable]
    public struct AttackDamagePlusPlus {

        public float LevelOne_AttackDamageToMultiply;
        public float LevelTwo_AttackDamageToMultiply;
        public float LevelThree_AttackDamageToMultiply;

    }

    [Space(10)]
    [Header("----- UPGRADES STATS -----")]
    public AttackSpeedPlusPlus attackSpeedPlusPlus;
    public ProjectileCountPlusPlus projectileCountPlusPlus;
    public PiercingPlusPlus piercingPlusPlus;
    public SizePlusPlus sizePlusPlus;
    public AttackDamagePlusPlus attackDamagePlusPlus;

}