using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSystem : MonoBehaviour
{
    public static InterfaceSystem Instance;
    public GameObject cooldownInterface;
    public GameObject hpInterface;
    private void Awake()
    {
        Instance = this;
    }
    public void EnableInterface()
    {
        cooldownInterface.SetActive(true);
        hpInterface.SetActive(true);
    }
    public void DisableInterface()
    {
        cooldownInterface.SetActive(false);
        hpInterface.SetActive(false);
    }
}
