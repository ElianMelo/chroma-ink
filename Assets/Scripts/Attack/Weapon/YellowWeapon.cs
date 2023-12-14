using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowWeapon : Weapon
{
    public GameObject prefabProjectile;

    private bool canAttack = true;
    private Pencil pencil;

    public void DisableAttack()
    {
        canAttack = false;
    }

    public IEnumerator AllowAttack()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.yellowAttackDelay);
        canAttack = true;
    }

    public override void PermformAttack(Pencil pencil)
    {
        this.pencil = pencil;
        if (canAttack)
        {
            StartCoroutine(PerformAttackCoroutine());
            pencil.PerformYellowAttack();
        }
    }

    public IEnumerator PerformAttackCoroutine()
    {
        var pencilQuaternion = pencil.transform.rotation * Quaternion.Euler(new Vector3(0, 0, 90));

        DisableAttack();
        Instantiate(prefabProjectile,
                 pencil.transform.position, pencilQuaternion);
        StartCoroutine(AllowAttack());
        yield return null;
    }
}
