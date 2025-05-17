using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;

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
        gameOverText.text = "Game Over!";
    }
}
