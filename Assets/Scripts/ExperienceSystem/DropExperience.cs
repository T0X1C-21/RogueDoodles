using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DropExperience : MonoBehaviour {

    private EnemyType enemyType;

    private int minimumAmountOfExperience;
    private int maximumAmountOfExperience;
    private int smallInkBlobExperienceAmount;
    private int mediumInkBlobExperienceAmount;
    private int largeInkBlobExperienceAmount;

    private void Awake() {
        enemyType = this.GetComponent<TypeOfEnemy>().GetEnemyType();
        ExperienceData experienceData = DataManager.Instance.GetExperienceData();

        switch (enemyType) {
            case EnemyType.Balloon:
                minimumAmountOfExperience = experienceData.balloon.minimumAmountOfExperienceDrop;
                maximumAmountOfExperience = experienceData.balloon.maximumAmountOfExperienceDrop;
                break;
            case EnemyType.CursedChalkStick:
                minimumAmountOfExperience = experienceData.cursedChalkStick.minimumAmountOfExperienceDrop;
                maximumAmountOfExperience = experienceData.cursedChalkStick.maximumAmountOfExperienceDrop;
                break;
        }

        smallInkBlobExperienceAmount = experienceData.smallInkBlobSettings.amountOfExperience;
        mediumInkBlobExperienceAmount = experienceData.mediumInkBlobSettings.amountOfExperience;
        largeInkBlobExperienceAmount = experienceData.largeInkBlobSettings.amountOfExperience;
    }

    public void SpawnExperienceOrbs() {
        int amountOfExperienceToDrop = Random.Range(minimumAmountOfExperience, maximumAmountOfExperience);
        int remainingAmountOfExperience = amountOfExperienceToDrop;
        int randomNumber = 0;

        while(remainingAmountOfExperience > 0) {
            if(remainingAmountOfExperience >= largeInkBlobExperienceAmount) {
                randomNumber = Random.Range(1, 3);
                if(randomNumber == 1) {
                    ExperienceManager.Instance.SpawnInkBlob(InkBlobSize.Large, this.transform.position);
                    remainingAmountOfExperience -= largeInkBlobExperienceAmount;
                } else {
                    ExperienceManager.Instance.SpawnInkBlob(InkBlobSize.Medium, this.transform.position);
                    remainingAmountOfExperience -= mediumInkBlobExperienceAmount;
                }
            } else if(remainingAmountOfExperience >= mediumInkBlobExperienceAmount) {
                randomNumber = Random.Range(1, 3);
                if(randomNumber == 1) {
                    ExperienceManager.Instance.SpawnInkBlob(InkBlobSize.Medium, this.transform.position);
                    remainingAmountOfExperience -= mediumInkBlobExperienceAmount;
                } else {
                    ExperienceManager.Instance.SpawnInkBlob(InkBlobSize.Small, this.transform.position);    
                    remainingAmountOfExperience -= smallInkBlobExperienceAmount;
                }
            } else if(remainingAmountOfExperience >= smallInkBlobExperienceAmount) {
                ExperienceManager.Instance.SpawnInkBlob(InkBlobSize.Small, this.transform.position);    
                remainingAmountOfExperience -= smallInkBlobExperienceAmount;
            }
        }
    }

}