using System.Collections;
using UnityEngine;

public class NotebookTear : PlayerWeapon {

    private WeaponData_Runtime weaponData;
    private GameObject notebookTearProjectilePrefab;
    private int numberOfProjectiles;
    private float cooldownTime;
    private float revolutionTime;
    private float animationTime;
    private float spawnAngle;
    private float currentSpawnAngle;

    protected override void Awake() {
        weaponData = RuntimeGameData.Instance.GetWeaponData();

        notebookTearProjectilePrefab = weaponData.notebookTear.notebookTearProjectilePrefab;
        numberOfProjectiles = weaponData.notebookTear.numberOfProjectiles;
        cooldownTime = weaponData.notebookTear.cooldownTime;
        revolutionTime = weaponData.notebookTear.revolutionTime;
        animationTime = weaponData.notebookTear.animationTime;
        
        spawnAngle = 360f / numberOfProjectiles;
        currentSpawnAngle = spawnAngle;
    }

    protected override void Update() {
        
    }

    private void Start() {
        StartCoroutine(ReleaseNotebookTearProjectiles());
    }

    private IEnumerator ReleaseNotebookTearProjectiles() {
        GameObject firstProjectileGameObject = null;    
        int currentNumberOfSpawnedProjectiles = 0;
        firstProjectileGameObject = ObjectPoolManager.GetObjectFromPool(PoolType.NotebookTearProjectile, 
            notebookTearProjectilePrefab, this.transform.position, Quaternion.identity);
        currentNumberOfSpawnedProjectiles++;
        firstProjectileGameObject.TryGetComponent(out NotebookTearProjectile firstNotebookTearProjectile);
        while (true) {
            if(firstNotebookTearProjectile.GetRevolutionAngle() >= currentSpawnAngle) {
                currentSpawnAngle += spawnAngle;
                ObjectPoolManager.GetObjectFromPool(PoolType.NotebookTearProjectile, notebookTearProjectilePrefab,
                    this.transform.position, Quaternion.identity);
                currentNumberOfSpawnedProjectiles++;
            }
            if(currentNumberOfSpawnedProjectiles == numberOfProjectiles) {
                currentSpawnAngle = spawnAngle;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(animationTime + revolutionTime + animationTime + cooldownTime);
        StartCoroutine(ReleaseNotebookTearProjectiles());
    }

    private void OnEnable() {
        UpgradeManager.OnAttackSpeedPlusPlusUpgrade += UpgradeManager_OnAttackSpeedPlusPlusUpgrade;
        UpgradeManager.OnProjectileCountPlusPlusUpgrade += UpgradeManager_OnProjectileCountPlusPlusUpgrade;
        UpgradeManager.OnPiercingPlusPlusUpgrade += UpgradeManager_OnOnPiercingPlusPlusUpgrade;
        UpgradeManager.OnSizePlusPlusUpgrade += UpgradeManager_OnSizePlusPlusUpgrade;
    }
    private void OnDisable() {
        UpgradeManager.OnAttackSpeedPlusPlusUpgrade -= UpgradeManager_OnAttackSpeedPlusPlusUpgrade;
        UpgradeManager.OnProjectileCountPlusPlusUpgrade -= UpgradeManager_OnProjectileCountPlusPlusUpgrade;
        UpgradeManager.OnPiercingPlusPlusUpgrade -= UpgradeManager_OnOnPiercingPlusPlusUpgrade;
        UpgradeManager.OnSizePlusPlusUpgrade += UpgradeManager_OnSizePlusPlusUpgrade;
    }

    private void UpgradeManager_OnAttackSpeedPlusPlusUpgrade(object sender, UpgradeManager.OnAttackSpeedPlusPlusUpgradeEventArgs e) {
        weaponData.notebookTear.attackHitCooldown /= e.attackSpeedToMultiply;
        weaponData.notebookTear.revolutionSpeed *= e.attackSpeedToMultiply;
        weaponData.notebookTear.rotationSpeed *= e.attackSpeedToMultiply;
    }

    private void UpgradeManager_OnProjectileCountPlusPlusUpgrade(object sender, UpgradeManager.OnProjectileCountPlusPlusUpgradeEventArgs e) {
        numberOfProjectiles += e.projectileCountToAdd;
        spawnAngle = 360f / numberOfProjectiles;
        currentSpawnAngle = spawnAngle;

        weaponData.notebookTear.numberOfProjectiles += e.projectileCountToAdd;
    }

    private void UpgradeManager_OnOnPiercingPlusPlusUpgrade(object sender, UpgradeManager.OnPiercingPlusPlusUpgradeEventArgs e) {
        weaponData.notebookTear.piercing += e.piercingToAdd;
    }

    private void UpgradeManager_OnSizePlusPlusUpgrade(object sender, UpgradeManager.OnSizePlusPlusUpgradeEventArgs e) {
        weaponData.notebookTear.size *= e.sizeToMultiply;
        weaponData.notebookTear.targetDetectionRadius *= e.sizeToMultiply;
    }

}