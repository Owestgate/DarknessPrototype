using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpObjects : MonoBehaviour
{

	public Transform onHandPos;
	public GameObject playerMainController;
	public GameObject playerMainHand;
	public GameObject holdingItem;
	public GameObject digitalCamera;
	public AudioSource batSound;

	public UnityEvent crosshairOn;
	public UnityEvent crosshairOff;

	public UnityEvent crosshairOnTwo;
	public UnityEvent crosshairOffTwo;

	public bool togglePickup;

	public bool thingInHand;

	public LayerMask pickupLayerMask;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10.0f, Color.green);
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10.0f, Color.yellow);
		CheckForPickup();
		CheckForMouseHover();
		CheckForEvidenceMouseHover();


		if (thingInHand == false) {
			holdingItem = null;
		}
	}

	private void CheckForPickup()
	{
		RaycastHit hit;
		if (Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20))
			{
				Debug.Log(hit.transform.name);
				if (hit.transform.tag == "Battery")
				{
					digitalCamera.GetComponent<CameraObjectController>().photosRemaining += 40;
					if (digitalCamera.GetComponent<CameraObjectController>().photosRemaining > digitalCamera.GetComponent<CameraObjectController>().maxPhotos)
					{
						digitalCamera.GetComponent<CameraObjectController>().photosRemaining = digitalCamera.GetComponent<CameraObjectController>().maxPhotos;
					}
					batSound.Play();
					Destroy(hit.transform.gameObject);
				}
			}
		}
	}

	private void CheckForMouseHover()
	{
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 20))
		{
			//Debug.Log(hit.transform.name);
			if (hit.transform.tag == "Battery")
			{
				crosshairOn.Invoke();
			}
			else
			{
				crosshairOff.Invoke();
			}
		}
	}

	private void CheckForEvidenceMouseHover()
	{
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 20))
		{
			//Debug.Log(hit.transform.name);

			if (hit.transform.tag == "Evidence")
			{
				crosshairOnTwo.Invoke();
			}
			else
			{
				crosshairOffTwo.Invoke();
			}
		}

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
        {
            switch(togglePickup)
            {
                case true:
                    if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E)) && hit.transform.gameObject.GetComponent<PickUpableItem>())
                    {
                        holdingItem = hit.transform.gameObject;

                        holdingItem.transform.parent = playerMainController.transform;
                        holdingItem.transform.parent = playerMainHand.transform;
                        holdingItem.transform.position = playerMainHand.transform.position;
                        holdingItem.transform.rotation = playerMainHand.transform.rotation;
                        thingInHand = !thingInHand;
                    }
                    break;
                case false:
                    if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E)) )
                    {
                        if (hit.transform.gameObject.GetComponent<PickUpableItem>())
                        {
                            holdingItem = hit.transform.gameObject;

                            holdingItem.transform.parent = playerMainController.transform;
                            holdingItem.transform.parent = playerMainHand.transform;
                            holdingItem.transform.position = playerMainHand.transform.position;
                            holdingItem.transform.rotation = playerMainHand.transform.rotation;
                            thingInHand = true;

                        }
                        else if (hit.transform.gameObject.GetComponent<LightButton>())
                        {
                            holdingItem = hit.transform.gameObject;

                            holdingItem.GetComponent<LightButton>().Press();
                        }
                        else if (hit.transform.gameObject.GetComponent<DoorButton>())
                        {
                            holdingItem = hit.transform.gameObject;

                            holdingItem.GetComponent<DoorButton>().Press();
                        }
                    }
                    if ((Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.E)) /*&& hit.transform.gameObject.GetComponent<PickUpableItem>()*/)
                    {
                        if (holdingItem != null)
                        {
                            if (holdingItem.transform.parent != null)
                                holdingItem.transform.parent = null;
                        }
                        
                        hit.transform.gameObject.transform.position = hit.transform.gameObject.transform.position;
                        thingInHand = false;
                    }
                    break;
            }

            

            //Tells Spinning puzzle its being hit
            if (Input.GetKey(KeyCode.Mouse0) && hit.transform.gameObject.GetComponent<SpinPuzzle>()){
                hit.transform.SendMessage ("SpinningObject");
                
            }

            if( hit.transform.gameObject != holdingItem){
              //  holdingItem.transform.parent = null;
            }

        }
    }
}
