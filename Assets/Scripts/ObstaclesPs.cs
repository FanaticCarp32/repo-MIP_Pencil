using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPs : MonoBehaviour
{

    public GameObject gameObject;
    //public GameObject gameObjectMoving;
    //Transform[] objChilds = null;
    //Transform[] objChildsMoving = null;
    public Dictionary<Vector3, bool> posObj = new Dictionary<Vector3, bool>();
    //public Dictionary<Vector3, bool> posObjMoving = new Dictionary<Vector3, bool>();

    public Dictionary<Vector3, bool> GetPosObstacles()
    {
        foreach (var obj in gameObject.GetComponentsInChildren<Transform>())
        {
            if (obj.tag == "Obstacles")
            {
                //Debug.Log(obj.name + obj.transform.position);
                posObj.Add(obj.transform.position, false);
            }
        }
        return posObj;
    }

    //private void Awake()
    //{
        //foreach (var obj in gameObject.GetComponentsInChildren<Transform>())
        //{
        //    if (obj.tag == "Obstacles")
        //    {
        //        //Debug.Log(obj.name + obj.transform.position);
        //        posObj.Add(obj.transform.position, false);
        //    }
        //}

        //foreach (var obj in gameObjectMoving.GetComponentsInChildren<Transform>())
        //{
        //    if (obj.tag == "ObstaclesMoving")
        //    {
        //        //Debug.Log(obj.name + obj.transform.position);
        //        posObj.Add(obj.transform.position, false);
        //    }
        //}
    //}

    // Start is called before the first frame update
    //void Start()
    //{
        //Debug.Log(posObj[new Vector3(0.5f, 0, 0.5f)]);
        //gameObject = GetComponent<GameObject>();
        //objChilds = gameObject.GetComponentsInChildren<GameObject>();
        //for (int i = 0; i < objChilds.Length; i++)
        //{
        //Debug.Log("adwadawdwa" + objChilds[i].transform.GetChild(i).position);
        //posObj.Add(objChilds[i].transform.GetChild(i).position, true);
        //}

        //gameObjectMoving = GetComponent<GameObject>();
        //objChildsMoving = gameObjectMoving.GetComponentsInChildren<Transform>();
        //for (int i = 0; i < objChildsMoving.Length; i++)
        //{
        //    posObjMoving.Add(objChildsMoving[i].position, true);
        //}
    //}

}
