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
                yield return new WaitForSeconds(.5f);
            }
        }  
    }
}
