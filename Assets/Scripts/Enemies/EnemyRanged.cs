using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    public GameObject prefabProjectile;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AttackDelay(3));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    IEnumerator AttackDelay(int DelayTime)
    {
        while(true)
        {
            if(canAct)
            {
                Instantiate(prefabProjectile, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(DelayTime);
            }
            else
            {
                yield return null;
            }
        }  
    }
}
