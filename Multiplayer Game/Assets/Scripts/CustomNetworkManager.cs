using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustomNetworkManager : NetworkManager
{
    // public int cont = 0;
    public int cont = 0;
    Player playColor;
    

    public override void OnServerAddPlayer(NetworkConnection conn, short playerId)
    {
        GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        if(cont == 0)
        {
            player.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if(cont == 1)
        {
            player.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if(cont == 2)
        {
            player.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            player.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        
        NetworkServer.AddPlayerForConnection(conn, player, playerId);
        
        Debug.Log(playerId);
        Debug.Log(playColor.DeadColor);

        Debug.Log(cont);
        cont++;
    }

    public override void OnClientDisconnect(NetworkConnection conn) {
        StopClient();
        cont--;
        Debug.Log(cont); 
        if (conn.lastError != NetworkError.Ok)
        {
            if (LogFilter.logError) { Debug.LogError("ClientDisconnected due to error: " + conn.lastError); }
        }
        Debug.Log("Client disconnected from server: " + conn);
    }
    
}
