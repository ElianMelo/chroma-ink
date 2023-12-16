using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFlower : Flower
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TagByAction(collision, "RedAttack", "RedSkill", EffectType.Orange);
        TagByAction(collision, "BlueAttack", "BlueSkill", EffectType.Green);
        TagByAction(collision, "YellowAttack", "YellowSkill", EffectType.Yellow);
    }
}
