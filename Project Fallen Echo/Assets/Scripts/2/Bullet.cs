using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damageStat;

    public void StatPass(int damage)
    {
        damageStat = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }

        //Checks if hits player
        if (collision.gameObject.layer == 8)
        {
            return;
        }

        //Checks if hits enemy
        else if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponent<EnemyDamage>() != null)
                collision.gameObject.GetComponent<EnemyDamage>().DamageEnemy(damageStat);
        }

        else if (collision.gameObject.layer == 11)
        {
            return;
        }

        Destroy(gameObject);
    }
}
