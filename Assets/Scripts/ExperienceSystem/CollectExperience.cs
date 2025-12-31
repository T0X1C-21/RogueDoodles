using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CollectExperience : Singleton<CollectExperience> {

    public static event EventHandler<OnExperienceCollectedEventArgs> OnExperienceCollected;
    public class OnExperienceCollectedEventArgs : EventArgs {
        public int levelNumber;
        public int amountOfExperience;
    }

    public static event EventHandler<OnLevelUpEventArgs> OnLevelUp;
    public class OnLevelUpEventArgs : EventArgs {
        public int levelNumber;
    }

    [SerializeField] private bool drawGizmos;

    private static int amountOfExperience;
    private static int levelNumber;
    private static int nextLevelThreshold;

    private float experienceCollectionRadius;
    private static AnimationCurve levelExperienceThresholdCurve;

    protected override void Awake() {
        base.Awake();

        ExperienceData_Runtime experienceData = RuntimeGameData.Instance.GetExperienceData();
        experienceCollectionRadius = experienceData.experienceCollectionRadius;
        levelExperienceThresholdCurve = experienceData.levelExperienceThresholdCurve;

        levelNumber = 0;
        amountOfExperience = 0;
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

        OnExperienceCollected?.Invoke(null, new OnExperienceCollectedEventArgs {
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

        OnLevelUp?.Invoke(null, new OnLevelUpEventArgs {
            levelNumber = levelNumber
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