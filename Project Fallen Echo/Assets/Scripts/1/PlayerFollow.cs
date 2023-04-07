using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    Transform playerPos;


    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = new Vector3(playerPos.position.x, transform.position.y, transform.position.z);
    }
}
