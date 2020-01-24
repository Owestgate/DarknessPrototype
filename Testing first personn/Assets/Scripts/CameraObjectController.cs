using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.AI;

public class CameraObjectController : MonoBehaviour
{
	public static CameraObjectController Instance { get; private set; }
    public int photosRemaining = 60;
    public int maxPhotos = 60;
    public float raycastDistance = 3;
    public bool isFlashing;
	public GameObject startLight;
	public GameObject startLightTwo;

	public EnemyAILevelTwo enemyScript;

    public RawImage batteryLow;
    public RawImage batteryMedium;
    public RawImage batteryHigh;
    public RawImage batteryFrame;

	public NavMeshAgent navAgent;
	public float speedMultiplier;

	[ColorUsage(false, true)] public Color normalColHdr;
    [ColorUsage(false, true)] public Color redColHdr;

	private Vector3 cameraShift;
	private LayerMask mask;
	private float walldistance = 100;

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
	public AudioSource scarySounds;
	public AudioSource scarySoundsFirst;
	public AudioSource scarySoundsNext;
	public AudioSource scarySoundsLast;
	public AudioSource scarySoundsFinale;
	public AudioSource scaryMusicFinale;
	public UnityEvent escape;
	public UnityEvent useFKey;
	public UnityEvent rightKey;
	public UnityEvent enemySpawn;
	public UnityEvent onFlash;


	public UnityEvent OnAllEvidencePickedUp;
	public LightningState lightningStateScript;
	private bool lastLightningState;

	private void Awake()
	{
		Instance = this;
	}

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

		mask = LayerMask.GetMask("FlashDetect");
	}

	void Update()
	{
		if (isFlashing)
		{
			Raycast();
		}

		if (lastLightningState != lightningStateScript.lightningActive)
		{
			if (lightningStateScript.lightningActive)
			{
				navAgent.speed += 8f;
				enemyScript.isDefending = false;
				enemyScript.meshfilter.mesh = enemyScript.killPose;
				if (navAgent.enabled && navAgent.gameObject.activeInHierarchy) navAgent.isStopped = false;
			}
			else
			{
				navAgent.speed -= 8f;
			}
		}

		lastLightningState = lightningStateScript.lightningActive;

		if (Input.GetKeyDown(KeyCode.F))
		{
			Anim.Play("CamFlashRest");
			currentHoldDownTime = 0;
			orangeLight.enabled = false;
			isFlashing = false;
			CheckCameraPhotos();
			useFKey.Invoke();
		}

		if (navAgent.gameObject.activeInHierarchy && navAgent.enabled)
		{
			navAgent.isStopped = enemyScript.isDefending;
		}

		transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, PlayerCamera.rotation, Time.deltaTime * Smoothing);
		transform.parent.position = new Vector3(PlayerCamera.position.x, PlayerCamera.position.y + verticalOffset, PlayerCamera.position.z);

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

			if (Input.GetMouseButtonUp(1))
			{
				orangeLight.enabled = false;
				currentHoldDownTime = 0;
			}

			if (Input.GetMouseButton(1))
			{
				if (orangeLight.enabled)
				{
					currentHoldDownTime += Time.deltaTime;

					if (currentHoldDownTime >= holdDownTime)
					{
						if (!isFlashing)
						{
							isFlashing = true;
							nextFlashTime = Time.time + CameraFlashRate;
							Anim.Play("CameraFlash", 0, 0);
							orangeLight.enabled = false;
							photosRemaining--;
							onFlash.Invoke();

							CheckCameraPhotos();

							cameraShift = transform.position + (transform.TransformDirection(Vector3.forward) * -2);
							Debug.Log("snap");
							RaycastHit[] hits;
							hits = Physics.RaycastAll(cameraShift, transform.TransformDirection(Vector3.forward), raycastDistance);

							for (int i = 0; i < hits.Length; i++)
							{
								if (hits[i].transform.tag == "Wall")
								{
									walldistance = hits[i].distance;
								}
							}

							Raycast();
						}
					}
				}
			}
		}
	}

	public void CheckCameraPhotos()
	{
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
			if (Anim2.gameObject.activeInHierarchy) Anim2.Play("BatteryFlash", 0, 0);
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
			if (Anim2.gameObject.activeInHierarchy) Anim2.Play("BatteryFlash 1", 0, 0);
		}

		photosRemaining = Mathf.Max(photosRemaining, 0);
	}

	void Raycast()
	{
		if (!Physics.Raycast(cameraShift, transform.forward, out RaycastHit hit, raycastDistance, mask)) return;
		
		Debug.Log("wall: " + walldistance);
		Debug.Log("hit: " + hit.distance);

		if (walldistance <= hit.distance) return;
		
		Debug.Log(hit.transform.name, hit.transform.gameObject);

		if (hit.transform.tag != "Evidence") return;
		
		Debug.Log("evidence!");

		CameraDetectPlane plane = hit.collider.transform.GetComponent<CameraDetectPlane>();
		plane.evidence.OnPhotoTaken?.Invoke();
		plane.hintMusicFadeOut.fading = true;

		Destroy(hit.transform.gameObject);
		evidenceCount += 1;

		if (evidenceCount == 3)
		{
			startLight.SetActive(false);
			startLightTwo.SetActive(false);
			scarySoundsFirst.Play();
			enemySpawn.Invoke();
			navAgent.speed = PlayerPrefs.GetInt("difficulty") == 0 ? 3f : 6f;
		}

		if (evidenceCount == 4)
		{
			scarySounds.Play();
			RenderSettings.reflectionIntensity = 0.5f;
			navAgent.speed = PlayerPrefs.GetInt("difficulty") == 0 ? 6f : 11f;
		}

		if (evidenceCount == 5)
		{
			scarySoundsNext.Play();
			navAgent.speed = PlayerPrefs.GetInt("difficulty") == 0 ? 9f : 16f;
			RenderSettings.reflectionIntensity = 0.4f;
		}

		if (evidenceCount == 6)
		{
			scarySoundsLast.Play();
			navAgent.speed = PlayerPrefs.GetInt("difficulty") == 0 ? 12f : 19f;
			RenderSettings.reflectionIntensity = 0.3f;
		}
		if (evidenceCount == 7)
		{
			scarySoundsFirst.Stop();
			scarySounds.Stop();
			scarySoundsNext.Stop();
			scarySoundsLast.Stop();
			scarySoundsFinale.Play();
			scaryMusicFinale.Play();
			RenderSettings.reflectionIntensity = 0;
			escape.Invoke();
			navAgent.speed = PlayerPrefs.GetInt("difficulty") == 0 ? 15f : 24f;
			WeatherSystem.instance.LightningTime = new Vector2(1, 3);
			WeatherSystem.instance.ResetLightningTime();
		}

		if (evidenceCount != 7) return;
		OnAllEvidencePickedUp.Invoke();
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
        isFlashing = false;
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
