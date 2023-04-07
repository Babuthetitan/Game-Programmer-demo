using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] int health;
    [SerializeField] float enemyDeathTimer = 0.5f;
    [SerializeField] MonoBehaviour movementScript;

    Animator anim;
    GameManager gameManager;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DamageEnemy(int damageToDeal)
    {
        health -= damageToDeal;
        anim.SetTrigger("isHit");

        AkSoundEngine.PostEvent("spitter_receive_damage", gameObject); 

        if (health <= 0)
        {
            anim.SetTrigger("isDead");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;

            movementScript.enabled = false;

            AkSoundEngine.PostEvent("ghoul_death", gameObject);

            Invoke("KillEnemy", enemyDeathTimer);
        }
    }

    void KillEnemy()
    {
        gameManager.EnemyDead();
        Destroy(gameObject);
    }
}
