using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar] public int health = 1;

    //Players takes damage
    public void GotShot(int damage)
    {
        
            health -= damage;
        
            //If health is equal to 0, the player dies
            if (health == 0)
            {
                //Debug.Log("Moriste " + health);
                //Destroy(gameObject);
                //CustomNetworkManager.OnServerRemoveCustomPlayer(gameObject);
                //NetworkServer.Destroy(gameObject);
                //CustomNetworkManager.Disconnect();
                //CustomNetworkManager.StopClient();

                //CustomNetworkManager.cont--;

                // if (NetworkServer.active && NetworkClient.active) {
                //     CustomNetworkManager.singleton.StopHost();
                // } else if (!NetworkServer.active && NetworkClient.active) {
                    CustomNetworkManager.singleton.StopClient();
                // }

                
            }
        
        
    }
}
