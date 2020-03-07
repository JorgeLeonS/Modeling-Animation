using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public Texture2D texture;

    private void Awake() {
        
    }

    void Start()
    {
        texture = new Texture2D(512, 512, TextureFormat.RGBAFloat, false);
        GetComponent<Renderer>().material.mainTexture = texture;

        /* for(int y=0; y<texture.height;y++){
            for(int x=0; x<texture.width;x++){
                Color color = ((x == y) ? Color.white : Color.black);
                texture.SetPixel(x, y, color);
            }
        } */
        texture.Apply(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
