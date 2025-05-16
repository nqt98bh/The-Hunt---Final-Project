using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private CharacterState characterState;

    public CharacterState CharacterState => characterState;
    [HideInInspector] public bool isGameOver = false;

    public Action GameFinished;
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
        GameFinished += OnGameFinished;
        isGameOver = false;
    }
    private void OnDestroy()
    {
         GameFinished -= OnGameFinished;
    }
    private void OnGameFinished()
    {
        isGameOver = true;
    }
}
