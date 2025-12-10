using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField] private CharacterType characterType;
    [SerializeField] private PlayerType playerType;
    
    private float healthPoints;
    private int maxHealthPoints;
    private Slider healthSlider;

    private void Awake() {
        EnemyData enemyData;
        switch (characterType) {
            case CharacterType.Player:
                switch (playerType) {
                    case PlayerType.ScribbleKid:
                        PlayerData playerData = DataManager.Instance.GetPlayerData();
                        maxHealthPoints = playerData.scribbleKid.maxHealthPoints;
                        break;
                }
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
            if(characterType != CharacterType.Player) {
                this.GetComponent<DropExperience>().SpawnExperienceOrbs();
            }

            healthPoints = 0f;

            switch (characterType) {
                case CharacterType.Player:
                    Destroy(this.gameObject);
                    break;
                case CharacterType.Balloon:
                    ObjectPoolManager.SetObjectBackToPool(PoolType.Balloon, this.gameObject);
                    break;
                case CharacterType.CursedChalkStick:
                    ObjectPoolManager.SetObjectBackToPool(PoolType.CursedChalkStick, this.gameObject);
                    break;
            }

            return;
        }

        healthPoints -= damageAmount;
    }

    // Instant Health Regeneration For Player
    public void RegenerateHealthPointsInstant(int healAmount) {

    }

    // For pooling enemies
    public void ResetHealth() {
        healthPoints = maxHealthPoints;
    }

}