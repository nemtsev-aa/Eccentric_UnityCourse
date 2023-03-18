using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause_Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject _pauseMenuUI;
    //[SerializeField] private UI_Description _uiDescription;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = true;
    }   

    private void Wait()
    {
        Time.timeScale = 0f;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
