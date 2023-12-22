using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSkill : Skill
{
    public GameObject hitCollider;
    public GameObject blueVisual;
    public float disableDelay = 0.2f;
    public float startDelay = 0.1f;
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
        yield return new WaitForSeconds(AttributeManager.Instance.blueSkillDelay);
        canAttack = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
    }

    public override void PermformSkill(Pencil pencil)
    {
        if (canAttack)
        {
            StartCoroutine(PerformAttackCoroutine());
            pencil.PerformBlueSkill();
        }
    }

    public IEnumerator PerformAttackCoroutine()
    {
        DisableAttack();
        yield return new WaitForSeconds(startDelay);
        SwordTurn();
        EnableCollision();
        Instantiate(blueVisual, hitCollider.transform.position, transform.rotation);
        StartCoroutine(DisableColission());
        WeaponsCDUI.Instance.blueSkillCd = AttributeManager.Instance.blueSkillDelay;
        StartCoroutine(AllowAttack());
        yield return null;
    }
}
