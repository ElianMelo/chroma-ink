using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        AttributeManager.Instance.paused = true;
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    public void Continue()
    {
        AttributeManager.Instance.paused = false;
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
