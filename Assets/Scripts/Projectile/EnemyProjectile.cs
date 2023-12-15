using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float speed = 10;
    private Rigidbody2D rb;
    private Transform target;
    private Vector2 dir;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        dir = (target.position - transform.position).normalized;
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
        Invoke("DestroyProjectile", 2);
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
