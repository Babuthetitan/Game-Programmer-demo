using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] string soundName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger");
        if (collision.CompareTag("Player"))
        {
            //Put Music Code Here
            print("PlayerTagged");
        }
    }
}
