using System.Collections;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public bool canMove = true;
    public bool canDash = true;
    public float speed;
    public float dashForce;
    public float dashDuration;
    public float dashRecover;
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

        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }

        if (!canMove) return;
        Move();
    }

    public void StartDash()
    {
        canMove = false;
        canDash = false;
        effect.ActivateEffect();
        this.playerRb.AddForce(new Vector2(xInput, yInput).normalized * dashForce, ForceMode2D.Impulse);
        StartCoroutine(StopDash());
    }

    public void StopPlayer()
    {
        this.playerRb.velocity = Vector2.zero;
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);
        StopPlayer();
        canMove = true;
        effect.DeactivateEffect();
        StartCoroutine(RestoreDash());
    }

    private IEnumerator RestoreDash()
    {
        yield return new WaitForSeconds(dashRecover);
        canDash = true;
    }

    void Move()
    {
        playerRb.velocity = new Vector2(xInput, yInput).normalized * speed;
    }
}
