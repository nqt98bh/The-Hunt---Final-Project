using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private SoundFXManager soundFXManager;
    [SerializeField] private GameUIManager gameUIManager;

    public CharacterController CharacterController => characterController;
    public SoundFXManager SoundFXManager => soundFXManager;
    public GameUIManager GameUIManager => gameUIManager;
    [HideInInspector] public bool isGameOver = false;
  

    public Action OnGameOver;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

    }
    private void Start()
    {
        DataPersistenceManager.Instance.LoadGame();
    }

    public void GameFinished()
    {
        isGameOver = true;
        OnGameOver?.Invoke();
        

    }
    public void RestartGame()
    {
        isGameOver = false;
        characterController.RestartGame();
        gameUIManager.RestartGame();
    }
    public void PlaySoundFX(SoundType soundType)
    {
        SoundFXManager.PlaySound(soundType);
    }
}
