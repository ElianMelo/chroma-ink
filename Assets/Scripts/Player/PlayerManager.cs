using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ReceiveEffect
{
    public GameObject floatingText;

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
            if (AttributeManager.Instance.health <= 0)
            {
                LevelManager.Instance.LoseGame();
            }
            HealthUI.Instance.UpdateHealth();

            var text = Instantiate(floatingText, this.transform.position, Quaternion.identity).GetComponent<FloatingText>();
            text.transform.SetParent(this.transform);
            text.SetColor(TextColors.WHITE);
            text.ChangeText(AttributeManager.Instance.enemiesDamage.ToString());

            StartCoroutine(TakeHit(collision.transform.position));
        }
    }

    private IEnumerator TakeHit(Vector3 otherPosition)
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.a = 0.4f;
        sprite.color = color;
        this.GetComponent<Rigidbody2D>().AddForce((this.transform.position - otherPosition).normalized *
            3f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        sprite.color = color;
        yield return null;
    }

    public override void ReceiveBlueEffect(Vector3 direction)
    {
        movementManager.ReceiveBlueForce(direction);
    }

    public override void ReceiveGreenEffect()
    {
        var heal = AttributeManager.Instance.maxHealth * (AttributeManager.Instance.greenEffectPercentage / 100);
        if (AttributeManager.Instance.health + heal > AttributeManager.Instance.maxHealth)
        {
            AttributeManager.Instance.health = AttributeManager.Instance.maxHealth;
        } else
        {
            AttributeManager.Instance.health += heal;
        }

        var text = Instantiate(floatingText, this.transform.position, Quaternion.identity).GetComponent<FloatingText>();
        text.transform.SetParent(this.transform);
        text.SetColor(TextColors.GREEN);
        text.ChangeText(heal.ToString());

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
