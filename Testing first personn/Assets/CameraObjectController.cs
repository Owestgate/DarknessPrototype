using UnityEngine;
using UnityEngine.UI;

public class CameraObjectController : MonoBehaviour
{
    public int photosRemaining = 60;
    public int maxPhotos = 60;

    public RawImage batteryLow;
    public RawImage batteryMedium;
    public RawImage batteryHigh;
    public RawImage batteryFrame;

    [ColorUsage(false, true)] public Color normalColHdr;
    [ColorUsage(false, true)] public Color redColHdr;

    public AudioSource ReadySound;
    public AudioSource FlashSound;
    public AudioSource BeepSound;
    public Animator Anim;
    public Animator Anim2;
    public float CameraFlashRate = 0.1f;
    private float nextFlashTime;
    public Renderer orangeLight;

    public float holdDownTime = 1;
    private float currentHoldDownTime;
    public Transform PlayerCamera;
    public float Smoothing = 10;
    public float verticalOffset = 3;
    public RenderTexture rendTex;
    public Texture2D photo;
    public RawImage TakenPhoto;
    public float delay = 1;

    private void Start()
    {
        orangeLight.enabled = false;
        photosRemaining = maxPhotos;

        batteryLow.material.EnableKeyword("_EMISSION");
        batteryMedium.material.EnableKeyword("_EMISSION");
        batteryHigh.material.EnableKeyword("_EMISSION");
        batteryFrame.material.EnableKeyword("_EMISSION");

        batteryHigh.enabled = true;
        batteryMedium.enabled = true;
        batteryLow.enabled = true;

        batteryHigh.material.SetColor("_EmissionColor", normalColHdr);
        batteryMedium.material.SetColor("_EmissionColor", normalColHdr);
        batteryLow.material.SetColor("_EmissionColor", normalColHdr);
        batteryFrame.material.SetColor("_EmissionColor", normalColHdr);
    }

    void Update()
    {
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, PlayerCamera.rotation, Time.deltaTime * Smoothing);
        transform.parent.position = new Vector3 (PlayerCamera.position.x, PlayerCamera.position.y + verticalOffset, PlayerCamera.position.z);

        if (photosRemaining > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Time.time > nextFlashTime)
                {
                    orangeLight.enabled = true;
                    ReadySound.Play();
                }
            }

            if (Input.GetMouseButton(1))
            {
                if (orangeLight.enabled)
                {
                    currentHoldDownTime += Time.deltaTime;

                    if (currentHoldDownTime >= holdDownTime)
                    {
                        nextFlashTime = Time.time + CameraFlashRate;
                        Anim.Play("CameraFlash", 0, 0);
                        orangeLight.enabled = false;
                        photosRemaining--;
                        float ratio = photosRemaining / (float)maxPhotos;

                        // Battery is full / can take many photos.
                        if (ratio > 0.66f)
                        {
                            batteryHigh.enabled = true;
                            batteryMedium.enabled = true;
                            batteryLow.enabled = true;

                            batteryHigh.material.SetColor("_EmissionColor", normalColHdr);
                            batteryMedium.material.SetColor("_EmissionColor", normalColHdr);
                            batteryLow.material.SetColor("_EmissionColor", normalColHdr);
                            batteryFrame.material.SetColor("_EmissionColor", normalColHdr);
                        }
                        else if (ratio > 0.33f)
                        {
                            batteryHigh.enabled = false;
                            batteryMedium.enabled = true;
                            batteryLow.enabled = true;

                            batteryHigh.material.SetColor("_EmissionColor", normalColHdr);
                            batteryMedium.material.SetColor("_EmissionColor", normalColHdr);
                            batteryLow.material.SetColor("_EmissionColor", normalColHdr);
                            batteryFrame.material.SetColor("_EmissionColor", normalColHdr);
                        }
                        else if (ratio > 0)
                        {
                            batteryHigh.enabled = false;
                            batteryMedium.enabled = false;
                            batteryLow.enabled = true;

                            batteryHigh.material.SetColor("_EmissionColor", normalColHdr);
                            batteryMedium.material.SetColor("_EmissionColor", normalColHdr);
                            batteryLow.material.SetColor("_EmissionColor", redColHdr);
                            batteryFrame.material.SetColor("_EmissionColor", redColHdr);
                            Anim2.Play("BatteryFlash", 0, 0);
                        }
                        else
                        {
                            batteryHigh.enabled = false;
                            batteryMedium.enabled = false;
                            batteryLow.enabled = false;

                            batteryHigh.material.SetColor("_EmissionColor", normalColHdr);
                            batteryMedium.material.SetColor("_EmissionColor", normalColHdr);
                            batteryLow.material.SetColor("_EmissionColor", normalColHdr);
                            batteryFrame.material.SetColor("_EmissionColor", redColHdr);
                            Anim2.Play("BatteryFlash 1", 0, 0);
                        }

                        //is object in frame code
                        double walldistance = 100;
                        RaycastHit hit;
                        LayerMask mask = LayerMask.GetMask("FlashDetect");
                        RaycastHit wallhit;
                        LayerMask wallmask = LayerMask.GetMask("CameraDisplay");
                        Vector3 cameraShift = transform.position + (transform.TransformDirection(Vector3.forward) * -2);
                        if (Physics.Raycast(cameraShift, transform.TransformDirection(Vector3.forward), out wallhit, 50.0f, wallmask))
                        {
                            walldistance = wallhit.distance;
                        }
                        if (Physics.Raycast(cameraShift, transform.TransformDirection(Vector3.forward), out hit, 30.0f, mask))
                        {
                            if (walldistance > hit.distance)
                            {
                                string hittarget = hit.transform.parent.name;
                                Debug.Log("Object '" + hittarget + "' in frame");
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            currentHoldDownTime = 0;
            orangeLight.enabled = false;
        }
    }

    public void PlayBeepSound()
    {
        BeepSound.Play();
    }

    public void PlayFlashSound()
    {
        FlashSound.Play();
    }

    public void UpdateCameraDisplay()
    {
        photo = new Texture2D(rendTex.width, rendTex.height);
        RenderTexture.active = rendTex;
        photo.ReadPixels(new Rect(0, 0, RenderTexture.active.width, RenderTexture.active.height), 0, 0);
        photo.Apply();
        TakenPhoto.texture = photo;
        TakenPhoto.color = Color.white;
        Invoke("ClearTakenPhoto", delay);
    }

    void ClearTakenPhoto()
    {
        TakenPhoto.color = Color.clear;
    }
}
