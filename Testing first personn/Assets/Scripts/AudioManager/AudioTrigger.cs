using UnityEngine;
using UnityEngine.Events;

public class AudioTrigger : MonoBehaviour
{
    public int newIndex;
    public UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            if (newIndex != 0) AudioManager.Instance.SetLayerIndex(newIndex);
            AudioManager.Instance.AddSoundtrack();
            Destroy(gameObject);
        }
    }
}
