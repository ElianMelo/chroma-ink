using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : Enemy
{
    public float healDelayTime = 2f;
    public float radius = 100f;
    private bool canPerform = true;
    [SerializeField] private GameObject healCollider;
    [SerializeField] private GameObject lifeParticle;

    protected override void Start()
    {
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

        var particle = Instantiate(lifeParticle, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        particle.Play();
        Destroy(particle.gameObject, 2f);

        yield return new WaitForSeconds(.2f);
        healCollider.SetActive(false);
        FindEnemy();
        yield return new WaitForSeconds(DelayTime);
        canPerform = true;
    }

    private void FindEnemy()
    {
        List<GameObject> EnemiesOnScene = new List<GameObject>();

        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), 
            radius, Vector2.right, radius, collisionLayerMask);

        foreach (var hit in hits)
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.TryGetComponent(out Enemy enemy))
            {
                if(enemy != this)
                {
                    EnemiesOnScene.Add(obj);
                }
            }
        }

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
            RaycastHit2D[] hitsPlayer = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y),
            radius, Vector2.right, radius, collisionLayerMask);

            foreach (var hit in hitsPlayer)
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.TryGetComponent(out PlayerManager playerManager))
                {
                    target = playerManager.gameObject.transform;
                }
            }
        } else
        {
            target = targetToFollow;
        }
    }
}
