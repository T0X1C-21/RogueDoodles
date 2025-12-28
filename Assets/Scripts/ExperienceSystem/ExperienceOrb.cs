using System.Collections;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour, IPoolable {

    private static int orbAnimationDirection = 1;

    private int amountOfExperience;
    private AnimationCurve heightCurve;
    private float animationDuration;
    private AnimationCurve orbCollectionSpeedCurve;
    private Coroutine attractToPlayerCoroutine;
    private float orbCollectionDuration;
    private PoolType orbType;
    private Transform playerTransform;

    private void Awake() {
        ExperienceData_Runtime experienceData = RuntimeGameData.Instance.GetExperienceData();
        heightCurve = experienceData.heightCurve;
        animationDuration = experienceData.animationDuration;
        orbCollectionSpeedCurve = experienceData.orbCollectionPathCurve;
        orbCollectionDuration = experienceData.orbCollectionDuration;

        playerTransform = RuntimeGameData.Instance.GetPlayerTargetTransform();
    }

    private void Start() {
        StartCoroutine(SpawnParabolicAnimation());
    }

    public void OnSpawnFromPool() {
        StartCoroutine(SpawnParabolicAnimation());
    }

    public void OnReturnToPool() {
        
    }

    private IEnumerator SpawnParabolicAnimation() {
        float originalOrbPositionX = this.transform.position.x;
        float originalOrbPositionY = this.transform.position.y;
        float targetOrbPositionX = (Random.Range(0.1f, 1f) * orbAnimationDirection) + originalOrbPositionX;
        orbAnimationDirection = (orbAnimationDirection == 1) ? -1 : 1;
        float randomHeightMultiplier = Random.Range(0.25f, 1.25f);
        float randomAnimationDurationMultiplier = Random.Range(0.8f, 1.2f);
        float t = 0f;
        while(t < 1) {
            t += Time.deltaTime / animationDuration * randomAnimationDurationMultiplier;
            float animationX = Mathf.Lerp(originalOrbPositionX, targetOrbPositionX, t);
            float animationY = (heightCurve.Evaluate(t) * randomHeightMultiplier) + originalOrbPositionY;
            this.transform.position = new Vector3(animationX, animationY);
            yield return null;
        }
    }

    public IEnumerator AttractToPlayer() {
        Vector3 startPosition = this.transform.position;
        float t = Random.Range(-0.3f, 0f);
        while(t < 1f) {
            Vector3 endPosition = RuntimeGameData.Instance.GetPlayerTargetTransform().position;
            t += Time.deltaTime / orbCollectionDuration;
            this.transform.position = Vector3.LerpUnclamped(startPosition, endPosition, orbCollectionSpeedCurve.Evaluate(t));
            yield return null;
        }

        attractToPlayerCoroutine = null;

        float destroyDistance = 0.1f;
        if(Vector2.Distance(this.transform.position, playerTransform.position) <= destroyDistance) {
            CollectExperience.AddExperience(amountOfExperience);
            ObjectPoolManager.SetObjectBackToPool(orbType, this.gameObject);
        }
    }

    public void SetAttractToPlayerCoroutine(Coroutine attractToPlayerCoroutine) {
        this.attractToPlayerCoroutine = attractToPlayerCoroutine;
    }

    public Coroutine GetAttractToPlayerCoroutine() {
        return attractToPlayerCoroutine;
    }

    public int GetAmountOfExperience() {
        return amountOfExperience;
    }

    public void SetAmountOfExperience(int amountOfExperience) {
        this.amountOfExperience = amountOfExperience;
    }

    public void SetOrbType(PoolType poolType) {
        orbType = poolType;
    }
    
}