using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWeapon : Weapon
{
    public GameObject hitCollider;
    public float disableDelay = 0.2f;
    private bool canAttack = true;

    public void SwordTurn()
    {
        Vector2 result = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        this.transform.rotation = (Quaternion.Euler(0f, 0f, Mathf.Atan2(result.y, result.x) * Mathf.Rad2Deg));
        this.transform.Rotate(0, 0, -90);
    }
    public void EnableCollision()
    {
        hitCollider.SetActive(true);
    }

    public IEnumerator DisableColission()
    {
        yield return new WaitForSeconds(disableDelay);
        hitCollider.SetActive(false);
    }

    public IEnumerator AllowAttack()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.redAttackDelay);
        canAttack = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
    }

    public override void PermformAttack(Pencil pencil)
    {
        if(canAttack)
        {
            StartCoroutine(PerformAttackCoroutine());
            pencil.PerformRedAttack();
        }
    }

    public IEnumerator PerformAttackCoroutine()
    {
        SwordTurn();
        DisableAttack();
        EnableCollision();
        yield return DisableColission();
        yield return AllowAttack();
        yield return null;
    }
}
