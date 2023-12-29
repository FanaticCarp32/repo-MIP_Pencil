using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
    public GameObject GG;
    public Camera Camera;
    public Vector3 StartPos;
    public Vector3 rotate;
    public float speed;
    public float maxdist;
    public float mindistX;
    public float mindistZ;
    public Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Camera = Camera.main;
        Camera.transform.position = StartPos;
        Camera.transform.rotation = Quaternion.Euler(rotate);
    }

    // Update is called once per frame
    void Update()
    {
        Camera.transform.position = Vector3.SmoothDamp(Camera.transform.position, 
            new Vector3(GG.transform.position.x - mindistX, Camera.transform.position.y, (GG.transform.position.z - mindistZ)),
            ref velocity, speed);
    }
}
