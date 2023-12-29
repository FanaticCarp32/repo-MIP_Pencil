using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObstaclesMoving : MonoBehaviour
{
    public AnimationCurve animationCurve;
    List<ClassPair<Transform, List<Vector2>>> listPos = new List<ClassPair<Transform, List<Vector2>>>();
    //Dictionary<Vector3, List<Vector2>> pos = new Dictionary<Vector3, List<Vector2>>();
    public Dictionary<Vector3, bool> posObj = new Dictionary<Vector3, bool>();
    public GameObject parent;
    Vector2 startMove = Vector2.zero;
    //Vector3 key = Vector3.zero;
    public bool animWork = false;
    public GameObject game;
    //GameObject[] obstacles = null;

    //private void Awake()
    //{
        //obstacles = parent.GetComponentsInChildren<GameObject>();
        //for (int i = 0; i < obstacles.Length; i++)
        //{
        //    pos.Add(obstacles[i].transform.position, obstacles[i].GetComponent<MovingBuild>().moving);
        //}
        //foreach (var obj in parent.GetComponentsInChildren<Transform>())
        //{
        //    if (obj.tag == "ObstaclesMoving")
        //    {

        //        //Debug.Log(obj.name + obj.GetComponent<MovingBuild>().moving[0]);
        //        pos.Add(obj.transform.position, obj.GetComponent<MovingBuild>().moving);
        //    }
        //}
    //}

    public void InitObstaclesMoving()
    {
        foreach (var obj in parent.GetComponentsInChildren<Transform>())
        {
            if (obj.tag == "ObstaclesMoving")
            {
                obj.GetComponent<AudioSource>().Stop();
                List<Vector2> list = new List<Vector2>();
                list = obj.GetComponent<MovingBuild>().moving;
                startMove = obj.GetComponent<MovingBuild>().movingStart;
                listPos.Add(new ClassPair<Transform, List<Vector2>>(obj, list));
                obj.transform.position = new Vector3(list[0].x, 0, list[0].y);
                posObj.Add(obj.transform.position, false);
                Debug.Log(obj.name);
                //for (int i = 0; i < pos[obj.transform.position].Count; i++)
                //{
                //    posObj.Add(pos[obj.transform.position][i], false);
                //}
                //key = transform.position;


                //transform.position = new Vector3(pos[transform.position][0].x, 0, pos[transform.position][0].y);
            }
        }
    }

    public void ChangePosObj()
    {
        for (int i = 0; i < listPos.Count; i++)
        {
            startMove = listPos[i].First.GetComponent<MovingBuild>().movingStart;
            for (int j = 0; j < listPos[i].Second.Count; j++)
            {
                if (new Vector3(listPos[i].Second[j].x, 0, listPos[i].Second[j].y) == listPos[i].First.transform.position)
                {
                    if (j + 1 >= listPos[i].Second.Count)
                    {
                        if (startMove == listPos[i].Second[0])
                        {
                            listPos[i].First.transform.position = new Vector3(listPos[i].Second[0].x, 0, listPos[i].Second[0].y);
                            Debug.Log(listPos[i].First.name);
                            break;
                        }
                        else
                        {
                            StartCoroutine(Anim(i, 0, listPos[i].First.transform.position, new Vector3(listPos[i].Second[0].x, 0, listPos[i].Second[0].y)));
                            break;
                        }
                    }
                    else if (startMove == listPos[i].Second[j + 1])
                    {
                        listPos[i].First.transform.position = new Vector3(listPos[i].Second[j + 1].x, 0, listPos[i].Second[j + 1].y);
                        Debug.Log(listPos[i].First.name);
                        break;
                    }
                    else
                    {
                        StartCoroutine(Anim(i, j + 1, listPos[i].First.transform.position, new Vector3(listPos[i].Second[j + 1].x, 0, listPos[i].Second[j + 1].y)));
                        //listPos[i].First.transform.position = listPos[i].Second[j + 1];
                        Debug.Log(startMove + "   " + listPos[i].Second[j + 1]);
                        break;
                    }
                }
            }
            
            //for (int i = 0; i < pos[obj.transform.position].Count; i++)
            //{
            //    posObj.Add(pos[obj.transform.position][i], false);
            //}
            //key = transform.position;


            //transform.position = new Vector3(pos[transform.position][0].x, 0, pos[transform.position][0].y);

        }
    }

    private IEnumerator Anim(int i, int j, Vector3 Ppos, Vector3 Mpos)
    {
        posObj.Remove(Ppos);
        posObj.TryAdd(Mpos, false);

        animWork = true;
        listPos[i].First.GetComponent<AudioSource>().Play();
        for (float y = 0; y < 1; y += Time.deltaTime * 2)
        {
            listPos[i].First.transform.position = Vector3.Lerp(Ppos, Mpos, animationCurve.Evaluate(y));
            yield return null;
        }
        
        listPos[i].First.transform.position = Mpos;
        animWork = false;
        listPos[i].First.GetComponent<AudioSource>().Stop();

        if (new Vector3(ControllerPers.mainposStatic.x, 0, ControllerPers.mainposStatic.z) == Mpos)
        {
            Debug.Log("You loss");
            game.SetActive(true);
            MenuManager.Pause();
            gameObject.SetActive(false);
        }

        Debug.Log(posObj.Count);
        foreach (var dict in posObj)
        {
            Debug.Log(dict);
        }
    }
}
