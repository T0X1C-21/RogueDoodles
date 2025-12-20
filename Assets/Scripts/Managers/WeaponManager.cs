using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager> {

    private WeaponData weaponData;
    private Dictionary<WeaponType, PlayerWeapon> equippedWeaponDictionary = new Dictionary<WeaponType, PlayerWeapon>();

    protected override void Awake() {
        base.Awake();

        weaponData = DataManager.Instance.GetWeaponData();
        SpawnWeapon(weaponData.startingWeaponType);
        //SpawnWeapon(WeaponType.Pencil);
    }

    public void SpawnWeapon(WeaponType weaponType) {
        if(equippedWeaponDictionary.Count >= 3) {
            Debug.LogWarning("Cannot add more than 3 weapons!");
            return;
        }

        GameObject instantiatedWeapon = null;
        switch (weaponType) {
            case WeaponType.Pencil:
                instantiatedWeapon = Instantiate(weaponData.pencil.pencilPrefab,
                    Vector3.zero, Quaternion.identity);
                break;
            case WeaponType.ChalkShot:
                instantiatedWeapon = Instantiate(weaponData.chalkShot.chalkShotPrefab,
                    Vector3.zero, Quaternion.identity);
                break;
            case WeaponType.InkSplash:
                instantiatedWeapon = Instantiate(weaponData.inkSplash.inkSplashPrefab,
                    Vector3.zero, Quaternion.identity);
                break;
        }

        if(instantiatedWeapon == null) {
            return;
        }

        instantiatedWeapon.transform.parent = this.transform;
        instantiatedWeapon.name = weaponType.ToString();
        instantiatedWeapon.TryGetComponent(out PlayerWeapon playerWeapon);
        equippedWeaponDictionary.Add(weaponType, playerWeapon);
        Debug.Log($"Equipped {weaponType.ToString()}: {equippedWeaponDictionary[weaponType]}");
    }

}
