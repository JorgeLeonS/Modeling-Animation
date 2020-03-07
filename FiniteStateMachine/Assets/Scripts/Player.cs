﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    FSM fsm;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake() {
        fsm = GameObject.Find("Plane").GetComponent<FSM>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 my3DPos = transform.position;
        Vector2 myTexturePos = new Vector2(((my3DPos.x + 25.0f) * 512.0f) / 50.0f, ((my3DPos.z + 25.0f) * 512.0f) / 50.0f);
        for(int y=0; y<fsm.texture.height;y++){

            for(int x=0; x<fsm.texture.width;x++){

                Color color = Color.black;
                color.r = (myTexturePos.x - x)/512.0f;
                color.g = 0.0f;
                color.b =(myTexturePos.y - y)/512.0f;
                fsm.texture.SetPixel(x, y, color);
            }
        }
        fsm.texture.Apply();

    }
}
