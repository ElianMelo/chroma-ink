using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<string> scenes = new List<string>();
    private List<string> scenesTemp = new List<string>();
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        Instance = this;
    }

    public void ResetGame()
    {
        scenesTemp.Clear();
        scenes.ForEach(s => { scenesTemp.Add(s); });
        EndLevelNoCards();
    }

    public void EndGame()
    {
        InterfaceSystem.Instance.DisableInterface();
        AttributeManager.Instance.ResetAttributeManager();
        HealthUI.Instance.UpdateHealth();
        SceneManager.LoadScene("Menu");
    }

    public void EndLevelCards()
    {
        if (scenesTemp.Count <= 0)
        {
            EndGame();
        } else
        {
            CardManager.Instance.CallCards();
        }
    }

    public void EndLevelNoCards()
    {   
        var sceneIndex = UnityEngine.Random.Range(0, scenesTemp.Count);
        var scene = scenesTemp[sceneIndex];
        scenesTemp.Remove(scene);
        SceneManager.LoadScene(scene);
    }
}
