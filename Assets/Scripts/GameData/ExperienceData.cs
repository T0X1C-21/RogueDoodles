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

    [Header("----- INK BLOBS SETTINGS -----")]
    public float experienceCollectionRadius;
    public SmallInkBlob smallInkBlobSettings;
    public MediumInkBlob mediumInkBlobSettings;
    public LargeInkBlob largeInkBlobSettings;

    [Header("----- INK BLOB POP OUT ANIMATION -----")]
    public AnimationCurve heightCurve;
    public float animationDuration;

    [Header("----- INK BLOB COLLECTION ANIMATION -----")]
    public AnimationCurve orbCollectionPathCurve;
    public float orbCollectionDuration;

}