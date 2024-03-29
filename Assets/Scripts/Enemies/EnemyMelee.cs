using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Enemy
{
    [SerializeField] private GameObject hitCollider;
    private Animator animator;
    private const string SLASH_ANIM = "slash";

    protected override void Start()
    {
        animator = hitCollider.GetComponentInChildren<Animator>();
        base.Start();
        StartCoroutine(AttackDelay(2));
    }

    IEnumerator AttackDelay(int DelayTime)
    {
        if (!this) yield return null;
        if (canAct && target?.position != null && hitCollider?.transform?.position != null)
        {
            //Delay para bater, quando alcançar o player
            yield return new WaitForSeconds(0.2f);

            //Direciona a colisão do hit para o player
            Vector2 result = target.position - hitCollider.transform.position;
            hitCollider.transform.localPosition = Vector3.zero;
            hitCollider.transform.rotation = (Quaternion.Euler(0f, 0f, Mathf.Atan2(result.y, result.x) * Mathf.Rad2Deg));
            hitCollider.transform.Rotate(0, 0, -90);

            //Ativa a colisão por alguns segundos
            hitCollider.SetActive(true);
            animator.Play(SLASH_ANIM);
            yield return new WaitForSeconds(.2f);
            hitCollider.SetActive(false);

            //Delay para atacar novamente
            yield return new WaitForSeconds(DelayTime);
        }
        else
        {
            yield return new WaitForSeconds(.5f);
        }

        StartCoroutine(AttackDelay(DelayTime));
    }
}
