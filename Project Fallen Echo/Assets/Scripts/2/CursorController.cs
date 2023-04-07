using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    Vector3 targetPos;

    private void Update()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        transform.position = new Vector3(targetPos.x, targetPos.y, 0);
    }
}
