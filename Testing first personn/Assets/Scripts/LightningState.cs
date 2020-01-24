using UnityEngine;

public class LightningState : MonoBehaviour
{
	public static LightningState instance;
	public bool lightningActive;

	private void Awake()
	{
		instance = this;
	}

	public void SetLightningState(int state)
	{
		lightningActive = state == 1 ? true : false ;
	}
}
