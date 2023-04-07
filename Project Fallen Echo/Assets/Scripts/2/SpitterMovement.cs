using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterMovement : MonoBehaviour
{
    int currentState = 2;
    [SerializeField] float movementSpeed;
    float distanceToPlayer;

    Transform player;
    Vector2 playerPos;
    Animator anim;
    SpitterShooting shootingScript;

    [Header("State Bounds")]
    [SerializeField] float tooCloseUpper;
    [SerializeField] float goldilocksLower;
    [SerializeField] float goldilocksUpper;
    [SerializeField] float tooFarLower;
    [SerializeField] float outOfRangeLower;



    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        shootingScript = GetComponent<SpitterShooting>();
    }

    private void Update()
    {
        playerPos = new Vector2(player.position.x, transform.position.y);
        distanceToPlayer = Mathf.Abs(playerPos.x - transform.position.x);

        if (currentState == 0)
            Idle();
        else if (currentState == 1)
            TooFar();
        else if (currentState == 2)
            Goldilocks();
        else if (currentState == 3)
            TooClose();
    }

    private void Idle()
    {
        if (distanceToPlayer < outOfRangeLower)
            Transition(1);
    }

    private void TooFar()
    {
        //Transition Logic
        if (distanceToPlayer < tooFarLower)
            Transition(2);

        //Movement
        transform.position = Vector2.MoveTowards(transform.position, playerPos, movementSpeed * Time.deltaTime);
        anim.SetBool("isWalking", true);

        //Facing
        if (transform.position.x - playerPos.x > 1)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        else if (transform.position.x - playerPos.x < 1)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void Goldilocks()
    {
        anim.SetBool("isWalking", false);

        //Transition Logic
        if (distanceToPlayer > goldilocksUpper)
            Transition(1);
        else if (distanceToPlayer < goldilocksLower)
            Transition(3);

        if (transform.position.x - playerPos.x > 1)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        else if (transform.position.x - playerPos.x < 1)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void TooClose()
    {
        //Transition Logic
        if (distanceToPlayer > tooCloseUpper)
            Transition(2);

        //Movement
        transform.position = Vector2.MoveTowards(transform.position, playerPos, -movementSpeed * Time.deltaTime);
        anim.SetBool("isWalking", true);

        //Facing
        if (transform.position.x - playerPos.x > 1)
        {
            transform.localScale = new Vector2(1, 1);
        }

        else if (transform.position.x - playerPos.x < 1)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void Transition(int stateToTransitionTo)
    {
        if (stateToTransitionTo == 2)
            shootingScript.enabled = true;
        else
            shootingScript.enabled = false;

        currentState = stateToTransitionTo;
    }
}
