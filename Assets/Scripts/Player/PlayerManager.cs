using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ReceiveEffect
{
    private MovementManager movementManager;

    private void Awake()
    {
        movementManager = GetComponent<MovementManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            AttributeManager.Instance.health -= AttributeManager.Instance.enemiesDamage;
            HealthUI.Instance.UpdateHealth();
        }
    }

    public override void ReceiveBlueEffect(Vector3 direction)
    {
        movementManager.ReceiveBlueForce(direction);
    }

    public override void ReceiveGreenEffect()
    {
        var heal = AttributeManager.Instance.maxHealth * (AttributeManager.Instance.greenEffectPercentage / 100);
        if (heal > AttributeManager.Instance.maxHealth)
        {
            heal = AttributeManager.Instance.maxHealth;
        }
        AttributeManager.Instance.health += heal;
        HealthUI.Instance.UpdateHealth();
    }

    public override void ReceiveOrangeEffect()
    {
        return;
    }

    public override void ReceivePurpleEffect(Vector3 direction)
    {
        movementManager.ReceivePurpleForce(direction);
    }

    public override void ReceiveRedEffect()
    {
        return;
    }

    public override void ReceiveYellowEffect()
    {
        movementManager.IncreaseMoveSpeed();
    }
}
