using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject settingContent;


    [SerializeField] public Button buttonContinue;
    [SerializeField] public Button buttonNewGame;
    [SerializeField] public Button buttonSetting;
    [SerializeField] public Button buttonQuit;

    private void Start()
    {
        buttonContinue.onClick.AddListener(OnContinue);
        buttonNewGame.onClick.AddListener(OnNewGame);
        buttonSetting.onClick.AddListener(OnSetting);
        buttonQuit.onClick.AddListener(OnQuit);
    }

    private void OnContinue()
    {
        string mainMenuScene = "InGame";
        if (Application.CanStreamedLevelBeLoaded(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);
        }

    }
    private void OnNewGame()
    {
        string mainMenuScene = "InGame";
        if (Application.CanStreamedLevelBeLoaded(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
    private void OnSetting()
    {

        settingContent.SetActive(true);
    }
   

    private void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode in the editor
#else
        Application.Quit();  // Quit the game in a build
#endif
    }

    private void HideAllMenu(bool state)
    {
        backGround.SetActive(state);
        content.SetActive(state);
        if(state == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
