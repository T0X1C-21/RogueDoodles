using UnityEngine;

public class PlayerHealth : Health {

    [SerializeField] private PlayerType playerType;

    protected override void Awake() {
        switch (playerType) {
            case PlayerType.ScribbleKid:
                PlayerData playerData = DataManager.Instance.GetPlayerData();
                maxHealthPoints = playerData.scribbleKid.maxHealthPoints;
                break;
        }

        base.Awake();
    }

    public override void TakeDamage(float damageAmount) {
        if(healthPoints - damageAmount <= 0f) {
            healthPoints = 0f;
            Destroy(this.gameObject);
            return;
        }

        base.TakeDamage(damageAmount);
    }

    public void RegenerateHealthPointsInstant(int healAmount) {

    }

}
