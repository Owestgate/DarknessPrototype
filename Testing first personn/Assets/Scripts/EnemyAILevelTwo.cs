using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyAILevelTwo : MonoBehaviour
{
	public static EnemyAILevelTwo Instance;
	public GameObject playerCharacter;
	public GameObject PlyrKillScreen;
	public Transform raycastOrigin;
	public Transform raycastEnd;
	public NavMeshAgent navAgent;
	public bool FlashInRange;
	public Mesh[] models; //These are the two models currently. Its currently set up for two only but it shouldnt take too long to make space for more if we want more
	public Mesh flashedPose;
	public Mesh killPose;
	public float navSpeed;
	public float hardSpeed;
	public float nightmareSpeed;
	public AudioSource EnemyCloseSound;
	public AudioSource EnemyCloseSound2;
	public AudioSource EnemyCloseSound3;
	public AudioSource EnemyCloseSound4;
	public AudioSource EnemyCloseSound5;
	public AudioSource flashedSound;
	private float distToPlayer;
	private float speedMultiplier;
	public MeshFilter meshfilter;

	public CameraObjectController camController;

	public bool isInTrigger;

	public Transform Player;
	public Collider enemyChecker;
	//public GameObject AttackingPose;
	//public GameObject AttackingPoseTwo;
	//public GameObject DeathPose;

	//public GameObject DefendingPose;
	public bool isDefending;
	public float DefendingTimerRemain;
	public float DefendingTimerDuration;

	public Collider AngelAttackCol;

	private MeshFilter modelSlot;
	private bool lightsJustOn = false; //The enemy figures out the exact moment the lights switch, and thats what lightsJustOn is
	private bool currentModel;

	public AudioSource[] enemySounds;
	public float hardPitch = 1.3f;

	public Material normalMat;
	public Material brutalMat;
	public GameObject brutalEffects;

	public LayerMask ColLayerMask;
	private void Awake()
	{
		Instance = this;
	}
	void Start()
	{
		OnCameraOff();
		DefendingTimerRemain = 0;
		isDefending = false;

		modelSlot = GetComponent<MeshFilter>();
		playerCharacter = GameObject.FindGameObjectWithTag("Character");
		navAgent = GetComponent<NavMeshAgent>();

		int difficulty = PlayerPrefs.GetInt("difficulty");
		Debug.Log(difficulty);
		//Difficulty 
		if (difficulty == 2)
		{
			navSpeed = nightmareSpeed;
			gameObject.GetComponent<MeshRenderer>().material = brutalMat;
			brutalEffects.SetActive(true);
		}
		if (difficulty == 1)
		{
			navSpeed = hardSpeed;
			gameObject.GetComponent<MeshRenderer>().material = brutalMat;
			brutalEffects.SetActive(true);
			for (int i = 0; i < enemySounds.Length; i++)
			{
				enemySounds[i].pitch = hardPitch;
			}
		}
		if (difficulty == 0)
		{
			navSpeed = navAgent.speed;
			gameObject.GetComponent<MeshRenderer>().material = normalMat;
			brutalEffects.SetActive(false);
		} // --


	}

	// Update is called once per frame
	void Update()
	{
		//OnCameraOff();
		bool wasStopped = navAgent.isStopped;
		if (LightningState.instance.lightningActive)
		{
			isDefending = false;
			meshfilter.mesh = killPose;
			if (navAgent.enabled && navAgent.gameObject.activeInHierarchy) navAgent.isStopped = false;
		}
		else
		{
			if (!camController.gameObject.activeInHierarchy)
			{
				if (navAgent.enabled && navAgent.gameObject.activeInHierarchy) navAgent.isStopped = wasStopped;

			}
			
		}

		CheckDefendingTimer();

		if (PlyrKillScreen.GetComponent<KillScreenLevelTwo>().jumpScare2 == false)
		{
		}
		if (navAgent.enabled)
		{
			if (camController.isActiveAndEnabled)
			{
				NavMeshPath path = new NavMeshPath();
				if (NavMesh.CalculatePath(transform.position, playerCharacter.transform.position, NavMesh.AllAreas, path))
				{
					navAgent.SetPath(path);
				}
			}

			if (PlyrKillScreen.GetComponent<KillScreenLevelTwo>().jumpScare2 == true)
			{
				modelSlot.mesh = killPose;
			}
		}
	}

	void CheckDefendingTimer()
	{
		if (isDefending == true && DefendingTimerRemain > 0)
		{
			DefendingTimerRemain -= Time.deltaTime;
		}

		if (DefendingTimerRemain <= 0 && isDefending == true)
		{
			if (camController.gameObject.activeInHierarchy)
			{
				isDefending = false;
				navAgent.isStopped = false;
				meshfilter.mesh = models[Random.Range(0, models.Length)];
				transform.LookAt(Player.transform);

				Vector3 LookRotation = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
				transform.eulerAngles = LookRotation;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// Enemy is exposed to the collider light.
		if (other == enemyChecker)
		{
			isInTrigger = true;
			Debug.Log("Was hit with flash trigger.");
			RaycastToPlayer();
		}
	}

	public void CheckDefend()
	{
		if (isInTrigger)
		{
			isDefending = true;
			meshfilter.mesh = flashedPose;
			flashedSound.Play();
		}
	}

	void OnTriggerStay(Collider other)
	{
		// Enemy is exposed to the collider light.
		if (other == enemyChecker)
		{
			if (isInTrigger)
			{
				DefendingTimerRemain = DefendingTimerDuration; // Reset defendng timer.
			}
		}
	}

	public void RaycastToPlayer()
	{
		if (Physics.Raycast(raycastOrigin.position, raycastEnd.position - raycastOrigin.position, out RaycastHit hit, 1000f, ColLayerMask))
		{
			Debug.DrawRay(raycastOrigin.position, raycastEnd.position - raycastOrigin.position, Color.yellow, 1);
			Debug.Log(hit.collider.gameObject.name);

			if (hit.collider.tag == "Character")
			{
				Debug.Log("Found charater.");
				Debug.DrawRay(raycastOrigin.position, raycastEnd.position - raycastOrigin.position, Color.red, 1);
				CheckDefend();
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		// Enemy is not exposed to the collider light.
		if (other == enemyChecker)
		{
			if (isInTrigger)
			{
				isInTrigger = false;
				DefendingTimerRemain = DefendingTimerDuration; // Reset defendng timer.
				meshfilter.mesh = models[Random.Range(0, models.Length)];
			}
		}
	}
	public void OnCameraOff()
	{
		// Enemy will stop if player turns off cam.

		if (!camController.gameObject.activeInHierarchy)
		{
			isDefending = true;
			meshfilter.mesh = flashedPose;
			if (navAgent.enabled && navAgent.gameObject.activeInHierarchy) navAgent.isStopped = true;
		}
	}
}

