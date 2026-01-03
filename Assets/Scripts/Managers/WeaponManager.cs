using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager> {

    //public event EventHandler<OnPrimaryWeaponAddedEventArgs> OnPrimaryWeaponAdded;
    //public class OnPrimaryWeaponAddedEventArgs : EventArgs {
    //    public PrimaryWeaponType addedWeapon;
    //}

    private Dictionary<PrimaryWeaponType, int> equippedPrimaryWeaponDictionary = new Dictionary<PrimaryWeaponType, int>();
    private Dictionary<SecondaryWeaponType, int> equippedSecondaryWeaponDictionary = new Dictionary<SecondaryWeaponType, int>();

    private WeaponData_Runtime weaponData;

    protected override void Awake() {
        base.Awake();

        weaponData = RuntimeGameData.Instance.GetWeaponData();
        SpawnPrimaryWeapon(weaponData.startingPrimaryWeaponType);
    }

    public void SpawnPrimaryWeapon(PrimaryWeaponType primaryWeaponType) {
        if (equippedPrimaryWeaponDictionary.Count == 1 && !equippedPrimaryWeaponDictionary.ContainsKey(primaryWeaponType)) {
            Debug.LogWarning("Cannot add more than one Primary Weapon!");
            return;
        }

        equippedPrimaryWeaponDictionary.Add(primaryWeaponType, 1);
        GameObject instantiatedWeapon = null;

        switch (primaryWeaponType) {
            case PrimaryWeaponType.Pencil:
                instantiatedWeapon = Instantiate(weaponData.pencil.pencilPrefab,
                Vector3.zero, Quaternion.identity);
                instantiatedWeapon.transform.parent = this.transform;
                instantiatedWeapon.name = primaryWeaponType.ToString();
                break;
            case PrimaryWeaponType.ChalkShot:
                instantiatedWeapon = Instantiate(weaponData.chalkShot.chalkShotPrefab,
                Vector3.zero, Quaternion.identity);
                instantiatedWeapon.transform.parent = this.transform;
                instantiatedWeapon.name = primaryWeaponType.ToString();
                break;
            case PrimaryWeaponType.MopSwipe:
                instantiatedWeapon = Instantiate(weaponData.mopSwipe.mopSwipePrefab,
                Vector3.zero, Quaternion.identity);
                instantiatedWeapon.transform.parent = this.transform;
                instantiatedWeapon.name = primaryWeaponType.ToString();
                break;
        }
        Debug.Log($"Added to dictionary {primaryWeaponType.ToString()} with level 1");
    }

    public void TriggerPrimaryWeaponUpgrade() {
        PrimaryWeaponType primaryWeaponType = default;
        foreach(PrimaryWeaponType primaryWeaponTypeInDictionary in equippedPrimaryWeaponDictionary.Keys) {
            primaryWeaponType = primaryWeaponTypeInDictionary;
        }

        if (equippedPrimaryWeaponDictionary[primaryWeaponType] == 3) {
            Debug.LogWarning($"Cannot upgrade {primaryWeaponType.ToString()} past 3!");
            return;
        }
        equippedPrimaryWeaponDictionary[primaryWeaponType] += 1;
        int levelNumber = equippedPrimaryWeaponDictionary[primaryWeaponType];

        //GameObject instantiatedWeapon = null;
        switch (primaryWeaponType) {
            case PrimaryWeaponType.Pencil:
                switch (levelNumber) {
                    case 2:
                        Debug.Log("Not Implemented Yet!");
                        break;
                    case 3:
                        Debug.Log("Not Implemented Yet!");
                        UpgradeManager.Instance.DeleteFromUpgradePool(UpgradeType.MainWeapon);
                        break;
                }
                break;
            case PrimaryWeaponType.ChalkShot:
                switch (levelNumber) {
                    case 2:
                        Debug.Log("Not Implemented Yet!");
                        break;
                    case 3:
                        Debug.Log("Not Implemented Yet!");
                        UpgradeManager.Instance.DeleteFromUpgradePool(UpgradeType.MainWeapon);
                        break;
                }
                break;
            case PrimaryWeaponType.MopSwipe:
                switch (levelNumber) {
                    case 2:
                        Debug.Log("Not Implemented Yet!");
                        break;
                    case 3:
                        Debug.Log("Not Implemented Yet!");
                        UpgradeManager.Instance.DeleteFromUpgradePool(UpgradeType.MainWeapon);
                        break;
                }
                break;
        }
        Debug.Log($"Added to dictionary {primaryWeaponType.ToString()} with level {levelNumber}");
    }

    public void TriggerSecondaryWeaponUpgrade(SecondaryWeaponType secondaryWeaponType) {
        if (equippedSecondaryWeaponDictionary.Count == 3 && !equippedSecondaryWeaponDictionary.ContainsKey(secondaryWeaponType)) {
            Debug.LogWarning("Cannot add more than 3 Secondary Weapons!");
            return;
        }

        int levelNumber;
        if (!equippedSecondaryWeaponDictionary.ContainsKey(secondaryWeaponType)) {
            equippedSecondaryWeaponDictionary.Add(secondaryWeaponType, 1);
            levelNumber = equippedSecondaryWeaponDictionary[secondaryWeaponType];
        } else {
            if (equippedSecondaryWeaponDictionary[secondaryWeaponType] == 3) {
                Debug.LogWarning($"Cannot upgrade {secondaryWeaponType.ToString()} past 3!");
                return;
            }
            equippedSecondaryWeaponDictionary[secondaryWeaponType] += 1;
            levelNumber = equippedSecondaryWeaponDictionary[secondaryWeaponType];
        }

        GameObject instantiatedWeapon = null;
        switch (secondaryWeaponType) {
            case SecondaryWeaponType.InkSplash:
                switch (levelNumber) {
                    case 1:
                        instantiatedWeapon = Instantiate(weaponData.inkSplash.inkSplashPrefab,
                        Vector3.zero, Quaternion.identity);
                        instantiatedWeapon.transform.parent = this.transform;
                        instantiatedWeapon.name = secondaryWeaponType.ToString();
                        Debug.Log($"Equipped Secondary {secondaryWeaponType.ToString()}");
                        break;
                    case 2:
                        Debug.Log("Not Implemented Yet!");
                        break;
                    case 3:
                        Debug.Log("Not Implemented Yet!");
                        UpgradeManager.Instance.DeleteFromUpgradePool(UpgradeType.InkSplash);
                        break;
                }
                break;
            case SecondaryWeaponType.CrayonMissile:
                switch (levelNumber) {
                    case 1:
                        instantiatedWeapon = Instantiate(weaponData.crayonMissile.crayonMissilePrefab,
                        Vector3.zero, Quaternion.identity);
                        instantiatedWeapon.transform.parent = this.transform;
                        instantiatedWeapon.name = secondaryWeaponType.ToString();
                        Debug.Log($"Equipped Secondary {secondaryWeaponType.ToString()}");
                        break;
                    case 2:
                        Debug.Log("Not Implemented Yet!");
                        break;
                    case 3:
                        Debug.Log("Not Implemented Yet!");
                        UpgradeManager.Instance.DeleteFromUpgradePool(UpgradeType.CrayonMissile);
                        break;
                }
                break;
            case SecondaryWeaponType.NotebookTear:
                switch (levelNumber) {
                    case 1:
                        instantiatedWeapon = Instantiate(weaponData.notebookTear.notebookTearPrefab,
                        Vector3.zero, Quaternion.identity);
                        instantiatedWeapon.transform.parent = this.transform;
                        instantiatedWeapon.name = secondaryWeaponType.ToString();
                        Debug.Log($"Equipped Secondary {secondaryWeaponType.ToString()}");
                        break;
                    case 2:
                        Debug.Log("Not Implemented Yet!");
                        break;
                    case 3:
                        Debug.Log("Not Implemented Yet!");
                        UpgradeManager.Instance.DeleteFromUpgradePool(UpgradeType.NotebookTear);
                        break;
                }
                break;
        }
        Debug.Log($"Added to dictionary {secondaryWeaponType.ToString()} with level {levelNumber}");
    }

    public PrimaryWeaponType GetEquippedPrimaryWeaponType() {
        foreach(PrimaryWeaponType primaryWeaponType in equippedPrimaryWeaponDictionary.Keys) {
            return primaryWeaponType;
        } 
        return default;
    }

    public int GetLevelOfEquippedPrimaryWeapon() {
        foreach(PrimaryWeaponType primaryWeaponType in equippedPrimaryWeaponDictionary.Keys) {
            return equippedPrimaryWeaponDictionary[primaryWeaponType];
        } 
        return -1;
    }

    public List<SecondaryWeaponType> GetEquippedSecondaryWeaponTypes() {
        List<SecondaryWeaponType> secondaryWeaponTypesList = new List<SecondaryWeaponType>();

        foreach(SecondaryWeaponType secondaryWeaponType in equippedSecondaryWeaponDictionary.Keys) {
            secondaryWeaponTypesList.Add(secondaryWeaponType);
        }

        return secondaryWeaponTypesList;
    }

    public int GetLevelOfEquippedSecondaryWeapon(SecondaryWeaponType secondaryWeaponType) {
        if (equippedSecondaryWeaponDictionary.TryGetValue(secondaryWeaponType, out int level)) {
            return level;
        } else {
            return 0;
        }
    }

}
