using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float aggroRange;

    Transform player;
    Vector2 playerPos;
    Animator anim;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        playerPos = new Vector2(player.position.x, transform.position.y);

        if (Mathf.Abs(transform.position.x - playerPos.x) < aggroRange)
        {
            EnemyWake();
        }
    }

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            PlayerFollow();
        }
    }

    private void EnemyWake()
    {
        anim.SetTrigger("playerNear");
    }

    private void PlayerFollow()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, movementSpeed * Time.deltaTime);
        
        if (transform.position.x - playerPos.x > 1)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        else if (transform.position.x - playerPos.x < 1)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
