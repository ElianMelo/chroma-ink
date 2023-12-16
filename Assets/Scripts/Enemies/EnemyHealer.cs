using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealer : Enemy
{
    private GameObject healCollider;

    protected override void Start()
    {
        healCollider = transform.Find("HealCollider")?.gameObject;
        base.Start();
        target = null;

        InvokeRepeating("FindEnemy", 0f, 1f);
    }

    protected override void Update()
    {
        base.Update();

        if (canAct)
        {
            StartCoroutine(HealDelay(2));
            target = null;
            canAct = false;
        }
    }

    IEnumerator HealDelay(int DelayTime)
    {
        healCollider.SetActive(true);
        yield return new WaitForSeconds(DelayTime);
        healCollider.SetActive(false);
        FindEnemy();
    }

    private void FindEnemy()
    {
        GameObject[] EnemiesOnScene = GameObject.FindGameObjectsWithTag("Enemy");
        var minDistance = Mathf.Infinity;
        Transform targetToFollow = null;

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

        target = targetToFollow;
    }
}
