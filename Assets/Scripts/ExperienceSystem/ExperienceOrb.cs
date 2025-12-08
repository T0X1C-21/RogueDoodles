using System.Collections;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour {

    private int amountOfExperience;
    private AnimationCurve heightCurve;
    private float animationDuration;
    private AnimationCurve orbCollectionSpeedCurve;
    private Coroutine attractToPlayerCoroutine;

    private void Awake() {
        ExperienceData experienceData = DataManager.Instance.GetExperienceData();
        heightCurve = experienceData.heightCurve;
        animationDuration = experienceData.animationDuration;
        orbCollectionSpeedCurve = experienceData.orbCollectionSpeedCurve;
    }

    private void Start() {
        StartCoroutine(SpawnParabolicAnimation());
    }

    private IEnumerator SpawnParabolicAnimation() {
        float originalOrbPositionX = this.transform.position.x;
        float originalOrbPositionY = this.transform.position.y;
        float targetOrbPositionX = Random.Range(-1f, 1f) + originalOrbPositionX;
        float randomHeightMultiplier = Random.Range(0.25f, 1.25f);
        float randomAnimationDurationMultiplier = Random.Range(0.8f, 1.2f);
        float t = 0f;
        while(t < 1) {
            t += Time.deltaTime * animationDuration * randomAnimationDurationMultiplier;
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
            Vector3 endPosition = DataManager.Instance.GetPlayerTargetTransform().position;
            t += Time.deltaTime * 1f;
            this.transform.position = Vector3.Lerp(startPosition, endPosition, t * orbCollectionSpeedCurve.Evaluate(t));
            yield return null;
        }

        attractToPlayerCoroutine = null;

        float destroyDistance = 0.1f;
        if(Vector2.Distance(this.transform.position, 
            DataManager.Instance.GetPlayerTargetTransform().position) <= destroyDistance) {
            Destroy(this.gameObject);
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
    
}
