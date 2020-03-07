using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Player : NetworkBehaviour{

    [SyncVar] public int health = 1;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;

    Vector2 limitX = new Vector2(-14f, 14f); //Position limit in X
    Vector2 limitZ = new Vector2(-14f, 14f); //Position limit in Z

    //Player fire rate
    protected float fireRate = 0.40f;
    private double shootCooldown;

    int players;

    Color newRed = new Color(1.000f, 0.000f, 0.000f, 1.000f);
    Color newGreen = new Color(0.000f, 1.000f, 0.000f, 1.000f);
    Color newBlue = new Color(0.000f, 0.000f, 1.000f, 1.000f);
    public Color DeadColor   // property
    { get; set; }

    // Start is called before the first frame update
    void Start() {
        if (isLocalPlayer == false)
        {
            return;
        }

        shootCooldown = 0;

        Vector2 initialPos = new Vector2(Random.Range(-14, 14), Random.Range(-14, 14));
        transform.position = new Vector3(initialPos.x, transform.position.y, initialPos.y);
    }

    void Update()
    {
        if(isLocalPlayer == false)
        {
            return;
        }      

        //Capture movement
        float xMov = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        float zMov = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

        transform.Rotate(0, xMov, 0);
        transform.Translate(0, 0, zMov);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, limitX.x, limitX.y), transform.position.y, Mathf.Clamp(transform.position.z, limitZ.x, limitZ.y));

        // SyncState();

        //Check if player can shoot
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        //If spacebar is hit, we shoot a bullet
        if (Input.GetKeyDown(KeyCode.Space) && CanAttack)
        {
            shootCooldown = fireRate;
            CmdShootBullet();
        }

        /* if(Input.GetKeyDown("c")){
            Debug.Log("PlayerColor " + GetComponent<MeshRenderer>().material.color);
        } */
    }

    public bool CanAttack
    {
        //Check if the player can shoot by checking if the cooldwon has finished
        get
        {
            return shootCooldown <= 0;
        }
    }

    /* public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = newRed;
        players++;
    }

    public override void OnStartClient()
    {
        if(players<=1){
            GetComponent<MeshRenderer>().material.color = newBlue;
        }else{
            GetComponent<MeshRenderer>().material.color = newGreen;
        }
        players++;
        // gm.playersStartColor(GetComponent<MeshRenderer>(), players);
        // GetComponent<MeshRenderer>().material.color = Color.blue;
    } */

    //Method to shoot a bullet
    [Command]
    void CmdShootBullet()
    {
        //Spawn the bullet
        GameObject newBullet = (GameObject)Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);

        //Give the bullet velocity
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * 20f;

        //Spawn the bullet for other players
        NetworkServer.Spawn(newBullet);

        //Destroy the bullet after time passed
        Destroy(newBullet, 3f);
    }

    //Players takes damage
    public void GotShot(int damage)
    {
        
            health -= damage;
        
            //If health is equal to 0, the player dies
            if (health == 0)
            {
                //Debug.Log("Moriste " + health);
                
                //CustomNetworkManager.OnServerRemoveCustomPlayer(gameObject);
                //NetworkServer.Destroy(gameObject);
                //CustomNetworkManager.Disconnect();
                //CustomNetworkManager.StopClient();

                //CustomNetworkManager.cont--;

                // if (NetworkServer.active && NetworkClient.active) {
                //     CustomNetworkManager.singleton.StopHost();
                // } else if (!NetworkServer.active && NetworkClient.active) {
                    Debug.Log("Player died");
                    Destroy(gameObject);
                    //CustomNetworkManager.singleton.StopClient();
                // }

                
            }
        
        
    }

}