using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float damage;

    public void StatPassing(float dmg)
    {
        //Takes the damage attribute from the player and passes it to the bullet
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            //collision.gameObject.GetComponent<>().TakeDamage(damage);
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            print("Player");
            return;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
