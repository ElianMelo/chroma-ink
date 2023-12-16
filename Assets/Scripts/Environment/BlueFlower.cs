using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFlower : Flower
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TagByAction(collision, "RedAttack", "RedSkill", EffectType.Purple);
        TagByAction(collision, "BlueAttack", "BlueSkill", EffectType.Blue);
        TagByAction(collision, "YellowAttack", "YellowSkill", EffectType.Green);
    }
}
