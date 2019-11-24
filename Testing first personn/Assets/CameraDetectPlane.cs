using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetectPlane : MonoBehaviour
{
    private Camera target;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main;
        transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        var n = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(n) * Quaternion.Euler(90, 0, 0);
        transform.localScale = new Vector3(n.magnitude/30, n.magnitude/30, n.magnitude/30);

    }
}
