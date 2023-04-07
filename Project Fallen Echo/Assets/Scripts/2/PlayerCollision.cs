using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    bool facingRight;

    GameManager gameManager;
    HealthBar healthBar;
    Animator anim;
    Rigidbody2D rb;
    public float knockbackForce = 0.5f;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBar = GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<HealthBar>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            float enemyDamage = collision.gameObject.GetComponent<EnemyStats>().damage;

            if (collision.gameObject.transform.position.x - transform.position.x >= 0)
            {
                facingRight = true;
            }

            else if (collision.gameObject.transform.position.x - transform.position.x <= 0)
            {
                facingRight = false;
            }

            TakeDamage(enemyDamage, facingRight);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            print("Next Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void TakeDamage(float damage, bool xDir)
    {
        gameManager.playerHealth--;

        if (healthBar != null)
            healthBar.SetHealth((int)gameManager.playerHealth);

        anim.SetTrigger("isHit");
        
        if (facingRight)
        {
            GetComponent<PlayerMovement2>().enabled = false;
            
            if (GetComponent<PlayerMovement2>().isGrounded == true)
            {
                rb.AddForce(new Vector2(-knockbackForce, knockbackForce) * 10, ForceMode2D.Impulse);
            }

            else
            {
                rb.AddForce(new Vector2(-knockbackForce, knockbackForce * 2) * 10, ForceMode2D.Impulse);
            }

            AkSoundEngine.PostEvent("player_damage", gameObject);

            Invoke("RegainControl", 0.5f);
        }

        else if (!facingRight)
        {
            GetComponent<PlayerMovement2>().enabled = false;

            if (GetComponent<PlayerMovement2>().isGrounded == true)
            {
                rb.AddForce(new Vector2(knockbackForce, knockbackForce) * 10, ForceMode2D.Impulse);
            }

            else
            {
                rb.AddForce(new Vector2(knockbackForce, knockbackForce * 2) * 10, ForceMode2D.Impulse);
            }

            AkSoundEngine.PostEvent("player_damage", gameObject);

            Invoke("RegainControl", 0.5f);
        }
    }

    private void RegainControl()
    {
        GetComponent<PlayerMovement2>().enabled = true;
    }
}