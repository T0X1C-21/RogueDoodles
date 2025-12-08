using UnityEngine;

public class ExperienceManager : Singleton<ExperienceManager> {

    private GameObject smallInkBlobObject;
    private GameObject mediumInkBlobObject;
    private GameObject largeInkBlobObject;

    private ExperienceData.SmallInkBlob smallInkBlobSettings;
    private ExperienceData.MediumInkBlob mediumInkBlobSettings;
    private ExperienceData.LargeInkBlob largeInkBlobSettings;

    protected override void Awake() {
        base.Awake();

        ExperienceData experienceData = DataManager.Instance.GetExperienceData();

        smallInkBlobObject = experienceData.smallInkBlobObject;
        mediumInkBlobObject = experienceData.mediumInkBlobObject;
        largeInkBlobObject = experienceData.largeInkBlobObject;

        smallInkBlobSettings = experienceData.smallInkBlobSettings;
        mediumInkBlobSettings = experienceData.mediumInkBlobSettings;
        largeInkBlobSettings = experienceData.largeInkBlobSettings;
    }

    public void SpawnInkBlob(InkBlobSize inkBlobSize, Vector3 spawnPosition) {
        GameObject inkBlob = null;
        ExperienceOrb experienceOrb = null;

        switch (inkBlobSize) {
            case InkBlobSize.Small:
                inkBlob = Instantiate(smallInkBlobObject, spawnPosition, Quaternion.identity);
                experienceOrb = inkBlob.GetComponent<ExperienceOrb>();
                experienceOrb.SetAmountOfExperience(smallInkBlobSettings.amountOfExperience);
                break;
            case InkBlobSize.Medium:
                inkBlob = Instantiate(mediumInkBlobObject, spawnPosition, Quaternion.identity);
                experienceOrb = inkBlob.GetComponent<ExperienceOrb>();
                experienceOrb.SetAmountOfExperience(mediumInkBlobSettings.amountOfExperience);
                break;
            case InkBlobSize.Large:
                inkBlob = Instantiate(largeInkBlobObject, spawnPosition, Quaternion.identity);
                experienceOrb = inkBlob.GetComponent<ExperienceOrb>();
                experienceOrb.SetAmountOfExperience(largeInkBlobSettings.amountOfExperience);
                break;
        }

        inkBlob.transform.parent = this.transform;
    }

}
