using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private CharacterState characterState;
    [SerializeField] private SoundFXManager soundFXManager;

    public CharacterState CharacterState => characterState;
    public SoundFXManager SoundFXManager => soundFXManager;
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

    public void GameFinished()
    {
        isGameOver = true;
        OnGameOver?.Invoke();
    }
}
