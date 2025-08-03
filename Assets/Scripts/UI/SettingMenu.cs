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
    public Image muteIconImage;       
    public Sprite iconUnmuted;        
    public Sprite iconMuted;

    private bool isMuted = false; 

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

    public void OnMutedMasterButton(string name)
    {
        if(!isMuted)
        {
            isMuted = true;
            mainAudioMixer.SetFloat(name, -80);
            
        }
        else
        {
            isMuted = false;
            mainAudioMixer.SetFloat(name, 0);
        }
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (muteIconImage == null) return;
        if (isMuted)
        {
            muteIconImage.sprite = iconMuted;
        }
        else
        {
            muteIconImage.sprite = iconUnmuted;
        }
    }
}
