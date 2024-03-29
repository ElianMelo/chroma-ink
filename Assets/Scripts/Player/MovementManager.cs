using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovementManager : MonoBehaviour
{
    [SerializeField] public bool canMove = true;
    [SerializeField] public bool canDash = true;
    [SerializeField] public float speed;
    [SerializeField] public float dashForce;
    [SerializeField] public float dashDuration;
    [SerializeField] public GameObject moveEffect;
    [SerializeField] public GameObject walkEffect;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float angleUpdateRate;
    [SerializeField] private float moveLimiar;

    private bool receiveForce = false;
    private float xInput;
    private float yInput;
    private Rigidbody2D playerRb;
    private EchoEffect effect;
    private float h;
    private float v;
    private float baseSpeed;
    private float targetAngle = 180f;
    private IEnumerator RotateVisualsCoroutine;

    private bool canWalkEffect = true;
    private float walkEffectDelay = 0.1f;

    void Start()
    {
        baseSpeed = speed;
        playerRb = GetComponent<Rigidbody2D>();
        effect = GetComponent<EchoEffect>();
    }

    private void Update()
    {
        h = InputSystem.Instance.Horizontal();
        v = InputSystem.Instance.Vertical();

        if (canDash && InputSystem.Instance.Space())
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (AttributeManager.Instance.paused) { return; };

        xInput = h;
        yInput = v;

        if (h != 0 || v != 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        if(h < 0)
        {
            if (RotateVisualsCoroutine != null) StopCoroutine(RotateVisualsCoroutine);
            RotateVisualsCoroutine = RotateVisuals(true);
            StartCoroutine(RotateVisualsCoroutine);
        } else if(h > 0)
        {
            if (RotateVisualsCoroutine != null) StopCoroutine(RotateVisualsCoroutine);
            RotateVisualsCoroutine = RotateVisuals(false);
            StartCoroutine(RotateVisualsCoroutine);
        }

        if (receiveForce) return;


        if (!canMove) return;
        Move();

        if (canWalkEffect && playerRb.velocity.magnitude > moveLimiar)
        {
            canWalkEffect = false;
            var effect = Instantiate(walkEffect, this.transform.position, this.transform.rotation);
            Destroy(effect, 1f);
            StartCoroutine(ResetWalkEffect());
        }
    }

    public IEnumerator ResetWalkEffect()
    {
        yield return new WaitForSeconds(walkEffectDelay);
        canWalkEffect = true;
    }

    private IEnumerator RotateVisuals(bool isRight)
    {
        if(isRight)
        {
            while((spriteRenderer.transform.rotation * Quaternion.Euler(0f, angleUpdateRate, 0f)).eulerAngles.y <
                targetAngle)
            {
                spriteRenderer.transform.rotation *= Quaternion.Euler(0f, angleUpdateRate, 0f);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        } else
        {
            while((spriteRenderer.transform.rotation * Quaternion.Euler(0f, -angleUpdateRate, 0f)).eulerAngles.y <
                (targetAngle * 1.5f))
            {
                spriteRenderer.transform.rotation *= Quaternion.Euler(0f, -angleUpdateRate, 0f);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        yield return null;
    }

    public void StopPlayer()
    {
        this.playerRb.velocity = Vector2.zero;
    }

    public void VerifyEffect()
    {
        if (speed != baseSpeed)
        {
            moveEffect.SetActive(true);
        }
        if (speed == baseSpeed)
        {
            moveEffect.SetActive(false);
        }
    }

    public void IncreaseMoveSpeed()
    {
        speed *= (100 + AttributeManager.Instance.yellowEffectPercentage) / 100;
        VerifyEffect();
        StartCoroutine(ReduceMoveSpeed());
    }

    private IEnumerator ReduceMoveSpeed()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.yellowEffectDuration);
        speed /= (100 + AttributeManager.Instance.yellowEffectPercentage) / 100;
        VerifyEffect();
    }

    public void StartDash()
    {
        canMove = false;
        canDash = false;
        effect.ActivateEffect();
        this.playerRb.AddForce(new Vector2(xInput, yInput).normalized * dashForce, ForceMode2D.Impulse);
        //this.playerRb.velocity = new Vector2(xInput, yInput).normalized * dashForce;
        StartCoroutine(StopDash());
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);
        if (receiveForce) yield return null;
        // StopPlayer();
        canMove = true;
        effect.DeactivateEffect();
        StartCoroutine(RestoreDash());
    }

    public void ReceiveBlueForce(Vector3 direction)
    {
        receiveForce = true;
        effect.ActivateEffect();
        // StopPlayer();
        this.playerRb.velocity = direction.normalized * AttributeManager.Instance.blueEffectForce;
        StartCoroutine(StopBlueForce());
    }

    private IEnumerator StopBlueForce()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.blueEffectDuration);
        // StopPlayer();
        receiveForce = false;
        effect.DeactivateEffect();
    }

    public void ReceivePurpleForce(Vector3 direction)
    {
        receiveForce = true;
        effect.ActivateEffect();
        // StopPlayer();
        this.playerRb.velocity = direction.normalized * AttributeManager.Instance.purpleEffectForce;
        StartCoroutine(StopPurpleForce());
    }

    private IEnumerator StopPurpleForce()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.purpleEffectDuration);
        // StopPlayer();
        receiveForce = false;
        effect.DeactivateEffect();
    }

    private IEnumerator RestoreDash()
    {
        if (InputSystem.Instance.IsKeyboard())
        {
            WeaponsCDUI.Instance.dashCd = AttributeManager.Instance.dashRecover;
        }
        yield return new WaitForSeconds(AttributeManager.Instance.dashRecover);
        canDash = true;
    }

    void Move()
    {
        // playerRb.velocity = new Vector2(xInput, yInput).normalized * speed;
        playerRb.AddForce(new Vector2(xInput, yInput).normalized * speed);
    }
}
