using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField] private CharacterType characterType;
    
    private float healthPoints;
    private int maxHealthPoints;
    private Slider healthSlider;

    private void Awake() {
        EnemyData enemyData;
        switch (characterType) {
            case CharacterType.Player:
                PlayerData playerData = DataManager.Instance.GetPlayerData();
                maxHealthPoints = playerData.maxHealthPoints;
                break;
            case CharacterType.Balloon:
                enemyData = DataManager.Instance.GetEnemyData();
                maxHealthPoints = enemyData.balloon.maxHealthPoints;
                break;
            case CharacterType.CursedChalkStick:
                enemyData = DataManager.Instance.GetEnemyData();
                maxHealthPoints = enemyData.cursedChalkStick.maxHealthPoints;
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
            Destroy(this.gameObject);
            healthPoints = 0f;
            return;
        }

        healthPoints -= damageAmount;
    }

    // Instant Health Regeneration For Player
    public void RegenerateHealthPointsInstant(int healAmount) {

    }

}
