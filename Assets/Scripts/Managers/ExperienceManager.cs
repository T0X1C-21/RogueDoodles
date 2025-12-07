using UnityEngine;

public class ExperienceManager : Singleton<ExperienceManager> {

    private GameObject smallInkBlobObject;
    private GameObject mediumInkBlobObject;
    private GameObject largeInkBlobObject;

    protected override void Awake() {
        base.Awake();

        ExperienceData experienceData = DataManager.Instance.GetExperienceData();

        smallInkBlobObject = experienceData.smallInkBlobObject;
        mediumInkBlobObject = experienceData.mediumInkBlobObject;
        largeInkBlobObject = experienceData.largeInkBlobObject;
    }

    public void SpawnInkBlob(InkBlobSize inkBlobSize, Vector3 spawnPosition) {
        switch (inkBlobSize) {
            case InkBlobSize.Small:
                Instantiate(smallInkBlobObject, spawnPosition, Quaternion.identity);
                break;
            case InkBlobSize.Medium:
                Instantiate(mediumInkBlobObject, spawnPosition, Quaternion.identity);
                break;
            case InkBlobSize.Large:
                Instantiate(largeInkBlobObject, spawnPosition, Quaternion.identity);
                break;
        }
    }

}
