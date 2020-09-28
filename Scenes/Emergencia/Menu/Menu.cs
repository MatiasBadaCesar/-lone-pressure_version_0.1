using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void LoadLevelManual(int levelManual)
    {
        SceneManager.LoadScene(levelManual);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
