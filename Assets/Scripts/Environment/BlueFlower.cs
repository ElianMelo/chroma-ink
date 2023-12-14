using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFlower : Flower
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RedAttack"))
        {
            ColorEffect.Instance.InvokeEffect(this.transform.position, EffectType.Purple);
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("BlueAttack"))
        {
            ColorEffect.Instance.InvokeEffect(this.transform.position, EffectType.Blue);
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("YellowAttack"))
        {
            ColorEffect.Instance.InvokeEffect(this.transform.position, EffectType.Green);
            Destroy(this.gameObject);
        }
    }
}
