using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFlower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RedAttack"))
        {
            ColorEffect.Instance.InvokeEffect(this.transform.position, EffectType.Orange);
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("BlueAttack"))
        {
            ColorEffect.Instance.InvokeEffect(this.transform.position, EffectType.Green);
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("YellowAttack"))
        {
            ColorEffect.Instance.InvokeEffect(this.transform.position, EffectType.Yellow);
            Destroy(this.gameObject);
        }
    }
}
