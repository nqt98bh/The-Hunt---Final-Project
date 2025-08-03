using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject gameFinishPanel;

    [SerializeField] private GameObject MenuUI;
    public static Action refreshUI;
  
    private void Start()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.OnGameOver += ShowGameOverPanel;
        GameManager.Instance.OnGameFinish += ShowGameFinishPanel;

        refreshUI += RefreshUI;

    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameFinish -= ShowGameFinishPanel;
        GameManager.Instance.OnGameOver -= ShowGameOverPanel;
        refreshUI -= RefreshUI;

    }

    public void ShowGameOverPanel( )
    {
        gameOverPanel.SetActive(true);
        MenuUI.SetActive(true);

    }
    public void ShowGameFinishPanel()
    {
        gameFinishPanel.SetActive(true);
        //MenuUI.SetActive(true);

    }
    //public void RestartGame()
    //{
    //    gameOverPanel.SetActive(false);
    //    MenuUI.SetActive(false);

    //}
    public void RefreshUI()
    {
        CurrencyManager.OnCoinChanged?.Invoke();
        CharacterController.OnHealthChanged?.Invoke();
    }
}
