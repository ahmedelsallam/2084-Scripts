using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject exitDialogMenu;
//    public GameObject gamePlayMenu;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        exitDialogMenu.SetActive(false);
        pauseMenu.SetActive(false);
//        gamePlayMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
//        gamePlayMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
//        gamePlayMenu.SetActive(true);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        exitDialogMenu.SetActive(true);
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void CancelExit()
    {
        exitDialogMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitDialogMenu.gameObject.activeSelf)
            {
                exitDialogMenu.gameObject.SetActive(false);
                return;
            }
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex > 0)
            {
                if (pauseMenu.gameObject.activeSelf)
                {
                    Resume();
                }
                else Pause();
            }
            else
            {
                Exit();
            } 
        }
    }

}
