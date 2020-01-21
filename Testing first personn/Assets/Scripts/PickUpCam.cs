using UnityEngine;
using UnityEngine.Events;

public class PickUpCam : MonoBehaviour
{
	public UnityEvent OnCamPickup;
	public UnityEvent fKey;

	public int photosLossAmount = 40;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (CameraObjectController.Instance)
			{
				CameraObjectController.Instance.gameObject.SetActive(!CameraObjectController.Instance.gameObject.activeSelf);
			}

			if (!CameraObjectController.Instance.gameObject.activeSelf)
			{
				CameraObjectController.Instance.photosRemaining -= photosLossAmount;
				CameraObjectController.Instance.CheckCameraPhotos();
			}

			fKey.Invoke();
		}
	}
}