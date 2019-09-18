using UnityEngine;

public class SimplePlaySound : MonoBehaviour
{
    public AudioSource HoverSource;
    public AudioSource PressedSource;
    public void PlayHoverSound()
    {
        HoverSource.Play();
    }
    public void PlayPressedSound()
    {
         PressedSource.Play();
    }
}
