using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpFacing : MonoBehaviour
{
    Transform reference;
    private void Start()
    {
        reference = GameObject.Find("Level Up Reference").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = reference.transform.position;
    }
}
