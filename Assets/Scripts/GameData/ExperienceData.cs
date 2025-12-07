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
    public SmallInkBlob smallInkBlob;
    public MediumInkBlob mediumInkBlob;
    public LargeInkBlob largeInkBlob;

}
