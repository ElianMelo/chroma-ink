using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFlower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RedAttack"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("BlueAttack"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("YellowAttack"))
        {
            Destroy(this.gameObject);
        }
    }
}
