using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float stopDistance = 0f;
    protected Transform target;
    protected NavMeshAgent agent;
    protected bool canAct = false;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (ShouldChase(distanceToTarget))
            {
                agent.SetDestination(target.position);
                canAct = false;
            }
            else
            {
                agent.SetDestination(transform.position);
                canAct = true;
            }
        }
    }

    protected virtual bool ShouldChase(float distanceToTarget)
    {
        return distanceToTarget > stopDistance;
    }
}
