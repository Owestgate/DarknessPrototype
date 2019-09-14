using UnityEngine;

public class LightRoomTest : MonoBehaviour
{
    public bool shouldBypass;

    public Collider playerCol;

    private void OnTriggerEnter(Collider other)
    {
        if (other != playerCol) return;
        RoomLights.Instance.bypass = shouldBypass;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != playerCol) return;
        RoomLights.Instance.bypass = shouldBypass;
    }
}
