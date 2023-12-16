using System.Collections;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public bool canMove = true;
    public bool canDash = true;
    public float speed;
    public float dashForce;
    public float dashDuration;

    private bool receiveForce = false;
    private float xInput;
    private float yInput;
    private Rigidbody2D playerRb;
    private EchoEffect effect;
    private Animator animator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        effect = GetComponent<EchoEffect>();
        animator = this.GetComponent<Animator>();
        animator.SetInteger("estado", 3);
    }

    void Update()
    {
        if (AttributeManager.Instance.paused) { return; };

        int estado = animator.GetInteger("estado");
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        xInput = h;
        yInput = v;

        if (h != 0)
        {
            animator.speed = h < 0 ? h * -1 : h;
        }
        else
        {
            animator.speed = v < 0 ? v * -1 : v;
        }

        if (h > 0)
        {
            if (estado != 4)
            {
                animator.SetInteger("estado", 4);
            }
        }
        else if (h < 0)
        {
            if (estado != 2)
            {
                animator.SetInteger("estado", 2);
            }
        }
        else if (v != 0)
        {
            if (estado == 3)
            {
                animator.SetInteger("estado", 4);
            }
            else if (estado == 1)
            {
                animator.SetInteger("estado", 2);
            }
        }
        else
        {
            if (estado == 4)
            {
                animator.SetInteger("estado", 3);
            }
            else if (estado == 2)
            {
                animator.SetInteger("estado", 1);
            }
        }

        if (receiveForce) return;

        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }

        if (!canMove) return;
        Move();
    }

    public void StopPlayer()
    {
        this.playerRb.velocity = Vector2.zero;
    }

    public void IncreaseMoveSpeed()
    {
        speed *= (100 + AttributeManager.Instance.yellowEffectPercentage) / 100;
        StartCoroutine(ReduceMoveSpeed());
    }

    private IEnumerator ReduceMoveSpeed()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.yellowEffectDuration);
        speed /= (100 + AttributeManager.Instance.yellowEffectPercentage) / 100;
    }

    public void StartDash()
    {
        canMove = false;
        canDash = false;
        effect.ActivateEffect();
        this.playerRb.AddForce(new Vector2(xInput, yInput).normalized * dashForce, ForceMode2D.Impulse);
        StartCoroutine(StopDash());
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);
        StopPlayer();
        canMove = true;
        effect.DeactivateEffect();
        StartCoroutine(RestoreDash());
    }

    public void ReceiveBlueForce(Vector3 direction)
    {
        receiveForce = true;
        effect.ActivateEffect();
        StopPlayer();
        this.playerRb.AddForce(direction.normalized * AttributeManager.Instance.blueEffectForce, ForceMode2D.Impulse);
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
        StopPlayer();
        this.playerRb.AddForce(direction.normalized * AttributeManager.Instance.purpleEffectForce, ForceMode2D.Impulse);
        StartCoroutine(StopPurpleForce());
    }

    private IEnumerator StopPurpleForce()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.purpleEffectDuration);
        StopPlayer();
        receiveForce = false;
        effect.DeactivateEffect();
    }

    private IEnumerator RestoreDash()
    {
        WeaponsCDUI.Instance.dashCd = AttributeManager.Instance.dashRecover;
        yield return new WaitForSeconds(AttributeManager.Instance.dashRecover);
        canDash = true;
    }

    void Move()
    {
        playerRb.velocity = new Vector2(xInput, yInput).normalized * speed;
    }
}
