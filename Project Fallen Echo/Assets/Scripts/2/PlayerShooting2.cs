using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    Transform cursor;
    Transform gun;
    Transform gunPivot;
    PlayerMovement2 playerMov;

    public float reloadSpeed;
    public float bulletSpeed;
    public int damage;

    Vector2 directionToShoot;
    Vector3 mousePos;
    float rotationAngle;

    bool canShoot = true;

    private void Start()
    {
        cursor = GameObject.Find("Cursor").GetComponent<Transform>();
        gun = GameObject.Find("Gun").GetComponent<Transform>();
        gunPivot = GameObject.Find("GunPivot").GetComponent<Transform>();
        playerMov = GetComponent<PlayerMovement2>();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        directionToShoot = mousePos - transform.position;
        directionToShoot.Normalize();

        float angle = Mathf.Atan2(directionToShoot.y, directionToShoot.x) * Mathf.Rad2Deg;

        if (playerMov.facingRight && !PauseMenu.GameIsPaused)
        {
            gunPivot.eulerAngles = new Vector3(0, 0, angle);
        }
        else if (!playerMov.facingRight && !PauseMenu.GameIsPaused)
        {
            gunPivot.eulerAngles = new Vector3(0, 0, angle - 180f);
        }
    }

    void OnFire()
    {
        if (canShoot && bulletPrefab != null && !PauseMenu.GameIsPaused)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, gun.position + (Vector3)(directionToShoot * 0.1f), Quaternion.LookRotation(transform.position - mousePos, Vector3.forward));
            bulletInstance.transform.Rotate(new Vector3(0, 0, 90));
            bulletInstance.GetComponent<Rigidbody2D>().velocity = directionToShoot * bulletSpeed;
            bulletInstance.GetComponent<Bullet>().StatPass(damage);

            AkSoundEngine.PostEvent("player_shoot", gameObject);

            canShoot = false;
            Invoke("Reload", reloadSpeed);
        }
    }

    void Reload()
    {
        canShoot = true;
    }
}
