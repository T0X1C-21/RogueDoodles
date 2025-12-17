using UnityEngine;

[CreateAssetMenu(fileName = "ExperienceData", menuName = "GameData/ExperienceData")]
public class ExperienceData : ScriptableObject {

    [System.Serializable]
    public struct SmallInkBlob {
        public GameObject prefabObject;
        public int amountOfExperience;
    }

    [System.Serializable]
    public struct MediumInkBlob {
        public GameObject prefabObject;
        public int amountOfExperience;
    }

    [System.Serializable]
    public struct LargeInkBlob {
        public GameObject prefabObject;
        public int amountOfExperience;
    }

    [System.Serializable]
    public struct Balloon {
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
    }

    [System.Serializable]
    public struct CursedChalkStick {
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
    }

    [System.Serializable]
    public struct SadSandCastle {
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
    }

    [System.Serializable]
    public struct SketchyWorm {
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
    }

    [System.Serializable]
    public struct LargeAngryBench {
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
    }

    [System.Serializable]
    public struct MediumAngryBench {
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
    }

    [System.Serializable]
    public struct SmallAngryBench {
        public int minimumAmountOfExperienceDrop;
        public int maximumAmountOfExperienceDrop;
    }

    [Space(10)]
    [Header("---- LEVEL SETTINGS -----")]
    public AnimationCurve levelExperienceThresholdCurve;

    [Space(10)]
    [Header("----- INK BLOBS SETTINGS -----")]
    public float experienceCollectionRadius;
    public SmallInkBlob smallInkBlobSettings;
    public MediumInkBlob mediumInkBlobSettings;
    public LargeInkBlob largeInkBlobSettings;

    [Space(10)]
    [Header("----- INK BLOB POP OUT ANIMATION -----")]
    public AnimationCurve heightCurve;
    public float animationDuration;

    [Space(10)]
    [Header("----- INK BLOB COLLECTION ANIMATION -----")]
    public AnimationCurve orbCollectionPathCurve;
    public float orbCollectionDuration;

    [Space(10)]
    [Header("----- ENEMY SETTINGS -----")]
    public Balloon balloon;
    public CursedChalkStick cursedChalkStick;
    public SadSandCastle sadSandCastle;
    public SketchyWorm sketchyWorm;
    public LargeAngryBench largeAngryBench;
    public MediumAngryBench mediumAngryBench;
    public SmallAngryBench smallAngryBench;

}