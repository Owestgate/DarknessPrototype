using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : MonoBehaviour
{

    public GameObject ButtonController;
    public string color;

    public GameObject light;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        ButtonController.GetComponent<ButtonController>().ButtonPressed(color);
        StartCoroutine(FlashLight());
    }

    IEnumerator FlashLight(){
        light.SetActive (true);
        yield return new WaitForSeconds(0.3f);
        light.SetActive (false);
    }
}
