using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool LevelUpBoost = false;

    public float playerHealth;
    float maxPlayerHealth;
    float enemyDeathCount = 0;
    [SerializeField] float levelBoostTime;

    GameObject gun;
    GameObject player;
    HealthBar healthBar;
    Animator anim;
    GameObject healExplosion;
    GameObject levelExplosion;

    private void Start()
    {
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        healExplosion = GameObject.Find("Heal Explosion");
        levelExplosion = GameObject.Find("Level Up");

        maxPlayerHealth = playerHealth;


    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        anim.SetBool("isDead", true);
        gun = GameObject.FindGameObjectWithTag("Gun");
        player.GetComponent<PlayerMovement2>().groundDistance = 0;
        player.GetComponent<PlayerMovement2>().isGrounded = false;
        player.GetComponent<PlayerMovement2>().enabled = false;
        player.GetComponent<PlayerCollision>().knockbackForce = 0;
        player.GetComponent<PlayerCollision>().enabled = false;
        player.GetComponent<PlayerShooting2>().bulletPrefab = null;
        player.GetComponent<Rigidbody2D>().drag = 5;

        if (gun != null) gun.SetActive(false);  

    }

    public void EnemyDead()
    {
        if (enemyDeathCount == 9)
        {
            LevelPlayer();
            return;
        }

        enemyDeathCount++;

        if (enemyDeathCount == 5)
        {
            HealPlayer();
            return;
        }
    }

    private void HealPlayer()
    {
        healExplosion.GetComponent<Animator>().SetTrigger("healTrigger");
        if (playerHealth >= maxPlayerHealth)
            return;

        AkSoundEngine.PostEvent("player_health", gameObject);

        playerHealth++;

        if (healthBar != null)
            healthBar.SetHealth((int)playerHealth);
    }

    private void LevelPlayer()
    {
        levelExplosion.GetComponent<Animator>().SetTrigger("levelTrigger");
        enemyDeathCount = 0;
        /*player.GetComponent<PlayerMovement2>().walkSpeed += 50;
        player.GetComponent<PlayerMovement2>().runSpeed += 50;
        player.GetComponent<PlayerMovement2>().movementSpeed += 50;

        player.GetComponent<PlayerShooting2>().bulletSpeed += 20;
        player.GetComponent<PlayerShooting2>().damage += 2;*/
        player.GetComponent<PlayerShooting2>().reloadSpeed -= 0.1f;
        LevelUpBoost = true;

        AkSoundEngine.PostEvent("player_levelup", gameObject);

        Invoke(nameof(ResetLevelBoost), levelBoostTime);
    }

    private void ResetLevelBoost()
    {
        /*print("poow");
        player.GetComponent<PlayerMovement2>().walkSpeed -= 50;
        player.GetComponent<PlayerMovement2>().runSpeed -= 50;
        player.GetComponent<PlayerMovement2>().movementSpeed -= 50;

        player.GetComponent<PlayerShooting2>().bulletSpeed -= 20;
        player.GetComponent<PlayerShooting2>().damage -= 2;*/
        player.GetComponent<PlayerShooting2>().reloadSpeed += 0.1f;

        AkSoundEngine.PostEvent("player_levelupcooldown", gameObject);

        LevelUpBoost = false;
    }
}
