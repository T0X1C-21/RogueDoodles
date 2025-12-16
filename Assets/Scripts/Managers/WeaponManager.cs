using UnityEngine;

public class WeaponManager : MonoBehaviour {

    private void Awake() {
        WeaponData weaponData = DataManager.Instance.GetWeaponData();

        switch (weaponData.startingWeaponType) {
            case WeaponType.Pencil:
                GameObject instantiatedWeapon = Instantiate(weaponData.pencil.pencilPrefab, 
                    Vector3.zero, Quaternion.identity);
                instantiatedWeapon.transform.parent = this.transform;
                instantiatedWeapon.name = weaponData.startingWeaponType.ToString();
                break;
        }
    }

}
