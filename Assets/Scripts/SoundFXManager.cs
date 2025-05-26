using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClipName[] audioClipNames;

    [System.Serializable]
    class AudioClipName
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        musicSource.clip = GetComponent<AudioSource>().clip;

    }
    private void OnEnable()
    {
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= PlayGameOverSound;

    }
    private void Start()
    {
        GameManager.Instance.OnGameOver += PlayGameOverSound;

      
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

    public void PlayBackGroundMusic()
    {
        musicSource.Play();
    }
    public void StopBackGroundMusic()
    {
        musicSource.Stop();
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
    playerRoll,
    playerBlock,
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
