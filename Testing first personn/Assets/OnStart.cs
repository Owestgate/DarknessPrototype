using UnityEngine.Events;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    public UnityEvent OnStarted;
    public UnityEvent OnStartedTwo;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnStartDelay", 0.1f);
    }
    void OnStartDelay()
    {
        OnStarted.Invoke();
    }
}
