using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeEffect : EffectBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReceiveEffect receiveEffect = collision.GetComponent<ReceiveEffect>();
        if (receiveEffect)
        {
            receiveEffect.ReceiveOrangeEffect();
        }
    }
}
