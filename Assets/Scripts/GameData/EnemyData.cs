using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "GameData/EnemyData")]
public class EnemyData : ScriptableObject {

    [System.Serializable]
    public struct Balloon {
        public float moveSpeed;
        public int maxHealthPoints;
        [Tooltip("Health regeneration rate per second")]
        public float healthRegenRate;
        [Tooltip("Time until health regeneration")]
        public float timeToStartHealing;
    }

    [Header("----- ENEMY STATS -----")]
    public Balloon balloon;
    
}
