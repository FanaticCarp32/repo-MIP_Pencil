using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBuilding : MonoBehaviour
{
    public static TilemapBuilding current;

    public GridLayout gridLayout;
    private Grid grid;
    public int gridSizeX;
    public int gridSizeY;
    public Vector3[] gridPosition;
    private TileBase[] tileBase;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase whiteTile;
    [SerializeField] private TileBase blackTile;

    //public GameObject prefab1;
    //public GameObject prefab2;

    private PlaceableObject objectToPlace;

    #region Unity methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
        //for (int x = 0; x < 100; x++)
        //{
        //    for (int y = 0; y < 100; y++)
        //    {
        //        Debug.Log(SnapCoordinateToGrid(new Vector3(x, 0, y)));
        //    }
        //}

        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(Vector3Int.zero);
        area.size = new Vector3Int(gridSizeX, gridSizeY, 1);
        tileBase = GetTileBases(area, tilemap).First;
        gridPosition = GetTileBases(area, tilemap).Second;
        foreach (var indexer in gridPosition)
        {
            Debug.Log(indexer);
        }

        //BoundsInt boundsInt = new BoundsInt();
        //boundsInt.position = new Vector3Int();
        TakeArea(area.position, area.size, whiteTile);
        //Debug.Log(a[0].GetTileData();
    }

    #endregion

    #region Utils

    public GameObject curBuild = null;

    public static Vector3 GetTouchWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    public void InitializeWithObject(GameObject go)
    {
        if (objectToPlace != null && !objectToPlace.placed)
        {
            Destroy(objectToPlace.gameObject);
        }
        Vector3 pos = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(go, pos, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }

    //public bool CanbePlaced(PlaceableObject place)
    //{
    //    if ()
    //    {

    //    }
    //}

    //public void EnterBuilding(GameObject go)
    //{

    //}

    private static ClassPair<TileBase[], Vector3[]> GetTileBases(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int count = 0;
        Vector3[] arrayPos = new Vector3[area.size.x * area.size.y * area.size.z];
        foreach (var i in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(i.x, i.y, 0);
            arrayPos[count] = new Vector3(pos.x + 0.5f, 0, pos.y + 0.5f);
            array[count] = tilemap.GetTile(pos);
            count++;
        }
        return new ClassPair<TileBase[], Vector3[]> (array, arrayPos);
    }

    public void TakeArea(Vector3Int start, Vector3Int size, TileBase tileBase)
    {
        tilemap.BoxFill(start, tileBase, start.x, start.y, start.x + size.x, start.y + size.y);
    }

    public void OnEnterBuild()
    {
        if (!objectToPlace) { return; }
        BoundsInt boundsInt = new BoundsInt();
        boundsInt.position = objectToPlace.BuildPos();
        boundsInt.size = objectToPlace.BuildSize();
        TileBase[] baseArray = GetTileBases(boundsInt, tilemap).First;
        
        foreach (var i in baseArray)
        {
            if (i == blackTile)
            {
                return;
            }
        }

        objectToPlace.Place();
        if (objectToPlace.placed == true)
        {
            
            Debug.Log(objectToPlace.BuildSize());
            Debug.Log(objectToPlace.BuildPos());
            TakeArea(boundsInt.position, new Vector3Int(boundsInt.size.x - 1, boundsInt.size.y - 1, 1), blackTile);
        }
    }

    public void OnMoving()
    {
        ObjectDrag.isRecording = !ObjectDrag.isRecording;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
