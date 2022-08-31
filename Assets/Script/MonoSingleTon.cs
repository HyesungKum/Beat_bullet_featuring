using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleTon<T> : MonoBehaviour where T: class, new()
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if(instance == null)
                {
                    GameObject inst = new GameObject(typeof(T).ToString(), typeof(T));
                    instance = inst.GetComponent<T>();
                }
            }
            return instance;
        }
    }
}
