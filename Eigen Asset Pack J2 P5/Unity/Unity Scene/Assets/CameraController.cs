using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed = 5.0f;
    public GameObject cam;

    public float horizontalSpeed;
    public float verticalSpeed;

	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

        Move();
        Rotate();
	}

    void Move()
    {
        transform.Translate(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, Input.GetAxis("Vertical")*speed*Time.deltaTime);
    }

    void Rotate()
    {
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, h, 0);

            float v = verticalSpeed * -Input.GetAxis("Mouse Y");
            cam.transform.Rotate(v, 0, 0);

    }
}
