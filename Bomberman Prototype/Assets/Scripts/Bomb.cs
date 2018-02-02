using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float explodeTimer = 5.0f;

    public bool didExplode;

    public RaycastHit hit;

    public GameObject bombPos;

    public PlayerController playerController;
    public PowerUps powerUps;

    public List<GameObject> particleList = new List<GameObject>();
    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        powerUps = GameObject.Find("PowerUp Manager").GetComponent<PowerUps>();

        for(int i =0; i<particleList.Count; i++)
        {
            particleList[i].transform.GetComponent<Renderer>().enabled = false;
            particleList[i].transform.GetComponent<ParticleSystem>().startLifetime *= playerController.bombRange;
        }

    }

    void Update () {
        explodeTimer -= 1 * Time.deltaTime;


        if (explodeTimer < 2.5f &&!didExplode)
        {
            Explode();
            didExplode = true;
            
        }
        if(explodeTimer < 0)
        {
            //destroys the bomb
            Destroy(transform.gameObject);

            //removes active bombs amount upon calling , allowing players to spawn new bombs
            playerController.activeBombs--;

            //removes transform's bombPosition from playercontroller's bombGrid to allow player to place new bomb on that position
            if (playerController.bombGrid[0] == bombPos)
            {
                playerController.bombGrid.Remove(bombPos);
            }
            else
            {
                playerController.bombGrid.Remove(bombPos);
            }
        }
	}


    void Explode()
    {
        //drawrays to check if range works properly.
        Debug.DrawRay(transform.position, transform.forward * playerController.bombRange,Color.red,5);
        Debug.DrawRay(transform.position, -transform.forward * playerController.bombRange,Color.red,5);
        Debug.DrawRay(transform.position, transform.right * playerController.bombRange,Color.red,5);
        Debug.DrawRay(transform.position, -transform.right * playerController.bombRange,Color.red,5);




        for (int i = 0; i < particleList.Count; i++) {
            particleList[i].GetComponent<Renderer>().enabled = true;
        }

        //shoots raycasts left right for and -backwards to check if it hits something
        //if an object with the tag box is hit, destroy that box
        if (Physics.Raycast(transform.position,transform.forward,out hit,playerController.bombRange))
        {
            if (hit.transform.tag == "Box")
            {
                Destroy(hit.transform.gameObject);
                powerUps.SpawnUpgrade(hit.transform.gameObject);
            }
            if (hit.transform.GetComponent<Bomb>())
            {
                hit.transform.GetComponent<Bomb>().explodeTimer = 2.5f;
            }
        }
        if (Physics.Raycast(transform.position, -transform.forward,out hit,playerController.bombRange))
        {
            if (hit.transform.tag == "Box")
            {
                Destroy(hit.transform.gameObject);
                powerUps.SpawnUpgrade(hit.transform.gameObject);
            }
            if (hit.transform.GetComponent<Bomb>())
            {
                hit.transform.GetComponent<Bomb>().explodeTimer = 2.5f;
            }
        }
        if (Physics.Raycast(transform.position,transform.right,out hit,playerController.bombRange))
        {
            if (hit.transform.tag == "Box")
            {
                Destroy(hit.transform.gameObject);
                powerUps.SpawnUpgrade(hit.transform.gameObject);
            }
            if (hit.transform.GetComponent<Bomb>())
            {
                hit.transform.GetComponent<Bomb>().explodeTimer = 2.5f;
            }
        }
        if (Physics.Raycast(transform.position, -transform.right,out hit,playerController.bombRange))
        {
  ;
            if (hit.transform.tag == "Box")
            {
                Destroy(hit.transform.gameObject);
                powerUps.SpawnUpgrade(hit.transform.gameObject);
            }
            if (hit.transform.GetComponent<Bomb>())
            {
                hit.transform.GetComponent<Bomb>().explodeTimer = 2.5f;
            }
        }
        
    }
}
