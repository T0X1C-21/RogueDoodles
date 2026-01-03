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
        button.TryGetComponent(out ButtonUpgradeType buttonUpgradeType);

        switch (buttonUpgradeType.GetUpgradeType()) {
            case UpgradeType.MainWeapon:
                WeaponManager.Instance.TriggerPrimaryWeaponUpgrade();
                break;
            case UpgradeType.InkSplash:
                WeaponManager.Instance.TriggerSecondaryWeaponUpgrade(SecondaryWeaponType.InkSplash);
                break;
            case UpgradeType.CrayonMissile:
                WeaponManager.Instance.TriggerSecondaryWeaponUpgrade(SecondaryWeaponType.CrayonMissile);
                break;
            case UpgradeType.NotebookTear:
                WeaponManager.Instance.TriggerSecondaryWeaponUpgrade(SecondaryWeaponType.NotebookTear);
                break;
            case UpgradeType.AttackSpeedPlusPlus:
                PassiveManager.Instance.TriggerPassiveUpgrade(PassiveType.AttackSpeedPlusPlus);
                break;
            case UpgradeType.ProjectileCountPlusPlus:
                PassiveManager.Instance.TriggerPassiveUpgrade(PassiveType.ProjectileCountPlusPlus);
                break;
            case UpgradeType.PiercingPlusPlus:
                PassiveManager.Instance.TriggerPassiveUpgrade(PassiveType.PiercingPlusPlus);
                break;
            case UpgradeType.SizePlusPlus:
                PassiveManager.Instance.TriggerPassiveUpgrade(PassiveType.SizePlusPlus);
                break;
            case UpgradeType.AttackDamagePlusPlus:
                PassiveManager.Instance.TriggerPassiveUpgrade(PassiveType.AttackDamagePlusPlus);
                break;
            case UpgradeType.RerollPlusPlus:
                PassiveManager.Instance.TriggerPassiveUpgrade(PassiveType.RerollPlusPlus);
                break;
            case UpgradeType.LuckPlusPlus:
                PassiveManager.Instance.TriggerPassiveUpgrade(PassiveType.LuckPlusPlus);
                break;
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
        button.TryGetComponent(out ButtonUpgradeType buttonUpgradeType);

        bool gotUniqueUpgrade = false;
        while (!gotUniqueUpgrade) {
            int randomNumber = Random.Range(0, UpgradeManager.Instance.NumberOfItemsInUpgradePool());
            switch (randomNumber) {
                case 0:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.MainWeapon)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.MainWeapon);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        switch (WeaponManager.Instance.GetEquippedPrimaryWeaponType()) {
                            case PrimaryWeaponType.Pencil:
                                buttonText.text = userInterfaceData.upgradeMenuInfo.pencilText;
                                break;
                            case PrimaryWeaponType.ChalkShot:
                                buttonText.text = userInterfaceData.upgradeMenuInfo.chalkShotText;
                                break;
                            case PrimaryWeaponType.MopSwipe:
                                buttonText.text = userInterfaceData.upgradeMenuInfo.mopSwipeText;
                                break;
                        }
                        buttonText.text += " " + (WeaponManager.Instance.GetLevelOfEquippedPrimaryWeapon() + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.MainWeapon);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 1:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.InkSplash)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.InkSplash);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.inkSplashText;
                        buttonText.text += " " + (WeaponManager.Instance.
                            GetLevelOfEquippedSecondaryWeapon(SecondaryWeaponType.InkSplash) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.InkSplash);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 2:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.CrayonMissile)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.CrayonMissile);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.crayonMissileText;
                        buttonText.text += " " + (WeaponManager.Instance.
                            GetLevelOfEquippedSecondaryWeapon(SecondaryWeaponType.CrayonMissile) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.CrayonMissile);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 3:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.NotebookTear)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.NotebookTear);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.notebookTearText;
                        buttonText.text += " " + (WeaponManager.Instance.
                            GetLevelOfEquippedSecondaryWeapon(SecondaryWeaponType.NotebookTear) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.NotebookTear);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 4:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.AttackSpeedPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.AttackSpeedPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.attackSpeedPlusPlusText;
                        buttonText.text += " " + (PassiveManager.Instance.
                            GetLevelOfPassive(PassiveType.AttackSpeedPlusPlus) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.AttackSpeedPlusPlus);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 5:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.ProjectileCountPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.ProjectileCountPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.projectileCountPlusPlusText;
                        buttonText.text += " " + (PassiveManager.Instance.
                            GetLevelOfPassive(PassiveType.ProjectileCountPlusPlus) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.ProjectileCountPlusPlus);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 6:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.PiercingPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.PiercingPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.piercingPlusPlusText;
                        buttonText.text += " " + (PassiveManager.Instance.
                            GetLevelOfPassive(PassiveType.PiercingPlusPlus) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.PiercingPlusPlus);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 7:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.SizePlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.SizePlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.sizePlusPlusText;
                        buttonText.text += " " + (PassiveManager.Instance.
                            GetLevelOfPassive(PassiveType.SizePlusPlus) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.SizePlusPlus);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 8:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.AttackDamagePlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.AttackDamagePlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.attackDamagePlusPlusText;
                        buttonText.text += " " + (PassiveManager.Instance.
                            GetLevelOfPassive(PassiveType.AttackDamagePlusPlus) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.AttackDamagePlusPlus);
                        gotUniqueUpgrade = true;
                    }
                    break;
                case 9:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.RerollPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.RerollPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.rerollPlusPlusText;
                        buttonText.text += " " + (PassiveManager.Instance.
                            GetLevelOfPassive(PassiveType.RerollPlusPlus) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.RerollPlusPlus);
                        //gotUniqueUpgrade = true;
                    }
                    break;
                case 10:
                    if (UpgradeManager.Instance.CheckItemInUpgradePool(UpgradeType.LuckPlusPlus)) {
                        UpgradeManager.Instance.RemoveFromUpgradePool(UpgradeType.LuckPlusPlus);
                        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                        buttonText.text = userInterfaceData.upgradeMenuInfo.luckPlusPlusText;
                        buttonText.text += " " + (PassiveManager.Instance.
                            GetLevelOfPassive(PassiveType.LuckPlusPlus) + 1).ToString();
                        buttonUpgradeType.SetUpgradeType(UpgradeType.LuckPlusPlus);
                        //gotUniqueUpgrade = true;
                    }
                    break;
            }
        }
    }

}
