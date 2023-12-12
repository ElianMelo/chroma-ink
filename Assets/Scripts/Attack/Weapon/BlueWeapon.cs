using System.Collections;
using UnityEngine;

public class BlueWeapon : Weapon
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

    public bool CanAttack()
    {
        return canAttack;
    }

    public void AllowAttack()
    {
        canAttack = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
    }

    public override void PermformAttack(Pencil pencil)
    {
        StartCoroutine(PerformAttackCoroutine());
        pencil.PerformBlueAttack();
    }

    public IEnumerator PerformAttackCoroutine()
    {
        SwordTurn();
        DisableAttack();
        EnableCollision();
        yield return DisableColission();
        AllowAttack();
        yield return null;
    }
}
