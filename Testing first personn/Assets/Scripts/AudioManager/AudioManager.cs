using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public int layerIndex;
    public AudioSource[] sources;

    private void Awake()
    {
        Instance = this;
    }

    public void AddSoundtrack()
    {
        if (layerIndex >= sources.Length) return;

        sources[layerIndex].Play();

        if (layerIndex < sources.Length - 1)
        {
            if (layerIndex != 0)
            {
                sources[layerIndex].time = sources[0].time;
            }

            layerIndex++;
        }
        else
        {
            for (int i = 0; i < sources.Length - 1; i++)
            {
                sources[i].Stop();
            }
        }
    }

    public void SetLayerIndex(int index)
    {
        layerIndex = index;
    }
}
