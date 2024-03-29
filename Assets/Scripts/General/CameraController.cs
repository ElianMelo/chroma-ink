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
        AttributeManager.Instance.SetPaused(true);
        showArena = true;
        InterfaceSystem.Instance.EnableLevelName();
        InterfaceSystem.Instance.DisableInterface();
        spawner?.DisableWaveText();
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
                InterfaceSystem.Instance.EnableInterface();
                showArena = false; 
                followPlayer = true; 
                spawner?.StartSpawner();
                spawner?.EnableWaveText();
                AttributeManager.Instance.SetPaused(false);
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

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeAction(duration, magnitude));
    }

    private IEnumerator ShakeAction(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        followPlayer = false;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = originalPosition;
        followPlayer = true;
        // StartCoroutine(cameraShake.Shake(.15f, .4f));
        // CameraShaker.Instance.ShakeOnce(4f,4f,.1f,1f);
        // Ez Camera Shake
    }

}
