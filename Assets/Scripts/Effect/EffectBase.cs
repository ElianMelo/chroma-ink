using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    private CircleCollider2D col;
    private SpriteRenderer spriteRenderer;
    private bool decreaseAlpha = false;
    private void Awake()
    {
        col = this.GetComponent<CircleCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(DisableCollider());
        // StartCoroutine(DestroyEffect());
    }
    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.EffectDisableTimer);

        var color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;

        decreaseAlpha = true;

        col.enabled = false;
    }
    //private IEnumerator DestroyEffect()
    //{
    //    yield return new WaitForSeconds(AttributeManager.Instance.EffectDestroyTimer);
    //    Destroy(this.gameObject);
    //}

    private void Update()
    {
        if(decreaseAlpha)
        {
            var color = spriteRenderer.color;
            color.a -= 0.001f;
            if (color.a <= 0)
            {
                Destroy(this.gameObject);
            }
            spriteRenderer.color = color;
        }
    }
}
