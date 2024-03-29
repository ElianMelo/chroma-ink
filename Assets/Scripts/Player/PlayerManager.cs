using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ReceiveEffect
{
    public GameObject floatingText;
    public SpriteRenderer pencil;
    public SpriteRenderer crosshair;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject playerHitEffect;

    private MovementManager movementManager;

    private void Awake()
    {
        movementManager = GetComponent<MovementManager>();
    }

    private void OnEnable()
    {
        AttributeManager.onPauseEvent += OnPauseEvent;
    }

    private void OnDisable()
    {
        AttributeManager.onPauseEvent -= OnPauseEvent;
    }

    private void OnPauseEvent()
    {
        if (AttributeManager.Instance.paused)
        {
            Color color = pencil.color;
            color.a = 0f;
            pencil.color = color;
            crosshair.color = color;
        }
        else
        {
            Color color = pencil.color;
            color.a = 1f;
            pencil.color = color;
            if (InputSystem.Instance.IsKeyboard())
            {
                crosshair.color = color;
            }
        }
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

            Camera.main.GetComponent<CameraController>().Shake(.15f, 0.2f);
            text.transform.SetParent(this.transform);
            text.SetColor(TextColors.WHITE);
            text.ChangeText(AttributeManager.Instance.enemiesDamage.ToString());

            var instHitEffect = Instantiate(playerHitEffect, this.transform.position, Quaternion.identity);
            Destroy(instHitEffect.gameObject, 1f);

            StartCoroutine(TakeHit(collision.transform.position));
        }
    }

    private IEnumerator TakeHit(Vector3 otherPosition)
    {
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
