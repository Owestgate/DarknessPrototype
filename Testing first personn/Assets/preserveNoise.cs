using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preserveNoise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private static preserveNoise instance = null;
    public static preserveNoise Instance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}