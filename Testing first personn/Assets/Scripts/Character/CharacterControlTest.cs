using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlTest : MonoBehaviour
{
    //movement information
    public float speed = 10.0f; //forward speed
    public float speedHor = 8.0f; //horizontal speed
    public float sprintSpeed = 15.0f;
    public bool isSprinting;
    //Jumping information
    public float jumpForce;
    public Rigidbody characterRigidBody;
    public bool canJump;
    public bool cooldown;
    public float coolDownWait = 1.0f;
    //Sneak
    public float sneakSpeed = 5.0f;
    public bool isSneaking;

    public bool moving;

    public bool mouseLock;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterRigidBody = GetComponent<Rigidbody>();
        isSprinting = false;
        isSneaking = false;
        cooldown = false;
        mouseLock = true;
    }

    void FixedUpdate(){
        if (canJump == true && cooldown == false){
            
            canJump = false;
            characterRigidBody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            Invoke ("CoolDownTime", coolDownWait);
            cooldown = true;
        }

    }

    void CoolDownTime(){
        cooldown = false;

    }

    void Update() {

        //Movement controls
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speedHor;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        //Checking if player is moving, used for footsteps
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
            moving = true;
        }
         if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)){
            moving = false;
        }

        //Sprinting true or false
        if (Input.GetKeyDown(KeyCode.LeftShift)){

            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){

            isSprinting = false;
        }
        
        //Sprinting active or not - changes speed
        if(isSprinting == true){
            speed = sprintSpeed;
        }
        if(isSprinting == false && isSneaking == false){
            speed = 10.0f;
        }


        /* Keep for the time being
        //Unlock cursor when testing - remove later
        if (Input.GetKeyDown(KeyCode.P) && mouseLock == true){
        Cursor.lockState = CursorLockMode.None;
        mouseLock = false;
        }
        else if (Input.GetKeyDown(KeyCode.P) && mouseLock == false){
            Cursor.lockState = CursorLockMode.Locked;
            mouseLock = true;
        }
          */


        //Jumping Input
        if (Input.GetKeyDown(KeyCode.Space)){

            canJump = true;
        }

        //Sneaking
        if (Input.GetKeyDown(KeyCode.Q)){
            isSneaking = true;
        }
        if (Input.GetKeyUp(KeyCode.Q)){

            isSneaking = false;
        }
        //Sneaking true/false
        if (isSneaking == true){
            speed = sneakSpeed;
        }

        if (isSneaking == false && isSprinting == false){
            speed = 10.0f;
        }
    }
}
