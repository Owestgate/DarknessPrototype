using UnityEngine;
using UnityEngine.Events;

public class EvidenceTextSpawn : MonoBehaviour
{
	public UnityEvent OnEvent;

	public void Event()
	{
		OnEvent.Invoke();
	}
}

