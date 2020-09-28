using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void Pause()
    {
        Time.timeScale =0f;

    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

}
