using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // if (instance == null)
        //     instance = gameObject;
        // else
        //     Destroy(gameObject);
    }
}
