using UnityEngine;

[CreateAssetMenu(fileName = "ExperienceData", menuName = "GameData/ExperienceData")]
public class ExperienceData : ScriptableObject {

    [System.Serializable]
    public struct SmallInkBlob {
        public Sprite inkBlobSprite;
        public int amountOfExperience;
    }

    [System.Serializable]
    public struct MediumInkBlob {
        public Sprite inkBlobSprite;
        public int amountOfExperience;
    }

    [System.Serializable]
    public struct LargeInkBlob {
        public Sprite inkBlobSprite;
        public int amountOfExperience;
    }

    [Header("-----Ink Blobs Objects-----")]
    public GameObject smallInkBlobObject;
    public GameObject mediumInkBlobObject;
    public GameObject largeInkBlobObject;

    [Header("-----Ink Blobs Settings-----")]
    public SmallInkBlob smallInkBlobSettings;
    public MediumInkBlob mediumInkBlobSettings;
    public LargeInkBlob largeInkBlobSettings;

    [Header("-----Ink Blob Animation-----")]
    public AnimationCurve yMinimumRandomization;
    public AnimationCurve yMaximumRandomization;
    public float animationDuration;

    [Header("-----Ink Blob Collection-----")]
    public AnimationCurve orbCollectionSpeedCurve;

}
