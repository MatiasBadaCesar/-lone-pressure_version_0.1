using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject goalsPanel;
    public GameObject exitPanel;

    public GameObject pausePanel;
    public GameObject restartPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void SettingOn()
    {

    }


    public void GoalsOn()
    {

    }

    public void ExitAdvise()
    {
        exitPanel.SetActive(true);
    }

    public void Exit(bool desition)
    {
        if(desition == true)
        {
            Application.Quit();
        }
        else
        {
            exitPanel.SetActive(false);
        }
    }

    public void pauseOn(bool pause)
    {
        if (pause == true)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void QuitLevel(bool quit)
    {
        if(quit == true)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            exitPanel.SetActive(false);
        }
    }

    public void RestartLevel(bool restart)
    {
        if (restart == true)
        {
            SceneManager.LoadScene("Nivel1");
            Time.timeScale = 1.0f; //lo deje por el timeScale para la pausa
        }
        else
        {
            restartPanel.SetActive(false);
        }
    }

    public void PanelOn(GameObject panel)
    {
        panel.SetActive(true);
    }
}
