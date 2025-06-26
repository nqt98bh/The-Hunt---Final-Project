using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    //public static SoundFXManager Instance;
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
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else Destroy(gameObject);
        musicSource.clip = GetComponentInChildren<AudioSource>().clip;
        soundFX = GetComponentInChildren<AudioSource>();

    }
  
    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= PlayGameOverSound;
        GameManager.Instance.OnGameFinish -= PlayFinishSound;
    }
    private void Start()
    {
        GameManager.Instance.OnGameOver += PlayGameOverSound;
        GameManager.Instance.OnGameFinish += PlayFinishSound;
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
    private void PlayFinishSound()
    {
        musicSource.Stop();
        PlaySound(SoundType.gameFinish);

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
    playerAttack1,
    playerAttack2,
    playerAttack3,
    playerCollect,
    playerDeath,
    blockDamage,


    enemyHit,
    enemyDeath,
    enemyAttack,

    music,
    gameOver,
    gameFinish,

    MenuSelected,
    MenuHighlight,
}
