using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSkill : Skill
{
    public GameObject hitCollider;
    public GameObject redVisual;
    public float startDelay = 0.1f;
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
        yield return new WaitForSeconds(AttributeManager.Instance.redSkillDelay);
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
            pencil.PerformRedSkill();
        }
    }

    public IEnumerator PerformAttackCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
        SwordTurn();
        DisableAttack();
        EnableCollision();
        var entity = Instantiate(redVisual, this.transform.position, transform.rotation);
        Destroy(entity, 2);
        StartCoroutine(DisableColission());
        WeaponsCDUI.Instance.redSkillCd = AttributeManager.Instance.redSkillDelay;
        StartCoroutine(AllowAttack());
        yield return null;
    }
}
