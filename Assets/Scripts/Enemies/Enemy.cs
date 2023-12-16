using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : ReceiveEffect
{
    public float stopDistance = 0f;
    public float life = 200f;
    public float speed = 5f;
    public GameObject floatingText;
    protected EnemyHealthUI hpBar;
    protected Transform target;
    protected NavMeshAgent agent;
    protected bool canAct = false;
    protected Rigidbody2D enemyRb;
    protected EchoEffect effect;
    protected bool receiveForce = false;
    protected float maxLife;
    protected bool isDead = false;

    protected virtual void Start()
    {
        maxLife = life;
        hpBar = GetComponentInChildren<EnemyHealthUI>();
        enemyRb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        effect = GetComponent<EchoEffect>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = FindObjectOfType<PlayerManager>().gameObject.transform;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.tag);
        // if (this.CompareTag("HealCollider")) return;

        if (isDead) return;
        var valDamage = 0f;
        var canShowPopup = false;

        if (collision.CompareTag("RedAttack"))
        {
            life -= AttributeManager.Instance.redAttackDamage;
            valDamage = AttributeManager.Instance.redAttackDamage;
            canShowPopup = true;
        }

        if (collision.CompareTag("RedSkill"))
        {
            life -= AttributeManager.Instance.redSkillDamage;
            valDamage = AttributeManager.Instance.redSkillDamage;
            canShowPopup = true;
        }

        if (collision.CompareTag("BlueAttack"))
        {
            life -= AttributeManager.Instance.blueAttackDamage;
            valDamage = AttributeManager.Instance.blueAttackDamage;
            canShowPopup = true;
        }

        if (collision.CompareTag("BlueSkill"))
        {
            life -= AttributeManager.Instance.blueSkillDamage;
            valDamage = AttributeManager.Instance.blueSkillDamage;
            canShowPopup = true;
        }

        if (collision.CompareTag("YellowAttack"))
        {
            life -= AttributeManager.Instance.yellowAttackDamage;
            valDamage = AttributeManager.Instance.yellowAttackDamage;
            canShowPopup = true;
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
        }

        if(canShowPopup)
        {
            hpBar.UpdateHealth(life, maxLife);
            var text = Instantiate(floatingText, this.transform.position, Quaternion.identity).GetComponent<FloatingText>();
            text.transform.SetParent(this.transform);
            text.ChangeText(valDamage.ToString());
        }

        if (life <= 0)
        { 
            Destroy(hpBar.gameObject);
            isDead = true;
            target = null;
            speed = 0;            
            Destroy(this.gameObject, 1f);
        }
    }

    protected virtual void Update()
    {
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
        var text = Instantiate(floatingText, this.transform.position, Quaternion.identity).GetComponent<FloatingText>();
        text.transform.SetParent(this.transform);
        text.ChangeText(AttributeManager.Instance.redEffectDamage.ToString());

        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
        return;
    }

    public override void ReceiveYellowEffect()
    {
        return;
    }

    public IEnumerator StopOrangeEffect()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.orangeEffectDuration);
        speed *= AttributeManager.Instance.orangeEffectPercentage;
    }

    public void ReceiveBlueForce(Vector3 direction)
    {
        receiveForce = true;
        effect.ActivateEffect();
        this.enemyRb.AddForce(direction.normalized * AttributeManager.Instance.blueEffectForce, ForceMode2D.Impulse);
        StartCoroutine(StopBlueForce());
    }

    private IEnumerator StopBlueForce()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.blueEffectDuration);
        StopPlayer();
        receiveForce = false;
        effect.DeactivateEffect();
    }

    public void ReceivePurpleForce(Vector3 direction)
    {
        receiveForce = true;
        effect.ActivateEffect();
        this.enemyRb.AddForce(direction.normalized * AttributeManager.Instance.purpleEffectForce, ForceMode2D.Impulse);
        StartCoroutine(StopPurpleForce());
    }

    private IEnumerator StopPurpleForce()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.purpleEffectDuration);
        StopPlayer();
        receiveForce = false;
        effect.DeactivateEffect();
    }

    public void StopPlayer()
    {
        this.enemyRb.velocity = Vector2.zero;
    }
}
