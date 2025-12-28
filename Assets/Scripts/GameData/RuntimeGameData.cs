using UnityEngine;

public struct WeaponData_Runtime {
    public PrimaryWeaponType startingPrimaryWeaponType;
    public SecondaryWeaponType[] startingSecondaryWeaponTypes;
    public float aimWeaponRadius;
    public float weaponRotationSpeed;

    public WeaponData.Pencil pencil;
    public WeaponData.ChalkShot chalkShot;
    public WeaponData.MopSwipe mopSwipe;

    public WeaponData.InkSplash inkSplash;
    public WeaponData.CrayonMissile crayonMissile;
    public WeaponData.NotebookTear notebookTear;
}

public class EnemyData_Runtime {

}

public class RuntimeGameData : Singleton<RuntimeGameData> {

    private WeaponData_Runtime runtimeWeaponData;

    protected override void Awake() {
        base.Awake();

        WeaponData weaponData = DataManager.Instance.GetWeaponData();
        runtimeWeaponData = new WeaponData_Runtime {
            startingPrimaryWeaponType = weaponData.startingPrimaryWeaponType,
            startingSecondaryWeaponTypes = weaponData.startingSecondaryWeaponTypes,
            aimWeaponRadius = weaponData.aimWeaponRadius,
            weaponRotationSpeed = weaponData.aimWeaponRadius,

            pencil = weaponData.pencil,
            chalkShot = weaponData.chalkShot,
            mopSwipe = weaponData.mopSwipe,

            inkSplash = weaponData.inkSplash,
            crayonMissile = weaponData.crayonMissile,
            notebookTear = weaponData.notebookTear
        };
    }

    public WeaponData_Runtime GetWeaponData() {
        return runtimeWeaponData;
    }

}
