using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    private WeaponData_Runtime weaponData;
    private Dictionary<PrimaryWeaponType, PlayerWeapon> equippedPrimaryWeaponDictionary =
        new Dictionary<PrimaryWeaponType, PlayerWeapon>();
    private Dictionary<SecondaryWeaponType, PlayerWeapon> equippedSecondaryWeaponDictionary = 
        new Dictionary<SecondaryWeaponType, PlayerWeapon>();

    private void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();
        SpawnWeapon(weaponData.startingPrimaryWeaponType);
        foreach(SecondaryWeaponType secondaryWeaponType in weaponData.startingSecondaryWeaponTypes) {
            SpawnWeapon(secondaryWeaponType);
        }
    }

    public void SpawnWeapon(SecondaryWeaponType secondaryWeaponType) {
        if(equippedSecondaryWeaponDictionary.Count == 3) {
            Debug.LogWarning("Cannot add more than 3 secondary weapons!");
            return;
        }

        GameObject instantiatedWeapon = null;
        switch (secondaryWeaponType) {
            case SecondaryWeaponType.InkSplash:
                instantiatedWeapon = Instantiate(weaponData.inkSplash.inkSplashPrefab,
                Vector3.zero, Quaternion.identity);
                break;
            case SecondaryWeaponType.CrayonMissile:
                instantiatedWeapon = Instantiate(weaponData.crayonMissile.crayonMissilePrefab,
                Vector3.zero, Quaternion.identity);
                break;
            case SecondaryWeaponType.NotebookTear:
                instantiatedWeapon = Instantiate(weaponData.notebookTear.notebookTearPrefab,
                Vector3.zero, Quaternion.identity);
                break;
        }

        if(instantiatedWeapon == null) {
            return;
        }

        instantiatedWeapon.transform.parent = this.transform;
        instantiatedWeapon.name = secondaryWeaponType.ToString();
        instantiatedWeapon.TryGetComponent(out PlayerWeapon playerWeapon);
        equippedSecondaryWeaponDictionary.Add(secondaryWeaponType, playerWeapon);
        Debug.Log($"Equipped Primary {secondaryWeaponType.ToString()}: " +
            $"{equippedSecondaryWeaponDictionary[secondaryWeaponType]}");
    }

    public void SpawnWeapon(PrimaryWeaponType primaryWeaponType) {
        if(equippedPrimaryWeaponDictionary.Count == 1) {
            Debug.LogWarning("Cannot add more than 1 primary weapon!");
            return;
        }

        GameObject instantiatedWeapon = null;
        switch (primaryWeaponType) {
            case PrimaryWeaponType.Pencil:
                instantiatedWeapon = Instantiate(weaponData.pencil.pencilPrefab,
                Vector3.zero, Quaternion.identity);
                break;
            case PrimaryWeaponType.ChalkShot:
                instantiatedWeapon = Instantiate(weaponData.chalkShot.chalkShotPrefab,
                Vector3.zero, Quaternion.identity);
                break;
            case PrimaryWeaponType.MopSwipe:
                instantiatedWeapon = Instantiate(weaponData.mopSwipe.mopSwipePrefab,
                Vector3.zero, Quaternion.identity);
                break;
        }

        if(instantiatedWeapon == null) {
            return;
        }

        instantiatedWeapon.transform.parent = this.transform;
        instantiatedWeapon.name = primaryWeaponType.ToString();
        instantiatedWeapon.TryGetComponent(out PlayerWeapon playerWeapon);
        equippedPrimaryWeaponDictionary.Add(primaryWeaponType, playerWeapon);
        Debug.Log($"Equipped Secondary {primaryWeaponType.ToString()}: " +
            $"{equippedPrimaryWeaponDictionary[primaryWeaponType]}");
    }

}
