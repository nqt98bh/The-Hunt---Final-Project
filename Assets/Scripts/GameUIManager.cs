using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject restartButton;
    private void Start()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.OnGameOver += ShowGameOverPanel;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= ShowGameOverPanel;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        restartButton.SetActive(true);
        gameOverText.text = "Game Over!";

    }
    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        
    }
}
