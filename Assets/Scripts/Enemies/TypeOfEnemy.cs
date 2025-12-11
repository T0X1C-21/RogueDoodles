using UnityEngine;

public class TypeOfEnemy : MonoBehaviour {

    [SerializeField] private EnemyType enemyType;

    public EnemyType GetEnemyType() {
        return enemyType;
    }

}
