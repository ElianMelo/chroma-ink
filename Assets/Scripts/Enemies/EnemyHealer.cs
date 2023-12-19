using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealer : Enemy
{
    public float healDelayTime = 2f;
    private bool canPerform = true;
    private GameObject healCollider;

    protected override void Start()
    {
        healCollider = transform.Find("HealCollider")?.gameObject;
        base.Start();
        target = null;

        InvokeRepeating("FindEnemy", 0f, 2f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canAct && canPerform)
        {
            StartCoroutine(HealDelay(healDelayTime));
            target = null;
            canAct = false;
            canPerform = false;
        }
    }

    IEnumerator HealDelay(float DelayTime)
    {
        healCollider.SetActive(true);
        yield return new WaitForSeconds(.2f);
        healCollider.SetActive(false);
        FindEnemy();
        yield return new WaitForSeconds(DelayTime);
        canPerform = true;
    }

    private void FindEnemy()
    {
        GameObject[] EnemiesOnScene = GameObject.FindGameObjectsWithTag("Enemy");
        var minDistance = Mathf.Infinity;
        Transform targetToFollow = null;

        agent.nextPosition = this.transform.position;
        agent.SetDestination(this.transform.position);

        foreach (GameObject enemy in EnemiesOnScene)
        {
            if(enemy.GetComponent<EnemyHealer>() != null) { continue; }
            var distance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetToFollow = enemy.transform;
            }
        }

        if(targetToFollow == null)
        {
            target = FindObjectOfType<PlayerManager>().gameObject.transform;
        } else
        {
            target = targetToFollow;
        }
    }
}
