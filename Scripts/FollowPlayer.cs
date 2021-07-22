using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Transform gameOverCollider;

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);

            gameOverCollider.position = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);
        }

    }
}
