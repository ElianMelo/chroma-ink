using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void FixedUpdate()
    {
        if(AttributeManager.Instance.paused) { return; }
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        this.transform.position = worldPosition;
    }
}
