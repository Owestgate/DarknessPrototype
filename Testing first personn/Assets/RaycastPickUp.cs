using UnityEngine;

public class RaycastPickUp : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10f))
			{
				if (hit.collider.tag == "PickupCam")
				{
					Debug.Log("Found it");
					PickUpCam pickUpCamScript = hit.collider.GetComponent<PickUpCam>();
					pickUpCamScript.OnCamPickup.Invoke();
				}
			}
		}
	}
}
