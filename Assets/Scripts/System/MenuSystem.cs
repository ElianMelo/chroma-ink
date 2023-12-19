using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    private bool enableInterface = false;
    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(enableInterface){ InterfaceSystem.Instance.EnableInterface(); enableInterface = false; }
    }
    public void NewGame()
    {
        LevelManager.Instance.ResetGame();
        enableInterface = true;
    }
    public void Tutorial()
    {

    }
    public void Credits()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
