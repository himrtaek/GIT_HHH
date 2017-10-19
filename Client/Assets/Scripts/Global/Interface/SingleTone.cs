using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTone<T> : MonoBehaviour where T : SingleTone<T>{
    private static T _instance = null;

	public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = UnityEngine.GameObject.FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "SingleTone";
                    _instance = go.AddComponent(typeof(T)) as T;
                }
            }

            return _instance;
        }
    }
}
