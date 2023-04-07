using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Transform target;
    Vector2 targetPos;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        targetPos = (target.position - transform.position).normalized;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        BulletMove(targetPos, 3);
    }

    public void BulletMove(Vector2 targetDir, float speed)
    {
        rb.velocity = targetDir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("isDestroyed");
        rb.velocity *= 0;
        Invoke("Destroy", 0.5f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}

