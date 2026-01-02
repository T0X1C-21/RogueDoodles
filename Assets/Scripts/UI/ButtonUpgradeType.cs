using UnityEngine;

public class ButtonUpgradeType : MonoBehaviour {

    private UpgradeType upgradeType;

    public void SetUpgradeType(UpgradeType upgradeType) {
        this.upgradeType = upgradeType;
    }

    public UpgradeType GetUpgradeType() {
        return upgradeType;
    }
    
}
