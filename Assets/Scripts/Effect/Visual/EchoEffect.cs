using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    private bool useEffect = false;

    public GameObject echo;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(useEffect)
        {
            if (timeBtwSpawns <= 0)
            {
                // spawn echo game object
                var echoInst = Instantiate(echo, transform.position, Quaternion.identity);
                echoInst.transform.rotation = transform.rotation;
                Destroy(echoInst, 0.5f);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
    public void ActivateEffect()
    {
        useEffect = true;
    }

    public void DeactivateEffect()
    {
        useEffect = false;
    }
}
