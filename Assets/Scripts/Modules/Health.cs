using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField] private CharacterType characterType;
    
    private float healthPoints;
    private int maxHealthPoints;
    private Slider healthSlider;

    private void Awake() {
        switch (characterType) {
            case CharacterType.Player:
                PlayerData playerData = DataManager.Instance.GetPlayerData();
                maxHealthPoints = playerData.maxHealthPoints;
                break;
            case CharacterType.Balloon:
                EnemyData enemyData = DataManager.Instance.GetEnemyData();
                maxHealthPoints = enemyData.balloon.maxHealthPoints;
                break;
        }
        healthPoints = maxHealthPoints;
        healthSlider = this.GetComponentInChildren<Slider>();
    }

    private void Update() {
        CalculateHealthSlider();
    }

    // temp health slider
    public void CalculateHealthSlider() {
        float healthSliderValue = Mathf.InverseLerp(0f, maxHealthPoints, healthPoints);
        healthSlider.value = healthSliderValue;
    }

    public void TakeDamage(float damageAmount) {

        if(healthPoints - damageAmount < 0) {
            // Death
            healthPoints = 0f;
            return;
        }

        healthPoints -= damageAmount;
        Debug.Log(characterType.ToString() + " Took Damage: " + healthPoints);
    }

    // Instant Health Regeneration For Player
    public void RegenerateHealthPointsInstant(int healAmount) {

    }

}
