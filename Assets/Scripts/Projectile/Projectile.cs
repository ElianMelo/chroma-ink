using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 10;
    private Rigidbody2D rb;
    private Vector2 dir;
    private PlayerManager player;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        dir = MouseDirection().normalized;
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
        Invoke("DestroyProjectile", 2);
    }

    public Vector2 MouseDirection()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
