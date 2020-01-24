using UnityEngine;

public class LightningState : MonoBehaviour
{
	public bool lightningActive;
	
	public void SetLightningState(int state)
	{
		lightningActive = state == 1 ? true : false ;
	}
}
