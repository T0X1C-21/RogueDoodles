using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {
    
    private static Dictionary<PoolType, List<GameObject>> poolDictionary;
    private static Dictionary<PoolType, Transform> poolObjectTransformDictionary;

    private void Awake() {
        poolObjectTransformDictionary = new Dictionary<PoolType, Transform>();
        poolDictionary = new Dictionary<PoolType, List<GameObject>>();

        // Create gameobjects at runtime which will parent pooled object to themselves for a clean hierarchy
        foreach(PoolType poolType in System.Enum.GetValues(typeof(PoolType))) {
            GameObject poolObject = new GameObject(poolType.ToString());
            poolObject.transform.parent = this.transform;
            poolObjectTransformDictionary.Add(poolType, poolObject.transform);
        }
    }

    // Replacement for Instantiate()
    public static GameObject GetObjectFromPool(PoolType poolType, GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation) {
        if(poolDictionary == null) {
            poolDictionary = new Dictionary<PoolType, List<GameObject>>();
        }

        // Create a new key-value pair if the specified pooltype doesn't exist yet
        if(!poolDictionary.TryGetValue(poolType, out List<GameObject> poolList)) {
            poolList = new List<GameObject>();
            poolDictionary.Add(poolType, poolList);
        }

        // Gets a disabled poolobject if exists
        foreach(GameObject gameObject in poolList) {
            if (!gameObject.activeInHierarchy) {
                gameObject.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
                gameObject.SetActive(true);

                // Reset runtime data of the pooled object for re-use
                if(gameObject.TryGetComponent(out IPoolable poolable)) {
                    poolable.OnSpawnFromPool();
                }

                return gameObject;
            }
        }

        // Creates a new poolobject if one doesn't already exist
        GameObject newGameObject = Instantiate(prefab, spawnPosition, spawnRotation);
        newGameObject.SetActive(true);
        poolList.Add(newGameObject);
        newGameObject.transform.parent = poolObjectTransformDictionary[poolType];

        return newGameObject;
    }

    public static void SetObjectBackToPool(PoolType poolType, GameObject gameObject) {
        if(!poolDictionary.TryGetValue(poolType, out List<GameObject> poolList)) {
            Debug.LogWarning("Pool type not found!");
            return;
        }

        if (!poolList.Contains(gameObject)) {
            Debug.LogWarning("This object does not belond to the pool!");
            return;
        }

        // Reset runtime data of the pooled object for re-use
        if(gameObject.TryGetComponent(out IPoolable poolable)) {
            poolable.OnReturnToPool();
        }
        gameObject.SetActive(false);
    }

}