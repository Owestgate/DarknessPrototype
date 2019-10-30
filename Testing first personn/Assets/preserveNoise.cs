using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preserveNoise : MonoBehaviour
{
    private static preserveNoise instance = null;

    public static preserveNoise Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null & instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
