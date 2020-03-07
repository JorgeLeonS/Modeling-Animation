using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // int players;

    public void playersStartColor(MeshRenderer mr, int players){
        if(players<=1){
            mr.material.color = Color.blue;
        }else{
            mr.material.color = Color.green;
        }
        players++;
    }

    public void playersChangeColor(MeshRenderer mr, int players){
        if(players<=1){
            mr.material.color = Color.blue;
        }else{
            mr.material.color = Color.green;
        }
        players++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
