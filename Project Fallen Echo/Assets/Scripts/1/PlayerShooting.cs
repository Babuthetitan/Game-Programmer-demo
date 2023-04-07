using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float reloadSpeed;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;

    Vector2 directionToShoot;

    bool canShoot = true;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        directionToShoot = mousePos - transform.position;
        directionToShoot.Normalize();

        if (Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, transform.position + (Vector3)(directionToShoot * 0.5f), Quaternion.LookRotation(transform.position - mousePos, Vector3.forward));
            bulletInstance.GetComponent<Rigidbody2D>().velocity = directionToShoot * bulletSpeed;
            bulletInstance.GetComponent<PlayerBullet>().StatPassing(damage);

            canShoot = false;
            Invoke("Reload", reloadSpeed);
        }
    }

    void Reload()
    {
        canShoot = true;
    }
}
