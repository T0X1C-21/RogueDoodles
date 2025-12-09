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

        smallInkBlobSettings = experienceData.smallInkBlobSettings;
        mediumInkBlobSettings = experienceData.mediumInkBlobSettings;
        largeInkBlobSettings = experienceData.largeInkBlobSettings;

        smallInkBlobObject = smallInkBlobSettings.prefabObject;
        mediumInkBlobObject = mediumInkBlobSettings.prefabObject;
        largeInkBlobObject = largeInkBlobSettings.prefabObject;
    }

    public void SpawnInkBlob(InkBlobSize inkBlobSize, Vector3 spawnPosition) {
        GameObject inkBlob = null;
        ExperienceOrb experienceOrb = null;

        switch (inkBlobSize) {
            case InkBlobSize.Small:
                inkBlob = ObjectPoolManager.GetObjectFromPool(PoolType.SmallInkBlob, smallInkBlobObject,
                    spawnPosition, Quaternion.identity);
                experienceOrb = inkBlob.GetComponent<ExperienceOrb>();
                experienceOrb.SetAmountOfExperience(smallInkBlobSettings.amountOfExperience);
                experienceOrb.SetOrbType(PoolType.SmallInkBlob);
                break;
            case InkBlobSize.Medium:
                inkBlob = ObjectPoolManager.GetObjectFromPool(PoolType.MediumInkBlob, mediumInkBlobObject,
                    spawnPosition, Quaternion.identity);
                experienceOrb = inkBlob.GetComponent<ExperienceOrb>();
                experienceOrb.SetAmountOfExperience(mediumInkBlobSettings.amountOfExperience);
                experienceOrb.SetOrbType(PoolType.MediumInkBlob);
                break;
            case InkBlobSize.Large:
                inkBlob = ObjectPoolManager.GetObjectFromPool(PoolType.LargeInkBlob, largeInkBlobObject,
                    spawnPosition, Quaternion.identity);
                experienceOrb = inkBlob.GetComponent<ExperienceOrb>();
                experienceOrb.SetAmountOfExperience(largeInkBlobSettings.amountOfExperience);
                experienceOrb.SetOrbType(PoolType.LargeInkBlob);
                break;
        }
    }

}