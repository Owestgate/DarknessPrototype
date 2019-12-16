using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    public float delay2 = 0.1f;

    private Shader shader1;
    public int evidenceCount = 0;
    public Camera renderCamera;
    public GameObject renderCam;

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

        shader1 = Shader.Find("Standard");
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
                        //renderCam.SetActive(false);
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
                        Vector3 cameraShift = transform.position + (transform.TransformDirection(Vector3.forward) * -2);
                        Debug.Log("snap");
                        RaycastHit[] hits;
                        hits = Physics.RaycastAll(cameraShift, transform.TransformDirection(Vector3.forward), 50.0f);
                        for (int i = 0; i < hits.Length; i++)
                        {
                            if (hits[i].transform.tag == "Wall")
                            {
                                walldistance = hits[i].distance;
                            }
                        }
                        if (Physics.Raycast(cameraShift, transform.TransformDirection(Vector3.forward), out hit, 30.0f, mask))
                        {
                            Debug.Log("wall: " + walldistance);
                            Debug.Log("hit: " + hit.distance);
                            if (walldistance > hit.distance)
                            {
                                Debug.Log("asdasda");
                                if (hit.transform.parent.tag == "Evidence")
                                {
                                    Debug.Log("evidence!");
                                    Destroy(hit.transform.parent.transform.Find("Plane").gameObject);
                                    hit.transform.parent.GetComponent<MeshRenderer>().material.shader = shader1;
                                    evidenceCount += 1;
                                }
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
        Invoke("ClearTakenPhoto", delay);
    }

    void ClearTakenPhoto()
    {
       // renderCam.SetActive(true);
        TakenPhoto.color = Color.clear;
    }

    public void TakeNewPhoto()
    {
        StartCoroutine(TakePhoto());
    }

    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();
        renderCamera.Render();
        TakePhotoDelayed();
    }

    public void TakePhotoDelayed()
    {
        photo = new Texture2D(rendTex.width, rendTex.height);
        RenderTexture.active = rendTex;
        photo.ReadPixels(new Rect(0, 0, RenderTexture.active.width, RenderTexture.active.height), 0, 0);
        photo.Apply();
        TakenPhoto.texture = photo;
        TakenPhoto.color = Color.white;
    }
}
