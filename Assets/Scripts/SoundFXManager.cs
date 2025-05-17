using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClipName[] audioClipNames;

    [System.Serializable]
    class AudioClipName
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += PlayGameOverSound;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= PlayGameOverSound;

    }
    private void Start()
    {
        PlayMusic();
    }
    private AudioClip GetSoundType(SoundType sound)
    {
        AudioClip audioClip = null;
        for (int i = 0;i < audioClipNames.Length; i++)
        {
            if (audioClipNames[i].soundType == sound)
            {
                audioClip = audioClipNames[i].audioClip;
                break;
            }
        }
        return audioClip;
    }
    public void PlaySound(SoundType soundType)
    {
        soundFX.PlayOneShot(GetSoundType(soundType));
    }

    private void PlayMusic()
    {
        musicSource.clip = GetComponent<AudioSource>().clip;
        musicSource.Play();
    }
    private void PlayGameOverSound()
    {
        PlaySound(SoundType.playerDeath);
        musicSource.Stop();
        PlaySound(SoundType.gameOver);
    }
}
public enum SoundType
{
    playerRun,
    playerJump,
    playerHit,
    playerWallSliding,
    playerAttack,
    playerCollect,
    playerDeath,

    enemyHit,
    enemyDeath,
    enemyAttack,

    music,
    gameOver,
}
