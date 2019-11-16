using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
public class RotatePlayer : MonoBehaviour
{

    public float speed;
    public bool dis;
    public bool spinning;
    private Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Chapter2") {
            if (PlayerPrefs.GetInt("pcheckpoint") == 0) {
                dis = true;
                StartCoroutine(RotateAtStart());
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (PlayerPrefs.GetInt("pcheckpoint") == 1 || PlayerPrefs.GetInt("pcheckpoint") == 2) {
                gameObject.transform.localRotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }
    
    IEnumerator RotateAtStart(){
        yield return new WaitForSeconds(0.01f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 100000);
        yield return new WaitForSeconds(8.5f);
        spinning = true;
        yield return new WaitForSeconds(1);
        //gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
     
        dis = false;
        spinning = false;
        GetComponent<FirstPersonController>().enabled = true;
        gameObject.GetComponent<RotatePlayer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(dis == true){
            GetComponent<FirstPersonController>().enabled = false;
        }
       

        if(spinning == true){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * speed);
        }
    }
}
