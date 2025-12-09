using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour{
    
    public static T Instance { get; private set; }

    protected virtual void Awake() {
        if(Instance != null) {
            Destroy(this.gameObject);
        } else {
            Instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
    }

}