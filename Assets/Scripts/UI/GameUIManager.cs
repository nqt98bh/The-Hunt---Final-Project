using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject MenuUI;
    public static Action refreshUI;
  
    private void Start()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.OnGameOver += ShowGameOverPanel;
        refreshUI += RefreshUI;

    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= ShowGameOverPanel;
        refreshUI -= RefreshUI;

    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        MenuUI.SetActive(true);
        gameOverText.text = "Game Over!";

    }
    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        MenuUI.SetActive(false);
        
    }
    public void RefreshUI()
    {
        CurrencyManager.OnCoinChanged?.Invoke();
    }
}
