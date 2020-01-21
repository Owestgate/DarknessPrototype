using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

public class EvidenceCompletion : MonoBehaviour
{

	public List<GameObject> spawnPoints; // 8 spawn points
	public List<GameObject> hardSpawnPoints;
	public List<GameObject> enemySpawnPoints; // 10 spawn points
	public List<GameObject> enemyHardSpawnPoints;
	public List<Transform> pieces; // 5 pieces + extra empty spots so placements change every game

	public List<Transform> enemy;

	public Animator doorOpenAnimator; // change to what ever animation we use
	public AudioSource doorAudio;

	private bool doorOpened = false;

	void Start()
	{
		RandomiseSpawnList();
		EnemyRandomiseSpawnList();
		SpawnPositions(pieces);
		EnemySpawnPositions(enemy);
	}

	//spawn objects
	public void SpawnPositions(List<Transform> objects)
	{
		if (PlayerPrefs.GetInt("difficulty") == 1)
		{
			for (int i = 0; i < objects.Count; i++)
			{
				objects[i].transform.position = hardSpawnPoints[i].transform.position;
				objects[i].transform.rotation = hardSpawnPoints[i].transform.rotation;
			}
		}
		else
		{
			for (int i = 0; i < objects.Count; i++)
			{
				objects[i].transform.position = spawnPoints[i].transform.position;
				objects[i].transform.rotation = hardSpawnPoints[i].transform.rotation;
			}
		}
	}

	public List<GameObject> usedSpawnPoints = new List<GameObject>();

	// Randomise order of puzzle pieces
	void RandomiseSpawnList()
	{
		usedSpawnPoints.Clear();

		if (PlayerPrefs.GetInt("difficulty") == 1 || PlayerPrefs.GetInt("difficulty") == 2)
		{
			//for (int i = 0; i < hardSpawnPoints.Count; i++)
			//{
			//    GameObject tempColor = hardSpawnPoints[i];
			//    int randomIndex = UnityEngine.Random.Range(i, hardSpawnPoints.Count);
			//    hardSpawnPoints[i] = hardSpawnPoints[randomIndex];
			//    hardSpawnPoints[randomIndex] = tempColor;
			//}

			IListExtensions.Shuffle(hardSpawnPoints);
		}
		else
		{
			IListExtensions.Shuffle(spawnPoints);
		}
	}



	//spawn enemy
	public void EnemySpawnPositions(List<Transform> objects)
	{
		if (PlayerPrefs.GetInt("difficulty") == 1)
		{
			for (int i = 0; i < objects.Count; i++)
			{
				objects[i].transform.position = enemyHardSpawnPoints[i].transform.position;
				objects[i].transform.rotation = enemyHardSpawnPoints[i].transform.rotation;
			}
		}
		else
		{
			for (int i = 0; i < objects.Count; i++)
			{
				objects[i].transform.position = enemySpawnPoints[i].transform.position;
				objects[i].transform.rotation = enemyHardSpawnPoints[i].transform.rotation;
			}
		}
	}

	public List<GameObject> EnemyusedSpawnPoints = new List<GameObject>();

	// Randomise order of enemySpawns
	void EnemyRandomiseSpawnList()
	{
		EnemyusedSpawnPoints.Clear();

		if (PlayerPrefs.GetInt("difficulty") == 1 || PlayerPrefs.GetInt("difficulty") == 2)
		{
			//for (int i = 0; i < hardSpawnPoints.Count; i++)
			//{
			//    GameObject tempColor = hardSpawnPoints[i];
			//    int randomIndex = UnityEngine.Random.Range(i, hardSpawnPoints.Count);
			//    hardSpawnPoints[i] = hardSpawnPoints[randomIndex];
			//    hardSpawnPoints[randomIndex] = tempColor;
			//}

			IListExtensions.Shuffle(enemyHardSpawnPoints);
		}
		else
		{
			IListExtensions.Shuffle(enemySpawnPoints);
		}
	}
}

public static class IListExtensions
{
	/// <summary>
	/// Shuffles the element order of the specified list.
	/// </summary>
	public static void Shuffle<T>(this IList<T> ts)
	{
		var count = ts.Count;
		var last = count - 1;
		for (var i = 0; i < last; ++i)
		{
			var r = UnityEngine.Random.Range(i, count);
			var tmp = ts[i];
			ts[i] = ts[r];
			ts[r] = tmp;
		}
	}
}


