using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowWeapon : Weapon
{
    public GameObject prefabProjectile;

    public override void PermformAttack(Pencil pencil)
    {
        Instantiate(prefabProjectile, 
            pencil.transform.position, Quaternion.identity);
        pencil.PerformYellowAttack();
    }
}
