using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public TMP_Dropdown graphicDropDown;
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;
    [SerializeField] private GameObject MenuContent;
    [SerializeField] private GameObject MenuBackground;
    public void ChangeGraphicQuality()
    {
        QualitySettings.SetQualityLevel(graphicDropDown.value);
    }
    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("Master",masterVol.value);
    }
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("Music", musicVol.value);
    }
    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFX", sfxVol.value);
    }
    public void OnBackButton()
    {
        gameObject.SetActive(false);
        MenuContent.SetActive(true);
        MenuBackground.SetActive(true);
    }
}
