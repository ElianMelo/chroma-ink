using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public float speed;
    private GameObject target;
    void Start()
    {
        target = FindObjectOfType<MovementManager>().gameObject;
    }
    void LateUpdate()
    {
        float targetX = target.transform.position.x;
        float targetY = target.transform.position.y;

        this.transform.position = new Vector3(targetX, targetY, this.transform.position.z);

        // var step = speed * Time.fixedDeltaTime; // calculate distance to move
        // transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY, this.transform.position.z), step);
        // Vector3.SmoothDamp
    }

}
