using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterShooting : MonoBehaviour
{
    [SerializeField] float reloadSpeed;
    [SerializeField] float shotSpeed;
    [SerializeField] float damage;

    bool isReloading = false;
    
    Transform target;
    Vector2 targetPos;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawner;
    GameObject bullet;
    Animator anim;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!isReloading)
        {
            anim.SetTrigger("Shoot");
            isReloading = true;
            Invoke("Fire", 0.5f);
        }
    }

    private void Fire()
    {
        targetPos = (target.position - transform.position).normalized;
        bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);
        //bullet.GetComponent<EnemyBullet>().BulletMove(targetPos, shotSpeed);

        AkSoundEngine.PostEvent("spitter_projectile_shot", gameObject);

        Invoke("Reload", reloadSpeed);
    }

    private void Reload()
    {
        isReloading = false;
    }
}
