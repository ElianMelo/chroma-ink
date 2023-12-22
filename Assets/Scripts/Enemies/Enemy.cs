using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : ReceiveEffect
{
    public delegate void OnDeath();
    public static event OnDeath onDeathEvent;

    public float stopDistance = 0f;
    public float life = 200f;
    public float speed = 5f;
    public ReactionData reactionData;
    protected EnemyHealthUI hpBar;
    protected Transform target;
    protected NavMeshAgent agent;
    protected bool canAct = false;
    protected Rigidbody2D enemyRb;
    protected EchoEffect effect;
    protected bool receiveForce = false;
    protected float maxLife;
    protected bool isDead = false;
    protected Vector3 velocity = Vector3.zero;
    protected float smoothTime = 0.3F;
    protected bool canTakeDamage = true;

    protected virtual void Start()
    {
        maxLife = life;
        hpBar = GetComponentInChildren<EnemyHealthUI>();
        enemyRb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        effect = GetComponent<EchoEffect>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
        target = FindObjectOfType<PlayerManager>().gameObject.transform;
        this.transform.rotation = Quaternion.identity;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (AttributeManager.Instance.paused) return;
        if (!canTakeDamage) return;
        if (isDead) return;
        var valDamage = 0f;
        var canShowPopup = false;
        TextColors color = TextColors.WHITE;
        bool increment = false;

        if (collision.CompareTag("RedAttack"))
        {
            life -= AttributeManager.Instance.redAttackDamage;
            valDamage = AttributeManager.Instance.redAttackDamage;
            canShowPopup = true;
            color = TextColors.RED;

            var particle = Instantiate(reactionData.redParticle, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            particle.Play();
            Destroy(particle.gameObject, 1f);
        }

        if (collision.CompareTag("RedSkill"))
        {
            life -= AttributeManager.Instance.redSkillDamage;
            valDamage = AttributeManager.Instance.redSkillDamage;
            canShowPopup = true;
            color = TextColors.RED;
            increment = true;

            var particle = Instantiate(reactionData.redParticle, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            particle.Play();
            Destroy(particle.gameObject, 1f);
        }

        if (collision.CompareTag("BlueAttack"))
        {
            life -= AttributeManager.Instance.blueAttackDamage;
            valDamage = AttributeManager.Instance.blueAttackDamage;
            canShowPopup = true;
            color = TextColors.BLUE;

            var particle = Instantiate(reactionData.blueParticle, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            particle.Play();
            Destroy(particle.gameObject, 1f);
        }

        if (collision.CompareTag("BlueSkill"))
        {
            life -= AttributeManager.Instance.blueSkillDamage;
            valDamage = AttributeManager.Instance.blueSkillDamage;
            ReceiveBlueEffect(collision.transform.position - this.transform.position);
            canShowPopup = true;
            color = TextColors.BLUE;
            increment = true;

            var particle = Instantiate(reactionData.blueParticle, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            particle.Play();
            Destroy(particle.gameObject, 1f);
        }

        if (collision.CompareTag("YellowAttack"))
        {
            life -= AttributeManager.Instance.yellowAttackDamage;
            valDamage = AttributeManager.Instance.yellowAttackDamage;
            canShowPopup = true;
            color = TextColors.YELLOW;

            var particle = Instantiate(reactionData.yellowParticle, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            Destroy(particle.gameObject, 1f);
            particle.Play();
        }

        if(collision.CompareTag("HealCollider"))
        {
            life += maxLife / 2;
            if(life > maxLife)
            {
                life = maxLife;
            }
            valDamage = maxLife / 2;
            canShowPopup = true;
            color = TextColors.GREEN;
        }

        if(canShowPopup)
        {
            hpBar.UpdateHealth(life, maxLife);
            var text = Instantiate(reactionData.floatingText, this.transform.position, Quaternion.identity).GetComponent<FloatingText>();
            text.transform.SetParent(this.transform);
            text.SetColor(color);
            if(increment){ text.IncrementSize(); }
            text.ChangeText(valDamage.ToString());
            StartCoroutine(TakeHit(collision.transform.position));
        }

        if (life <= 0) { Death(); }
    }

    private IEnumerator TakeHit(Vector3 otherPosition)
    {
        canTakeDamage = false;
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.a = 0.4f;
        sprite.color = color;
        this.enemyRb.AddForce((this.transform.position - otherPosition).normalized * 
            3f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
        color.a = 1f;
        sprite.color = color;
        yield return null;
    }

    private void Death()
    {
        Destroy(hpBar.gameObject);
        isDead = true;
        target = null;
        speed = 0;
        onDeathEvent?.Invoke();
        Destroy(this.gameObject, 1f);
    }

    protected virtual void FixedUpdate()
    {
        if (AttributeManager.Instance.paused)
        {
            agent.speed = 0;
            canAct = false;
            return;
        }
        agent.speed = speed;
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (ShouldChase(distanceToTarget))
            {
                agent.SetDestination(target.position);
                canAct = false;
            }
            else
            {
                agent.SetDestination(transform.position);
                canAct = true;
            }

            if (!receiveForce)
            {
                transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, smoothTime);
            }
        }
    }

    protected virtual bool ShouldChase(float distanceToTarget)
    {
        return distanceToTarget > stopDistance;
    }

    public override void ReceiveBlueEffect(Vector3 direction)
    {
        ReceiveBlueForce(direction);
        return;
    }

    public override void ReceiveGreenEffect()
    {
        return;
    }

    public override void ReceiveOrangeEffect()
    {
        this.speed /= AttributeManager.Instance.orangeEffectPercentage;
        StartCoroutine(StopOrangeEffect());
        return;
    }

    public override void ReceivePurpleEffect(Vector3 direction)
    {
        ReceivePurpleForce(direction);
        return;
    }

    public override void ReceiveRedEffect()
    {
        life -= AttributeManager.Instance.redEffectDamage;

        hpBar.UpdateHealth(life, maxLife);
        var text = Instantiate(reactionData.floatingText, this.transform.position, Quaternion.identity).GetComponent<FloatingText>();
        text.transform.SetParent(this.transform);
        text.SetColor(TextColors.RED);
        text.IncrementSize();
        text.ChangeText(AttributeManager.Instance.redEffectDamage.ToString());

        if (life <= 0) { Death(); }
        return;
    }

    public override void ReceiveYellowEffect()
    {
        return;
    }

    public IEnumerator StopOrangeEffect()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.orangeEffectDuration);
        this.speed *= AttributeManager.Instance.orangeEffectPercentage;
    }

    public void ReceiveBlueForce(Vector3 direction)
    {
        receiveForce = true;

        agent.nextPosition = this.transform.position;
        agent.SetDestination(this.transform.position);

        effect.ActivateEffect();
        this.enemyRb.AddForce(direction.normalized * AttributeManager.Instance.blueEffectForce, ForceMode2D.Impulse);
        StartCoroutine(StopBlueForce());
    }

    private IEnumerator StopBlueForce()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.blueEffectDuration);
        yield return new WaitForSeconds(1f);

        agent.nextPosition = this.transform.position;
        agent.SetDestination(this.transform.position);

        effect.DeactivateEffect();
        receiveForce = false;
    }

    public void ReceivePurpleForce(Vector3 direction)
    {
        receiveForce = true;

        agent.nextPosition = this.transform.position;
        agent.SetDestination(this.transform.position);

        effect.ActivateEffect();
        this.enemyRb.AddForce(direction.normalized * AttributeManager.Instance.purpleEffectForce, ForceMode2D.Impulse);
        StartCoroutine(StopPurpleForce());
    }

    private IEnumerator StopPurpleForce()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.purpleEffectDuration);
        yield return new WaitForSeconds(1f);

        agent.nextPosition = this.transform.position;
        agent.SetDestination(this.transform.position);

        effect.DeactivateEffect();
        receiveForce = false;
    }
}
