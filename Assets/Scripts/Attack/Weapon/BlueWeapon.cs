using System.Collections;
using UnityEngine;

public class BlueWeapon : Weapon
{
    public GameObject hitCollider;
    public GameObject blueVisual;
    public float disableDelay = 0.2f;
    private bool canAttack = true;
    private Pencil pencil;

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

    public IEnumerator AllowAttack()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.blueAttackDelay);
        canAttack = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
    }

    public override void PermformAttack(Pencil pencil)
    {
        this.pencil = pencil;
        if (canAttack)
        {
            StartCoroutine(PerformAttackCoroutine());
            pencil.PerformBlueAttack();
        }
    }

    public IEnumerator PerformAttackCoroutine()
    {
        SwordTurn();
        DisableAttack();
        EnableCollision();
        Instantiate(blueVisual, pencil.transform.position, transform.rotation);
        StartCoroutine(DisableColission());
        StartCoroutine(AllowAttack());
        yield return null;
    }
}
