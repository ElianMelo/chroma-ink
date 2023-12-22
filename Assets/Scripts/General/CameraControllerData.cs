using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CameraControllerData", order = 3)]
public class CameraControllerData : ScriptableObject
{
    [Header("Zoom in/out config")]
    public float startSize = 10f;
    public float targetSize = 40f;
    public float incrementValue = 0.1f;
    public GameObject floatingText;
}
