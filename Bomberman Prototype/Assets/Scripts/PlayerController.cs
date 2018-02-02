using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;

    public int bombLimit;
    public int bombRange;
    public int activeBombs;

    public bool spawned;

    public List<GameObject> bombGrid = new List<GameObject>();

    public GameObject bomb;

    public RaycastHit hit;

    public PowerUps powerups;


	void Update () {
        Move();
        PlaceBomb();
	}

    private void LateUpdate()
    {
        spawned = true;
    }

    void Move()
    {
        //transform.Translate(new Vector3(Input.GetAxis("Horizontal")*speed *Time.deltaTime, 0, Input.GetAxis("Vertical")*speed *Time.deltaTime));
        transform.GetComponent<Rigidbody>().velocity = transform.right * speed * Input.GetAxis("Horizontal") + transform.forward * speed * Input.GetAxis("Vertical");
        if(Input.GetAxis("Horizontal") ==0 && Input.GetAxis("Vertical") == 0)
        {
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void PlaceBomb()
    {
        if (Input.GetButtonDown("Jump") && activeBombs < bombLimit)
        {
            if (Physics.Raycast(transform.position, -Vector3.up * 10, out hit)) {

                if (hit.transform.tag == "Tile")
                {
                    if (bombGrid.Count <1){
                        Instantiate(bomb, new Vector3(hit.transform.position.x, hit.transform.position.y + 5, hit.transform.position.z), Quaternion.identity);
                        activeBombs++;

                        bombGrid.Add(hit.transform.gameObject);

                        bomb.GetComponent<Bomb>().bombPos = hit.transform.gameObject;
                    }
                    else if (!bombGrid.Contains(hit.transform.gameObject))
                    {
                        Instantiate(bomb, new Vector3(hit.transform.position.x, hit.transform.position.y + 5, hit.transform.position.z), Quaternion.identity);
                        activeBombs++;

                        bombGrid.Add(hit.transform.gameObject);

                        bomb.GetComponent<Bomb>().bombPos = hit.transform.gameObject;
                    }
                }
            }
            else
            {
                print("Can't place bomb here");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Box" &&!spawned)
        {
            Destroy(collision.gameObject);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PowerUp")
        {
            powerups.TriggerUpgrade(other.transform);
        }
    }

}
