using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    private float speed = 10;
    private Rigidbody2D rb;
    private Vector2 dir;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        dir = MouseDirection().normalized;
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
        Invoke("DestroyProjectile", 2);
    }

    public Vector2 MouseDirection()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;        
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
