using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    private void Update()
    {
        if (InputSystem.Instance.Escape() && SceneManager.GetActiveScene().name != "Menu")
        {
            Pause();
        }
    }

    public void Pause()
    {
        AttributeManager.Instance.SetPaused(true);
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    public void Continue()
    {
        AttributeManager.Instance.SetPaused(false);
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void Exit()
    {
        Continue();
        LevelManager.Instance.EndGame();
    }
}
