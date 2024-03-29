using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceSystem : MonoBehaviour
{
    public static InterfaceSystem Instance;
    public GameObject cooldownInterface;
    public GameObject mobileInterface;
    public GameObject hpInterface;
    public GameObject levelName;

    public GameObject endGame;
    public GameObject youWin;
    public GameObject youLose;

    private float limiar = 0.3f;
    private void Awake()
    {
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var name = "";
        if(scene.name == "FlowerField")
        {
            name = "Flower Field";
        } else if(scene.name == "DesertParadise")
        {
            name = "Desert Paradise";
        } else if(scene.name == "NightShift")
        {
            name = "Night Shift";
        }
        SetLevelName(name);
    }
    private void SetLevelName(string text)
    {
        levelName.GetComponent<TextMeshProUGUI>().text = text;
    }
    public void EnableLevelName()
    {
        SetAlphaLevelName(1);
        levelName.SetActive(true);
    }
    public void SetAlphaLevelName(float alpha)
    {
        if(alpha <= limiar) { levelName.SetActive(false); return; }
        var textColor = levelName.GetComponent<TextMeshProUGUI>().color;
        textColor.a = alpha;
        levelName.GetComponent<TextMeshProUGUI>().color = textColor;
    }

    public void LoseGame()
    {
        endGame.SetActive(true);
        youWin.SetActive(false);
        youLose.SetActive(true);
    }
    public void WinGame()
    {
        endGame.SetActive(true);
        youWin.SetActive(true);
        youLose.SetActive(false);
    }

    public void EnableInterface()
    {
        if(InputSystem.Instance.IsKeyboard())
        {
            cooldownInterface.SetActive(true);
        } else
        {
            mobileInterface.SetActive(true);
        }
        hpInterface.SetActive(true);
    }
    public void DisableInterface()
    {
        if (InputSystem.Instance.IsKeyboard())
        {
            cooldownInterface.SetActive(false);
        } else
        {
            mobileInterface.SetActive(false);
        }
        hpInterface.SetActive(false);
    }
}
