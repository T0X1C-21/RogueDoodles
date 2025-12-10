using UnityEngine;

public class CollectExperience : MonoBehaviour {

    [SerializeField] private bool drawGizmos;

    private float experienceCollectionRadius;

    private void Awake() {
        PlayerData playerData = DataManager.Instance.GetPlayerData();
        experienceCollectionRadius = playerData.experienceCollectionRadius;
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

    private void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }

        Gizmos.color = Color.limeGreen;
        Gizmos.DrawWireSphere(this.transform.position, experienceCollectionRadius);
    }

}