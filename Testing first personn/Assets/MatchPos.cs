using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPos : MonoBehaviour
{
    public GameObject match;
    public int verticality;
    private Vector3 shift;

    // Start is called before the first frame update
    void Start()
    {
        shift.y = verticality;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = match.transform.position;
        transform.position += shift;
    }
}
