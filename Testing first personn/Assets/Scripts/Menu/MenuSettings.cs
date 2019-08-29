using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
//using UnityStandardAssets.Utility;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    public FirstPersonController playerCharacterController;
    public Slider sliderSensitivity;
    public Slider sliderVolume;

    void Start()
    {
        sliderSensitivity.value = playerCharacterController.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity;
        sliderSensitivity.value = playerCharacterController.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity;
        AudioListener.volume = 1.0f;
        sliderVolume.value = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        playerCharacterController.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = sliderSensitivity.value;
        playerCharacterController.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = sliderSensitivity.value;

        AudioListener.volume = sliderVolume.value;
    }
}
