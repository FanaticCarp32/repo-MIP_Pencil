using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GenerateBase : MonoBehaviour
{
    public GameObject plane;
    int rows, columns; float spaces;
    float width, height;
    float sx, sy, sz;
    static public Dictionary<Vector3, int> vector3s;

    // Start is called before the first frame update
    void Start()
    {
        rows = 5; columns = 5; spaces = 1;
        rows = TilemapBuilding.current.gridSizeX; columns = TilemapBuilding.current.gridSizeY;
        sx = 0.5f; sy = -0.041f; sz = 0.5f;
        vector3s = new Dictionary<Vector3, int>();
        int index = 0;
        for (float i = sx; i <= rows + 0.5f; i+=spaces)
        {
            for (float j = sz; j <= columns + 0.5f; j+=spaces)
            {
                Instantiate(plane, new Vector3(i, sy, j), Quaternion.identity);
                vector3s.Add(new Vector3(i, 0, j), 0);
                //Debug.Log(i + " " + sz + " " + j);
                index++;
            }
        }
        //ref List<Vector3> vector3sRef = ref vector3s;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
