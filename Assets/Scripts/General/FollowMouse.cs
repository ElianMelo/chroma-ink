using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if(AttributeManager.Instance.paused) { return; }
        Vector3 worldPosition = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform);

        this.transform.position = worldPosition;
    }
}
