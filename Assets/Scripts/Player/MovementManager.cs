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

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        effect = GetComponent<EchoEffect>();
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

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
        yield return new WaitForSeconds(AttributeManager.Instance.dashRecover);
        canDash = true;
    }

    void Move()
    {
        playerRb.velocity = new Vector2(xInput, yInput).normalized * speed;
    }
}
