using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player playColor;

    void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        playColor.DeadColor = collision.gameObject.GetComponent<MeshRenderer>().material.color;

        //If player is not null, it takes damage
        if (player != null)
        {
            player.GotShot(1);
        }

        // Debug.Log(deadColor);

        Destroy(gameObject);
    }
}
