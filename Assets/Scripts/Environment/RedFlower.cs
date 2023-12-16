using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlower : Flower
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TagByAction(collision, "RedAttack", "RedSkill", EffectType.Red);
        TagByAction(collision, "BlueAttack", "BlueSkill", EffectType.Purple);
        TagByAction(collision, "YellowAttack", "YellowSkill", EffectType.Orange);
    }
}
