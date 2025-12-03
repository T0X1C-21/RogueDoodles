using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] private CharacterType characterType;
    
    private float healthPoints;
    private int maxHealthPoints;

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
    }

    public void TakeDamage(int damageAmount) {

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
