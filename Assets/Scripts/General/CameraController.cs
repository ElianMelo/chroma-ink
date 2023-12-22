using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraControllerData cameraControllerData;

    private GameObject target;
    private Camera cameraComponent;
    private EnemySpawner spawner;

    private bool followPlayer = false;
    private bool showArena = false;

    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        target = FindObjectOfType<MovementManager>().gameObject;
        spawner = FindObjectOfType<EnemySpawner>();
        cameraComponent.orthographicSize = cameraControllerData.targetSize;
        AttributeManager.Instance.paused = true;
        showArena = true;
        InterfaceSystem.Instance.EnableLevelName();
    }

    public void FollowPlayer()
    {
        
        followPlayer = true;
    }

    void FixedUpdate()
    {
        if(showArena)
        {
            if (cameraComponent.orthographicSize >= cameraControllerData.startSize)
            {
                InterfaceSystem.Instance.SetAlphaLevelName(cameraComponent.orthographicSize / cameraControllerData.targetSize);
                cameraComponent.orthographicSize -= cameraControllerData.incrementValue;
            }
            else { 
                showArena = false; 
                followPlayer = true; 
                spawner?.StartSpawner();
                AttributeManager.Instance.paused = false;
            }
        }

        if(followPlayer)
        {
            // float targetX = target.transform.position.x;
            // float targetY = target.transform.position.y;

            // this.transform.position = new Vector3(targetX, targetY, this.transform.position.z);

            // var step = speed * Time.fixedDeltaTime;
            // transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY, this.transform.position.z), step);
            transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref currentVelocity, 0.1f);
        }    
    }

}
