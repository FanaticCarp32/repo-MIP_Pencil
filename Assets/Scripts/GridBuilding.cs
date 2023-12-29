using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBuilding : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);

    private Building[,] grid;
    private Building flyingBuilding;
    private Camera camera;
    private float time = 0.5f;
    private bool move = false;

    

    //private void OnDrawGizmos()
    //{
    //    for (int x = 0; x < gridSize.x; x++)
    //    {
    //        for(int y = 0; y < gridSize.y; y++)
    //        {
    //            Gizmos.color = Color.yellow;
    //            Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
    //        }
    //    }
    //}

    private void Awake()
    {
        camera = Camera.main;
        grid = new Building[gridSize.x, gridSize.y];
    }
    
    public void StartPlacingBuild(Building building)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(building);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            //{
            //    time -= Time.deltaTime;
            //    Debug.Log(time);
            //}

            //if (time < 0)
            //{
            //    move = true;
            //    time = 0.5f;
            //}

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                move = true;
            }
            else
            {
                move = false;
            }

            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            //{
            //    move = false;
            //    time = 0.5f;
            //}

            if (move)
            {
                Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);

                if (groundPlane.Raycast(ray, out float position))
                {
                    Vector3 worldPosition = ray.GetPoint(position);

                    int x = Mathf.RoundToInt(worldPosition.x);
                    int y = Mathf.RoundToInt(worldPosition.z);

                    flyingBuilding.transform.position = new Vector3(x, 0, y);
                }
            }
            
        }
    }
}
