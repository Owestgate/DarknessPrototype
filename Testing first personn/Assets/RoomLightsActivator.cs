using UnityEngine;
using UnityEngine.Events;

public class RoomLightsActivator : MonoBehaviour
{
    public float startTime;

    public UnityEvent OnTimerComplete;

    private void Start()
    {
        Invoke("TimerComplete", startTime);
    }

    void TimerComplete()
    {
        OnTimerComplete.Invoke();
    }
}
