using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager> {

    public event EventHandler<OnPrimaryWeaponAddedEventArgs> OnPrimaryWeaponAdded;
    public class OnPrimaryWeaponAddedEventArgs : EventArgs {
        public PrimaryWeaponType addedWeapon;
    }

    private Dictionary<PrimaryWeaponType, PlayerWeapon> equippedPrimaryWeaponDictionary =
        new Dictionary<PrimaryWeaponType, PlayerWeapon>();
    private Dictionary<SecondaryWeaponType, PlayerWeapon> equippedSecondaryWeaponDictionary = 
        new Dictionary<SecondaryWeaponType, PlayerWeapon>();

    private WeaponData_Runtime weaponData;

    protected override void Awake() {
        base.Awake();

        weaponData = RuntimeGameData.Instance.GetWeaponData();
        SpawnWeapon(weaponData.startingPrimaryWeaponType);
        foreach(SecondaryWeaponType secondaryWeaponType in weaponData.startingSecondaryWeaponTypes) {
            SpawnWeapon(secondaryWeaponType);
        }
    }

    public void SpawnWeapon(SecondaryWeaponType secondaryWeaponType) {
        if (equippedSecondaryWeaponDictionary.ContainsKey(secondaryWeaponType)) {
            Debug.LogWarning("Cannot add the same weapon more than once!");
            return;
        }

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
        OnPrimaryWeaponAdded?.Invoke(this, new OnPrimaryWeaponAddedEventArgs {
            addedWeapon = primaryWeaponType
        });
        instantiatedWeapon.transform.parent = this.transform;
        instantiatedWeapon.name = primaryWeaponType.ToString();
        instantiatedWeapon.TryGetComponent(out PlayerWeapon playerWeapon);
        equippedPrimaryWeaponDictionary.Add(primaryWeaponType, playerWeapon);
        Debug.Log($"Equipped Secondary {primaryWeaponType.ToString()}: " +
            $"{equippedPrimaryWeaponDictionary[primaryWeaponType]}");
    }

}
