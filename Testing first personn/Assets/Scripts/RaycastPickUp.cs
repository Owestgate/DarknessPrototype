using UnityEngine;

public class RaycastPickUp : MonoBehaviour
{
	public LayerMask mask;

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.E) && !Input.GetMouseButtonDown(0)) return;
		if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10f, mask)) return;
		if (hit.collider.tag != "PickupCam") return;
		PickUpCam pickUpCamScript = hit.collider.GetComponent<PickUpCam>();
		pickUpCamScript.OnCamPickup.Invoke();
	}
}
