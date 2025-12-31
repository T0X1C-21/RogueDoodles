using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CollectExperience;

public class UIManager : Singleton<UIManager> {

    [Header("Level & Experience UI")]
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI amountOfExperienceText;

    [Header("Upgrade Menu UI")]
    [SerializeField] private GameObject upgradeMenuPanelGameObject;
    [SerializeField] private GameObject leftUpgradeButtonGameObject; 
    [SerializeField] private GameObject middleUpgradeButtonGameObject; 
    [SerializeField] private GameObject rightUpgradeButtonGameObject;  

    private UserInterfaceData_Runtime userInterfaceData;

    protected override void Awake() {
        base.Awake();

        userInterfaceData = RuntimeGameData.Instance.GetUserInterfaceData();

        upgradeMenuPanelGameObject.SetActive(false);

        leftUpgradeButtonGameObject.GetComponent<Button>().onClick.
            AddListener(() => UpgradeButtonClick(leftUpgradeButtonGameObject.GetComponent<Button>()));
        middleUpgradeButtonGameObject.GetComponent<Button>().onClick.
            AddListener(() => UpgradeButtonClick(middleUpgradeButtonGameObject.GetComponent<Button>()));
        rightUpgradeButtonGameObject.GetComponent<Button>().onClick.
            AddListener(() => UpgradeButtonClick(rightUpgradeButtonGameObject.GetComponent<Button>()));
    }

    private void Start() {
        CollectExperience.OnExperienceCollected += CollectExperience_OnExperienceCollected;
        CollectExperience.OnLevelUp += CollectExperience_OnLevelUp;
    }

    private void CollectExperience_OnExperienceCollected(object sender, OnExperienceCollectedEventArgs e) {
        levelNumberText.text = e.levelNumber.ToString();
        amountOfExperienceText.text = e.amountOfExperience.ToString();
    }

    private void CollectExperience_OnLevelUp(object sender, OnLevelUpEventArgs e) {
        if(e.levelNumber == 1) {
            //return;
        }
        ShowUpgradeUserInterface();
    }


    private void UpgradeButtonClick(Button button) {
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        if(buttonText.text == userInterfaceData.upgradeMenuInfo.inkSplashText) {
            WeaponManager.Instance.SpawnWeapon(SecondaryWeaponType.InkSplash);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.crayonMissileText) {
            WeaponManager.Instance.SpawnWeapon(SecondaryWeaponType.CrayonMissile);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.notebookTearText) {
            WeaponManager.Instance.SpawnWeapon(SecondaryWeaponType.NotebookTear);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.attackSpeedPlusPlusText) {
            UpgradeManager.Instance.TriggerUpgrade(PassiveType.AttackSpeedPlusPlus);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.projectileCountPlusPlusText) {
            UpgradeManager.Instance.TriggerUpgrade(PassiveType.ProjectileCountPlusPlus);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.piercingPlusPlusText) {
            UpgradeManager.Instance.TriggerUpgrade(PassiveType.PiercingPlusPlus);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.sizePlusPlusText) {
            UpgradeManager.Instance.TriggerUpgrade(PassiveType.SizePlusPlus);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.attackDamagePlusPlusText) {
            UpgradeManager.Instance.TriggerUpgrade(PassiveType.AttackDamagePlusPlus);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.rerollPlusPlusText) {
            UpgradeManager.Instance.TriggerUpgrade(PassiveType.RerollPlusPlus);
        } else if(buttonText.text == userInterfaceData.upgradeMenuInfo.luckPlusPlusText) {
            UpgradeManager.Instance.TriggerUpgrade(PassiveType.LuckPlusPlus);
        } 

        upgradeMenuPanelGameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ShowUpgradeUserInterface() {
        Time.timeScale = 0f;
        upgradeMenuPanelGameObject.SetActive(true);
        GenerateRandomUpgradeForButton(leftUpgradeButtonGameObject.GetComponent<Button>());
        GenerateRandomUpgradeForButton(middleUpgradeButtonGameObject.GetComponent<Button>());
        GenerateRandomUpgradeForButton(rightUpgradeButtonGameObject.GetComponent<Button>());
        UpgradeManager.Instance.ResetUpgradePool();
    }

    private void GenerateRandomUpgradeForButton(Button button) {
        TextMeshProUGUI buttonText;

        bool gotUniqueUpgrade = false;
        while (!gotUniqueUpgrade) {
            int randomNumber = Random.Range(0, UpgradeManager.Instance.NumberOfItemsInUpgradePool());
        
            switch (randomNumber) {
                case 0:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.InkSplash)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.InkSplash);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.inkSplashText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 1:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.CrayonMissile)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.CrayonMissile);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.crayonMissileText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 2:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.NotebookTear)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.NotebookTear);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.notebookTearText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 3:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.AttackSpeedPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.AttackSpeedPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.attackSpeedPlusPlusText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 4:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.ProjectileCountPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.ProjectileCountPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.projectileCountPlusPlusText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 5:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.PiercingPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.PiercingPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.piercingPlusPlusText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 6:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.SizePlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.SizePlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.sizePlusPlusText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 7:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.AttackDamagePlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.AttackDamagePlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.attackDamagePlusPlusText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 8:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.RerollPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.RerollPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.rerollPlusPlusText;
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 9:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UserInterfaceUpgradePool.LuckPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UserInterfaceUpgradePool.LuckPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.luckPlusPlusText;
                        gotUniqueUpgrade = true;
                    }
                    break;
            }
        }
    }

}
