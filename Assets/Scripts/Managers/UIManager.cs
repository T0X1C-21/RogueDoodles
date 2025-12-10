using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI amountOfExperienceText;

    private void Start() {
        CollectExperience.onLevelUp += CollectExperience_OnLevelUp;
    }

    private void CollectExperience_OnLevelUp(object sender, 
        CollectExperience.OnLevelUpEventArgs onLevelUpEventArgs) {
        levelNumberText.text = onLevelUpEventArgs.levelNumber.ToString();
        amountOfExperienceText.text = onLevelUpEventArgs.amountOfExperience.ToString();
    }

}
