using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    public Transform[] waypoints;    
    public float moveSpeed = 2f;     
    public bool loop = true;         

    private int currentWaypoint = 0;
    private int direction = 1;      

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[currentWaypoint].position)
        {
            currentWaypoint += direction;

            if (currentWaypoint >= waypoints.Length)
            {
                if (loop)
                {
                    currentWaypoint = 0;
                }
                else
                {
                    direction = -1;
                    currentWaypoint = waypoints.Length - 2;
                }
            }
            else if (currentWaypoint < 0)
            {
                if (loop)
                {
                    currentWaypoint = waypoints.Length - 1;
                }
                else
                {
                    direction = 1;
                    currentWaypoint = 1;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            collision.collider.transform.SetParent(null);
        }
    }
}