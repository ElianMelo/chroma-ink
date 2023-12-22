using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    public float rotateSpeed;
    void Update()
    {
        this.transform.Rotate(0, 0, rotateSpeed);
    }
}
