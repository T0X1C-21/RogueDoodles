using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] private CharacterType characterType;
    
    private float healthPoints;
    private int maxHealthPoints;
    private float healthRegenRate;
    private float timeToStartHealing;

    private Coroutine healthRegenerationCoroutine;

    private void Awake() {
        switch (characterType) {
            case CharacterType.Player:
                maxHealthPoints = DataManager.Instance.GetPlayerData().maxHealthPoints;
                healthRegenRate = DataManager.Instance.GetPlayerData().healthRegenRate;
                timeToStartHealing = DataManager.Instance.GetPlayerData().timeToStartHealing;
                break;
            case CharacterType.Balloon:
                maxHealthPoints = DataManager.Instance.GetEnemyData().balloon.maxHealthPoints;
                healthRegenRate = DataManager.Instance.GetEnemyData().balloon.healthRegenRate;
                timeToStartHealing = DataManager.Instance.GetEnemyData().balloon.timeToStartHealing;
                break;
        }
        healthPoints = maxHealthPoints;
    }

    public void TakeDamage(int damageAmount) {
        if(healthRegenerationCoroutine != null) {
            StopCoroutine(healthRegenerationCoroutine);
        }

        if(healthPoints - damageAmount < 0) {
            // Death
            healthPoints = 0f;
            return;
        }

        healthPoints -= damageAmount;
        Debug.Log(characterType.ToString() + " Took Damage: " + healthPoints);

        healthRegenerationCoroutine = StartCoroutine(RegenerateHealthPointsAutomatic());
    }

    // Instant Health Regeneration For Player
    public void RegenerateHealthPointsInstant() {

    }

    private IEnumerator RegenerateHealthPointsAutomatic() {
        yield return new WaitForSeconds(timeToStartHealing);
        while(healthPoints < maxHealthPoints) {
            healthPoints += healthRegenRate * Time.deltaTime;
            Debug.Log(characterType.ToString() + " Healing: " + healthPoints);
            yield return null;
        }
        healthPoints = maxHealthPoints;
        Debug.Log(characterType.ToString() + " Healing: " + healthPoints);
        yield return null;
    }

}
