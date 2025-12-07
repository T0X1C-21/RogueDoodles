using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DropExperience : MonoBehaviour {

    [SerializeField] private EnemyType enemyType;

    private int minimumAmountOfExperience;
    private int maximumAmountOfExperience;
    private int smallInkBlobExperienceAmount;
    private int mediumInkBlobExperienceAmount;
    private int largeInkBlobExperienceAmount;

    private void Awake() {
        EnemyData enemyData = DataManager.Instance.GetEnemyData();

        switch (enemyType) {
            case EnemyType.Balloon:
                minimumAmountOfExperience = enemyData.balloon.minimumAmountOfExperienceDrop;
                maximumAmountOfExperience = enemyData.balloon.maximumAmountOfExperienceDrop;
                break;
            case EnemyType.CursedChalkStick:
                minimumAmountOfExperience = enemyData.cursedChalkStick.minimumAmountOfExperienceDrop;
                maximumAmountOfExperience = enemyData.cursedChalkStick.maximumAmountOfExperienceDrop;
                break;
        }

        ExperienceData experienceData = DataManager.Instance.GetExperienceData();
        smallInkBlobExperienceAmount = experienceData.smallInkBlob.amountOfExperience;
        mediumInkBlobExperienceAmount = experienceData.mediumInkBlob.amountOfExperience;
        largeInkBlobExperienceAmount = experienceData.largeInkBlob.amountOfExperience;
    }

    public void SpawnExperienceOrbs() {
        int amountOfExperienceToDrop = Random.Range(minimumAmountOfExperience, maximumAmountOfExperience);
        int remainingAmountOfExperience = amountOfExperienceToDrop;

        while(remainingAmountOfExperience > 0) {
            if(remainingAmountOfExperience >= largeInkBlobExperienceAmount) {
                ExperienceManager.Instance.SpawnInkBlob(InkBlobSize.Large, this.transform.position);
                remainingAmountOfExperience -= largeInkBlobExperienceAmount;
            } else if(remainingAmountOfExperience >= mediumInkBlobExperienceAmount) {
                ExperienceManager.Instance.SpawnInkBlob(InkBlobSize.Medium, this.transform.position);
                remainingAmountOfExperience -= mediumInkBlobExperienceAmount;
            } else if(remainingAmountOfExperience >= smallInkBlobExperienceAmount) {
                ExperienceManager.Instance.SpawnInkBlob(InkBlobSize.Small, this.transform.position);    
                remainingAmountOfExperience -= smallInkBlobExperienceAmount;
            }
        }
    }

}
