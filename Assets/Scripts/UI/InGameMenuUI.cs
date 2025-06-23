using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject settingContent;
    

    [SerializeField] public Button buttonResume;
    [SerializeField] public Button buttonMainMenu;
    [SerializeField] public Button buttonSetting;
    [SerializeField] public Button buttonQuit;

    [SerializeField] private bool isPaused;
    private void Start()
    {
        buttonResume.onClick.AddListener(OnResumed);
        buttonMainMenu.onClick.AddListener(OnMainMenu);
        buttonSetting.onClick.AddListener(OnSetting);
        buttonQuit.onClick.AddListener(OnQuit);
        HideMenuUI(false);
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Paused();
            }
            else OnResumed();

        }
      
    }
    private void Paused()
    {
        HideMenuUI(true);
        Time.timeScale = 0f;
    }
    private void OnResumed()
    {
        HideMenuUI(false);
        isPaused = false ;
        Time.timeScale = 1f;
    }
    private void OnSetting()
    {
        HideMenuUI(false);

        settingContent.SetActive(true);


    }
    public void OnMainMenu()
    {
        string mainMenuScene = "MainMenu";
        if (Application.CanStreamedLevelBeLoaded(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);
        }
        Time.timeScale = 1f;
        //SoundFXManager.Instance.StopBackGroundMusic();
        GameManager.Instance.SoundFXManager.StopBackGroundMusic();


    }
    private void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode in the editor
#else
        Application.Quit();  // Quit the game in a build
#endif
    }

    public void HideMenuUI(bool state)
    {
        content.SetActive(state);
        backGround.SetActive(state);
        if (state == false)
        { EventSystem.current.SetSelectedGameObject(null); }
    }

}
