using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on Unity Singleton example https://wiki.unity3d.com/index.php/Singleton
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    private void Awake()
    {

    }
}
