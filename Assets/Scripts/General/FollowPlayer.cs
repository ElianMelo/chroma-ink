using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float speed = 25f;
    private GameObject target;
    void Start()
    {
        target = FindObjectOfType<MovementManager>().gameObject;
    }
    void FixedUpdate()
    {
        float targetX = target.transform.position.x;
        float targetY = target.transform.position.y;

        // this.transform.position = new Vector3(targetX, targetY, this.transform.position.z);

        var step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY, this.transform.position.z), step);        
    }

}
