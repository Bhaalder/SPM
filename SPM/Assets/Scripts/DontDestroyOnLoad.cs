using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    //Author: Patrik Ahlgren

    private static DontDestroyOnLoad instance;

    public static DontDestroyOnLoad Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<DontDestroyOnLoad>();
#if UNITY_EDITOR
                if (FindObjectsOfType<DontDestroyOnLoad>().Length > 1) {
                    Debug.LogError("Found more than one DDOL");
                }
#endif
            }
            return instance;
        }
    }

    void Start(){
        if (instance == null) {
            instance = this;
        }
        if (instance != null && instance != this) {
            Destroy(gameObject);
            Debug.LogWarning("Destroyed other Singleton with name: " + gameObject.name);
        }
        DontDestroyOnLoad(gameObject); 
    }
}
