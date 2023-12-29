using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBuild : MonoBehaviour
{
    public List<Vector2> moving;
    public Vector2 movingStart;
    private Vector2 prevPos;
    Vector2Int building;
    private void Awake()
    {
        prevPos = new Vector2(transform.position.x, transform.position.z);
        building = GetComponent<Building>().size;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Record()
    {
        if (transform.position.x <= TilemapBuilding.current.gridSizeX - building.x + 0.5f
            && transform.position.x >= 0
            && transform.position.z <= TilemapBuilding.current.gridSizeY - building.y + 0.5f
            && transform.position.z >= 0)
        {
            if (prevPos != new Vector2(transform.position.x, transform.position.z) || moving.Count == 0)
            {
                moving.Add(new Vector2(transform.position.x, transform.position.z));
                prevPos = new Vector2(transform.position.x, transform.position.z);
            }
        }
        
    }
}
