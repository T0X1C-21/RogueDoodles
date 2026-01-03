using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager> {

    private HashSet<UpgradeType> upgradePoolHashSet = new HashSet<UpgradeType>();
    private HashSet<UpgradeType> removedUpgradePoolHashSet = new HashSet<UpgradeType>();

    protected override void Awake() {
        base.Awake();

        ResetUpgradePool();
    }

    public void ResetUpgradePool() {
        Array array = null;
        for (int i = 0; i < Enum.GetValues(typeof(UpgradeType)).Length; i++) {
            array = Enum.GetValues(typeof(UpgradeType));
        }

        foreach(UpgradeType upgradeType in array) {
            if (!removedUpgradePoolHashSet.Contains(upgradeType)) {
                upgradePoolHashSet.Add(upgradeType);
            } else {
                Debug.Log($"Skipped {upgradeType}!");
            }
        }
    }

    public int NumberOfItemsInUpgradePool() {
        return upgradePoolHashSet.Count;
    }

    public bool CheckItemInUpgradePool(UpgradeType poolItem) {
        return upgradePoolHashSet.Contains(poolItem);
    }

    public bool RemoveFromUpgradePool(UpgradeType poolItem) {
        return upgradePoolHashSet.Remove(poolItem);
    }

    public void DeleteFromUpgradePool(UpgradeType poolItem) {
        removedUpgradePoolHashSet.Add(poolItem);
    }

}
