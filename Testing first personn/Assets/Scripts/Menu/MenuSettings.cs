using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuSettings : MonoBehaviour
{
    public FirstPersonController playerCharacterController;
    public Slider sliderSensitivity;
    public Slider sliderVolume;

    void Start()
    {
        if (playerCharacterController != null)
        {
            if (playerCharacterController.m_MouseLook != null)
            {
                if (sliderSensitivity != null)
                {
                    sliderSensitivity.value = playerCharacterController.m_MouseLook.XSensitivity;
                    sliderSensitivity.value = playerCharacterController.m_MouseLook.YSensitivity;
                }
            }
        }
       
        AudioListener.volume = 1.0f;
        sliderVolume.value = AudioListener.volume;
    }

    void Update()
    {
        if (playerCharacterController != null)
        {
            if (playerCharacterController.m_MouseLook != null)
            {
                playerCharacterController.m_MouseLook.XSensitivity = sliderSensitivity.value;
                playerCharacterController.m_MouseLook.YSensitivity = sliderSensitivity.value;
            }
        }
        
        AudioListener.volume = sliderVolume.value;
    }
}
