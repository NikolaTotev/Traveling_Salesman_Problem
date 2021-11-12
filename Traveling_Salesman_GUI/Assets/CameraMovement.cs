using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform CameraTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        Movement();
    }

    void Zoom()
    {
        Vector3 newPos = CameraTransform.position;
        newPos.y -= Input.mouseScrollDelta.y * 10f;
        CameraTransform.position = newPos;

    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 newPos = CameraTransform.position;
        if (Input.GetKey("w"))
        {
            newPos.z -= 5f + z;
        }

        if (Input.GetKey("s"))
        {
            newPos.z += 5f + z;
        }

        if (Input.GetKey("d"))
        {
            newPos.x -= 5f + x;
        }

        if (Input.GetKey("a"))
        {
            newPos.x += 5f + x;

        }

        CameraTransform.position = newPos;
    }
}
