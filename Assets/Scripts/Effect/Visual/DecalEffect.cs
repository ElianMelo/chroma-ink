using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalEffect : MonoBehaviour
{
    public DecalData decalData;

    private float deactivationTime;
    private float currentTime;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        deactivationTime = decalData.deactivationTime;
        currentTime = deactivationTime;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        currentTime -= Time.deltaTime;

        Color color = spriteRenderer.color;
        color.a = currentTime / deactivationTime;

        spriteRenderer.color = color;

        if (currentTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
