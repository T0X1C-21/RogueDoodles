using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager> {

    private HashSet<UpgradeType> userInterfaceUpgradePoolHashSet = new HashSet<UpgradeType>();

    protected override void Awake() {
        base.Awake();

        ResetUpgradePool();
    }

    public void ResetUpgradePool() {
        for (int i = 0; i < Enum.GetValues(typeof(UpgradeType)).Length; i++) {
            Array array = Enum.GetValues(typeof(UpgradeType));
            userInterfaceUpgradePoolHashSet.Add((UpgradeType) array.GetValue(i));
        }
    }

    public int NumberOfItemsInUpgradePool() {
        return userInterfaceUpgradePoolHashSet.Count;
    }

    public bool CheckItemInUpgradePool(UpgradeType poolItem) {
        return userInterfaceUpgradePoolHashSet.Contains(poolItem);
    }

    public bool AddToUpgradePool(UpgradeType poolItem) {
        return userInterfaceUpgradePoolHashSet.Add(poolItem);
    }

    public bool RemoveFromUpgradePool(UpgradeType poolItem) {
        return userInterfaceUpgradePoolHashSet.Remove(poolItem);
    }

}
