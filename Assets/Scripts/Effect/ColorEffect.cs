using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEffect : MonoBehaviour
{
    public GameObject redEffectPrefab;
    public GameObject blueEffectPrefab;
    public GameObject yellowEffectPrefab;
    public GameObject orangeEffectPrefab;
    public GameObject greenEffectPrefab;
    public GameObject purpleEffectPrefab;

    public static ColorEffect Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void InvokeEffect(Vector3 position, EffectType type)
    {
        Instantiate(GetObjectByEffect(type), position, Quaternion.identity);
    }

    public GameObject GetObjectByEffect(EffectType type)
    {
        switch (type)
        {
            case EffectType.Red:
                return redEffectPrefab;
            case EffectType.Blue:
                return blueEffectPrefab;
            case EffectType.Yellow:
                return yellowEffectPrefab;
            case EffectType.Orange:
                return orangeEffectPrefab;
            case EffectType.Green:
                return greenEffectPrefab;
            case EffectType.Purple:
                return purpleEffectPrefab;
        }
        return null;
    }
}
