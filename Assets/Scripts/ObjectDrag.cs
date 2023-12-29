using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool move = false;
    private Camera camera;
    public bool available = true;
    public Vector3 pos = Vector3.zero;
    public static bool isRecording { get; set; }
    MovingBuild moving;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        pos = transform.position;
        moving = GetComponent<MovingBuild>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            move = true;
        }
        else
        {
            move = false;
        }

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    offset = transform.position - TilemapBuilding.GetTouchWorldPos();
        //}

        //if (move)
        //{
        //    Vector3 pos = TilemapBuilding.GetTouchWorldPos() + offset;
        //    transform.position = TilemapBuilding.current.SnapCoordinateToGrid(pos);
        //}

        var groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (move)
        {
            Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                //int x = Mathf.RoundToInt(worldPosition.x);
                //int y = Mathf.RoundToInt(worldPosition.z);

                //Debug.Log(new Vector3(x + 0.5f, 0, y + 0.5f));
                //Debug.Log(transform.position);

                pos = TilemapBuilding.current.SnapCoordinateToGrid(worldPosition);

                Debug.Log(pos);
                transform.position = pos;

                if (isRecording)
                {
                    moving.Record();
                }

                //if (available)
                //{
                //    TilemapBuilding.current.curBuild = null;
                //}
            }
        }
    }
}
