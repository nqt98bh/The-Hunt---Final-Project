using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class MenuSoundFX : MonoBehaviour
{
    [SerializeField] private AudioClip highlight;
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayHighlightSound()
    {
        audioSource.PlayOneShot(highlight);
    }
    public void PlaySelectSound()
    {
        audioSource.PlayOneShot(select);
    }
}
