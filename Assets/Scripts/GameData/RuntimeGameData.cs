using System.Collections.Generic;
using UnityEngine;
using static EnemyData;
using static ExperienceData;
using static PlayerData;
using static UpgradeData;

public class EnemyData_Runtime {

    public float minimumSpawnCircleRadius;
    public float maximumSpawnCircleRadius;
    public int firstWaveCost;
    public int firstWaveTime;
    public AnimationCurve waveCostIncreaseCurve;
    public AnimationCurve waveTimeIncreaseCurve;
    public List<EnemyCost> enemyCostList;
    
    public LayerMask enemyLayerMask;
    public EnemyData.Balloon balloon;
    public EnemyData.CursedChalkStick cursedChalkStick;
    public EnemyData.SadSandCastle sadSandCastle;
    public EnemyData.SketchyWorm sketchyWorm;
    public EnemyData.AngryBench angryBench;

}

public class ExperienceData_Runtime {

    public AnimationCurve levelExperienceThresholdCurve;

    public float experienceCollectionRadius;
    public SmallInkBlob smallInkBlobSettings;
    public MediumInkBlob mediumInkBlobSettings;
    public LargeInkBlob largeInkBlobSettings;

    public AnimationCurve heightCurve;
    public float animationDuration;

    public AnimationCurve orbCollectionPathCurve;
    public float orbCollectionDuration;

    public ExperienceData.Balloon balloon;
    public ExperienceData.CursedChalkStick cursedChalkStick;
    public ExperienceData.SadSandCastle sadSandCastle;
    public ExperienceData.SketchyWorm sketchyWorm;
    public LargeAngryBench largeAngryBench;
    public MediumAngryBench mediumAngryBench;
    public SmallAngryBench smallAngryBench;

}

public class PlayerData_Runtime {
    
    public LayerMask playerLayerMask;
    public ScribbleKid scribbleKid;

}

public class UpgradeData_Runtime {

    public GearCog gearCog;
    public InkOverflow inkOverflow;

}

public class WeaponData_Runtime {

    public PrimaryWeaponType startingPrimaryWeaponType;
    public SecondaryWeaponType[] startingSecondaryWeaponTypes;
    public float aimWeaponRadius;
    public float weaponRotationSpeed;

    public WeaponData.Pencil pencil;
    public WeaponData.ChalkShot chalkShot;
    public WeaponData.MopSwipe mopSwipe;

    public WeaponData.InkSplash inkSplash;
    public WeaponData.CrayonMissile crayonMissile;
    public WeaponData.NotebookTear notebookTear;

}

public class RuntimeGameData : Singleton<RuntimeGameData> {

    private EnemyData_Runtime enemyData_Runtime;
    private ExperienceData_Runtime experienceData_Runtime;
    private PlayerData_Runtime playerData_Runtime;
    private UpgradeData_Runtime upgradeData_Runtime;
    private WeaponData_Runtime weaponData_Runtime;

    private Transform playerTargetTransform;

    protected override void Awake() {
        base.Awake();

        EnemyData enemyData = DataManager.Instance.GetEnemyData();
        enemyData_Runtime = new EnemyData_Runtime {
            minimumSpawnCircleRadius = enemyData.minimumSpawnCircleRadius,
            maximumSpawnCircleRadius = enemyData.maximumSpawnCircleRadius,
            firstWaveCost = enemyData.firstWaveCost,
            firstWaveTime = enemyData.firstWaveTime,
            waveCostIncreaseCurve = enemyData.waveCostIncreaseCurve,
            waveTimeIncreaseCurve = enemyData.waveTimeIncreaseCurve,
            enemyCostList = enemyData.enemyCostList,

            enemyLayerMask = enemyData.enemyLayerMask,
            balloon = enemyData.balloon,
            cursedChalkStick = enemyData.cursedChalkStick,
            sadSandCastle = enemyData.sadSandCastle,
            sketchyWorm = enemyData.sketchyWorm,
            angryBench = enemyData.angryBench
        };

        ExperienceData experienceData = DataManager.Instance.GetExperienceData();
        experienceData_Runtime = new ExperienceData_Runtime {
            levelExperienceThresholdCurve = experienceData.levelExperienceThresholdCurve,

            experienceCollectionRadius = experienceData.experienceCollectionRadius,
            smallInkBlobSettings = experienceData.smallInkBlobSettings,
            mediumInkBlobSettings = experienceData.mediumInkBlobSettings,
            largeInkBlobSettings = experienceData.largeInkBlobSettings,

            heightCurve = experienceData.heightCurve,
            animationDuration = experienceData.animationDuration,

            orbCollectionPathCurve = experienceData.orbCollectionPathCurve,
            orbCollectionDuration = experienceData.orbCollectionDuration,

            balloon = experienceData.balloon,
            cursedChalkStick = experienceData.cursedChalkStick,
            sadSandCastle = experienceData.sadSandCastle,
            sketchyWorm = experienceData.sketchyWorm,
            largeAngryBench = experienceData.largeAngryBench,
            mediumAngryBench = experienceData.mediumAngryBench,
            smallAngryBench = experienceData.smallAngryBench
        };

        PlayerData playerData = DataManager.Instance.GetPlayerData();
        playerData_Runtime = new PlayerData_Runtime {
            playerLayerMask = playerData.playerLayerMask,
            scribbleKid = playerData.scribbleKid
        };

        UpgradeData upgradeData = DataManager.Instance.GetUpgradeData();
        upgradeData_Runtime = new UpgradeData_Runtime {
            gearCog = upgradeData.gearCog,
            inkOverflow = upgradeData.inkOverflow
        };

        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        weaponData_Runtime = new WeaponData_Runtime {
            startingPrimaryWeaponType = weaponData.startingPrimaryWeaponType,
            startingSecondaryWeaponTypes = weaponData.startingSecondaryWeaponTypes,
            aimWeaponRadius = weaponData.aimWeaponRadius,
            weaponRotationSpeed = weaponData.weaponRotationSpeed,

            pencil = weaponData.pencil,
            chalkShot = weaponData.chalkShot,
            mopSwipe = weaponData.mopSwipe,

            inkSplash = weaponData.inkSplash,
            crayonMissile = weaponData.crayonMissile,
            notebookTear = weaponData.notebookTear
        };

        playerTargetTransform = DataManager.Instance.GetPlayerTargetTransform();
    }

    public EnemyData_Runtime GetEnemyData() {
        return enemyData_Runtime;
    }

    public ExperienceData_Runtime GetExperienceData() {
        return experienceData_Runtime;
    }

    public PlayerData_Runtime GetPlayerData() {
        return playerData_Runtime;
    }

    public UpgradeData_Runtime GetUpgradeData() {
        return upgradeData_Runtime;
    }

    public WeaponData_Runtime GetWeaponData() {
        return weaponData_Runtime;
    }

    public Transform GetPlayerTargetTransform() {
        return playerTargetTransform;
    }

}
