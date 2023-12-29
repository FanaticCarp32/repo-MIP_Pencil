using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool placed {  get; private set; }
    public BoundsInt boundsInt { get; private set; }

    public virtual Vector3Int BuildSize ()
    {
        Building building = gameObject.GetComponent<Building>();
        return new Vector3Int(building.size.x, building.size.y, 1);
    }

    public virtual Vector3Int BuildPos()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        return new Vector3Int(Mathf.FloorToInt(drag.pos.x), Mathf.FloorToInt(drag.pos.z));
    }

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Building building = gameObject.GetComponent<Building>();
        bool available = true;
        Vector3 pos = drag.pos;
        //if (pos.x <= TilemapBuilding.current.gridSizeX - building.size.x + 0.5f 
        //    && pos.x >= (-1 * TilemapBuilding.current.gridSizeX + building.size.x - 0.5f)
        //    && pos.z <= TilemapBuilding.current.gridSizeY - building.size.y + 0.5f
        //    && pos.z >= (-1 * TilemapBuilding.current.gridSizeY + building.size.y - 0.5f))
        //{
        //    available = true;
        //}
        if (pos.x <= TilemapBuilding.current.gridSizeX - building.size.x + 1.5f
            && pos.x >= 0
            && pos.z <= TilemapBuilding.current.gridSizeY - building.size.y + 1.5f
            && pos.z >= 0)
        {
            available = true;
        }
        else
        {
            available = false;
        }

        if (available)
        {
            Destroy(drag);
            placed = true;
        }
        else
        {
            placed = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
