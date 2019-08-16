using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    private float timer = 0.0f;
    public float bobbingSpeed = 0.14f;
    public float bobbingAmount = 0.08f;
    float midpoint = 0.80f;

    public CharacterControlTest characterControl;
    //public bool headbobbing;
  
  void Start (){
      

  }

  void Update () {
     float waveslice = 0.0f;
     float horizontal = Input.GetAxis("Horizontal");
     float vertical = Input.GetAxis("Vertical");
  
  Vector3 cSharpConversion = transform.localPosition; 
  
  //If sprinting head bobbing increases
  if(characterControl.isSprinting == true && characterControl.isSneaking == false){
      bobbingSpeed = 0.20f;
  }
  //Resets head bobbing if neither sprint or sneak
  if(characterControl.isSprinting == false && characterControl.isSneaking == false){
      bobbingSpeed = 0.14f;
      bobbingAmount = 0.08f;
  }
  //If Sneaking Head bobbing less
  if(characterControl.isSneaking == true && characterControl.isSprinting == false){
      bobbingSpeed = 0.10f;
      bobbingAmount = 0.06f;
  }

  //if(characterControl.isSneaking == false)


   //Head Bobbing
  if(characterControl.cooldown == false){
     if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
        timer = 0.0f;
     }
     else {
        waveslice = Mathf.Sin(timer);
        timer = timer + bobbingSpeed;
        if (timer > Mathf.PI * 2) {
           timer = timer - (Mathf.PI * 2);
        }
     }
     if (waveslice != 0) {
        float translateChange = waveslice * bobbingAmount;
        float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
        translateChange = totalAxes * translateChange;
        cSharpConversion.y = midpoint + translateChange;
     }
     else {
        cSharpConversion.y = midpoint;
     }
  
  transform.localPosition = cSharpConversion;
  }
  }
}
