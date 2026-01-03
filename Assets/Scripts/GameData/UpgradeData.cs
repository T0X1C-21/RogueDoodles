using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "GameData/UpgradeData")]
public class UpgradeData : ScriptableObject {

    [System.Serializable]
    public struct AttackSpeedPlusPlus {

        public float AttackSpeedToMultiply_LevelOne;
        public float AttackSpeedToMultiply_LevelTwo;
        public float AttackSpeedToMultiply_LevelThree;

    }

    [System.Serializable]
    public struct ProjectileCountPlusPlus {

        public int ProjectileCountToAdd_LevelOne;
        public int ProjectileCountToAdd_LevelTwo;
        public int ProjectileCountToAdd_LevelThree;

    }

    [System.Serializable]
    public struct PiercingPlusPlus {

        public int PiercingToAdd_LevelOne;
        public int PiercingToAdd_LevelTwo;
        public int PiercingToAdd_LevelThree;

    }

    [System.Serializable]
    public struct SizePlusPlus {

        public float SizeToMultiply_LevelOne;
        public float SizeToMultiply_LevelTwo;
        public float SizeToMultiply_LevelThree;

    }

    [System.Serializable]
    public struct AttackDamagePlusPlus {

        public float AttackDamageToMultiply_LevelOne;
        public float AttackDamageToMultiply_LevelTwo;
        public float AttackDamageToMultiply_LevelThree;

    }

    [Space(10)]
    [Header("----- PASSIVES STATS -----")]
    public AttackSpeedPlusPlus attackSpeedPlusPlus;
    public ProjectileCountPlusPlus projectileCountPlusPlus;
    public PiercingPlusPlus piercingPlusPlus;
    public SizePlusPlus sizePlusPlus;
    public AttackDamagePlusPlus attackDamagePlusPlus;

}