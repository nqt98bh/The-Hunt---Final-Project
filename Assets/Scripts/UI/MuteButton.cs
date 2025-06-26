using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite unMuted;
    [SerializeField] private Sprite muted;

    [SerializeField] private Slider volume;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Vector2 volumeValue = new Vector2(-80f, 0f);
    private bool isMuted = false;
    public void OnMuteButton(string name)
    {
        if (!isMuted)
        {
            isMuted = true;
            volume.value = volumeValue.x;
            audioMixer.SetFloat(name, volume.value);
            image.sprite = muted;
            Debug.Log("Mute state: " + isMuted);
            Debug.Log("image: " +image.sprite.name);
        }
        else
        {
            isMuted = false;
            volume.value = volumeValue.y;
            audioMixer.SetFloat(name, volume.value);
            image.sprite = unMuted;
            
        }
    }
}
