using System;
using UnityEngine;

public class CollectExperience : MonoBehaviour {

    public static EventHandler<OnLevelUpEventArgs> onLevelUp;
    public class OnLevelUpEventArgs : EventArgs {
        public int levelNumber;
        public int amountOfExperience;
    }

    [SerializeField] private bool drawGizmos;

    private static int amountOfExperience;
    private static int levelNumber = 0;
    private static int nextLevelThreshold;

    private float experienceCollectionRadius;
    private static AnimationCurve levelExperienceThresholdCurve;

    private void Awake() {
        ExperienceData experienceData = DataManager.Instance.GetExperienceData();
        experienceCollectionRadius = experienceData.experienceCollectionRadius;
        levelExperienceThresholdCurve = experienceData.levelExperienceThresholdCurve;
    }

    private void Start() {
        CalculateNextLevelParameters();
    }

    private void Update() {
        AttractExperienceOrbs();
    }

    private void AttractExperienceOrbs() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, experienceCollectionRadius);
        foreach(Collider2D hit in hits) {
            if(hit.TryGetComponent(out ExperienceOrb experienceOrb)) {
                if(experienceOrb.GetAttractToPlayerCoroutine() == null) {
                    experienceOrb.SetAttractToPlayerCoroutine(StartCoroutine(experienceOrb.AttractToPlayer()));
                }
            }
        }
    }

    public static void AddExperience(int amountOfExperience) {
        CollectExperience.amountOfExperience += amountOfExperience;

        onLevelUp?.Invoke(null, new OnLevelUpEventArgs {
            levelNumber = CollectExperience.levelNumber,
            amountOfExperience = CollectExperience.amountOfExperience
        });

        if(CollectExperience.amountOfExperience >= nextLevelThreshold) {
            CalculateNextLevelParameters();
        }
    }

    private static void CalculateNextLevelParameters() {
        CollectExperience.amountOfExperience = 0;
        levelNumber += 1;
        nextLevelThreshold = (int) levelExperienceThresholdCurve.Evaluate(levelNumber);

        onLevelUp?.Invoke(null, new OnLevelUpEventArgs {
            levelNumber = CollectExperience.levelNumber,
            amountOfExperience = CollectExperience.amountOfExperience
        });
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }

        Gizmos.color = Color.limeGreen;
        Gizmos.DrawWireSphere(this.transform.position, experienceCollectionRadius);
    }

}