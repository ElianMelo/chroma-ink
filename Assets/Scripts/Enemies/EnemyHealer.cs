using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : Enemy
{
    private GameObject healCollider;

    protected override void Start()
    {
        healCollider = transform.Find("HealCollider")?.gameObject;
        base.Start();

        GameObject[] EnemiesOnScene = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in EnemiesOnScene)
        {
            if (enemy.name == "EnemyRanged")
            {
                target = enemy.transform;
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        //GameObject[] EnemiesOnScene = GameObject.FindGameObjectsWithTag("Enemy");

        //foreach (GameObject enemy in EnemiesOnScene)
        //{
        //    //se a vida desse inimigo for menor que 100%
        //    //target == enemy.transform;
        //}

        if (canAct)
        {
            StartCoroutine(HealDelay(2));
            target = null;
            canAct = false;

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 runTo = transform.position + ((transform.position - other.transform.position) * 1);
            agent.SetDestination(runTo);
        }
    }

    IEnumerator HealDelay(int DelayTime)
    {
        healCollider.SetActive(true);
        yield return new WaitForSeconds(DelayTime);
        healCollider.SetActive(false);
    }
}
