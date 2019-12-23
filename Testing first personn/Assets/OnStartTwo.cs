using UnityEngine.Events;
using UnityEngine;

public class OnStartTwo : MonoBehaviour
{
    public UnityEvent OnStartedTwo;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnStartDelay", 3f);
    }
    void OnStartDelay()
    {
        OnStartedTwo.Invoke();
    }
}
