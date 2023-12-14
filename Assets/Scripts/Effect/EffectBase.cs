using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    private CircleCollider2D col;
    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        StartCoroutine(DisableCollider());
        StartCoroutine(DestroyEffect());
    }
    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.EffectDisableTimer);
        col.enabled = false;
    }
    private IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.EffectDestroyTimer);
        Destroy(this.gameObject);
    }
}
