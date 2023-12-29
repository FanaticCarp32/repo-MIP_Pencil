using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerPers : MonoBehaviour
{
    public AnimationCurve animationCurve;
    static public bool animatorWork = false;
    Vector3 StartPos, EndPos;
    Vector3 Mainpos;
    Vector3 MainposPrev;
    static public Vector3 mainposStatic;
    public readonly float yPers = 0.10f;
    public float xPers;
    public float zPers;
    static public float moveSpace = 1.0f;
    Dictionary<Vector3, int> vector3s1 = new Dictionary<Vector3, int>();
    Dictionary<Vector3, bool> obstaclesPos = new Dictionary<Vector3, bool>();
    AudioSource audio;
    public GameObject gameObject1;

    [SerializeField] Vector3 finish;

    ObstaclesPs obstaclesPs;
    ObstaclesMoving obstaclesMoving;
    // Start is called before the first frame update
    void Start()
    {
        obstaclesMoving = GetComponent<ObstaclesMoving>();
        obstaclesMoving.InitObstaclesMoving();
        obstaclesPs = GetComponent<ObstaclesPs>();
        obstaclesPos = obstaclesPs.GetPosObstacles();
        Mainpos = new Vector3(xPers, yPers, zPers);
        mainposStatic = Mainpos;
        transform.position = Mainpos;
        audio = GetComponent<AudioSource>();
        audio.Stop();
        StartCoroutine(a());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && animatorWork == false && ControllerGameObject.animatorWorkGameObject == false && obstaclesMoving.animWork == false)
        {
            
            EndPos = Input.GetTouch(0).position;
            //Debug.Log(StartPos + "  " + EndPos);
            float x = EndPos.x - StartPos.x;
            float y = EndPos.y - StartPos.y;
            if (Mathf.Abs(x) <= Mathf.Abs(y))
            {
                //up
                if (y > 100)
                {
                    if (vector3s1.ContainsKey(new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z + moveSpace))
                        && vector3s1[new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z + moveSpace)] == 0
                        && !obstaclesPos.ContainsKey(new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z + moveSpace))
                        && !obstaclesMoving.posObj.ContainsKey(new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z + moveSpace)))
                    {
                        MainposPrev = Mainpos;
                        Mainpos = new Vector3(Mainpos.x, Mainpos.y, Mainpos.z + moveSpace);
                        //transform.position = Mainpos;
                        StartCoroutine(animation());
                        vector3s1[new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z)] = 1;
                    }
                }
                //down
                else if (y < -100)
                {
                    Debug.Log(Mainpos);
                    if (vector3s1.ContainsKey(new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z - moveSpace))
                        && vector3s1[new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z - moveSpace)] == 0
                        && !obstaclesPos.ContainsKey(new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z - moveSpace))
                        && !obstaclesMoving.posObj.ContainsKey(new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z - moveSpace)))
                    {
                        MainposPrev = Mainpos;
                        Mainpos = new Vector3(Mainpos.x, Mainpos.y, Mainpos.z - moveSpace);
                        //transform.position = Mainpos;
                        StartCoroutine(animation());
                        vector3s1[new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z)] = 1;
                    }
                }
            }
            else
            {
                //right
                if (x > 100)
                {
                    Debug.Log(Mainpos);
                    if (vector3s1.ContainsKey(new Vector3(Mainpos.x + moveSpace, Mainpos.y - yPers, Mainpos.z))
                        && vector3s1[new Vector3(Mainpos.x + moveSpace, Mainpos.y - yPers, Mainpos.z)] == 0
                        && !obstaclesPos.ContainsKey(new Vector3(Mainpos.x + moveSpace, Mainpos.y - yPers, Mainpos.z))
                        && !obstaclesMoving.posObj.ContainsKey(new Vector3(Mainpos.x + moveSpace, Mainpos.y - yPers, Mainpos.z)))
                    {
                        MainposPrev = Mainpos;
                        Mainpos = new Vector3(Mainpos.x + moveSpace, Mainpos.y, Mainpos.z);
                        //transform.position = Mainpos;
                        StartCoroutine(animation());
                        vector3s1[new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z)] = 1;
                    }
                }
                //left
                else if (x < -100)
                {
                    Debug.Log(Mainpos);
                    if (vector3s1.ContainsKey(new Vector3(Mainpos.x - moveSpace, Mainpos.y - yPers, Mainpos.z))
                        && vector3s1[new Vector3(Mainpos.x - moveSpace, Mainpos.y - yPers, Mainpos.z)] == 0
                        && !obstaclesPos.ContainsKey(new Vector3(Mainpos.x - moveSpace, Mainpos.y - yPers, Mainpos.z))
                        && !obstaclesMoving.posObj.ContainsKey(new Vector3(Mainpos.x - moveSpace, Mainpos.y - yPers, Mainpos.z)))
                    {
                        MainposPrev = Mainpos;
                        Mainpos = new Vector3(Mainpos.x - moveSpace, Mainpos.y, Mainpos.z);
                        //transform.position = Mainpos;
                        StartCoroutine(animation());
                        vector3s1[new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z)] = 1;
                    }
                }
            }

            
        }
    }

    IEnumerator animation()
    {
        animatorWork = true;
        audio.Play();
        for (float i = 0; i < 1; i += Time.deltaTime*2)
        {
            transform.position = Vector3.Lerp(MainposPrev, Mainpos, animationCurve.Evaluate(i));
            yield return null;
        }

        transform.position = Mainpos;
        mainposStatic = Mainpos;
        animatorWork = false;
        audio.Stop();
        if (transform.position == finish)
        {
            //GameObject gameObject = GameObject.Find("ObjectCanvas");
            if (gameObject1 != null)
            {
                gameObject1.SetActive(true);
                //Time.timeScale = 0;
                //GameObject game = gameObject.transform.root.Find("TextTime").gameObject;
                //TextMeshPro textMeshPro = game.GetComponent<TextMeshPro>();
                //textMeshPro.text = "wdwdwdwdwdw";
            }
        }
        else
        {
            obstaclesMoving.ChangePosObj();
        }

    }
    IEnumerator a()
    {
        yield return new WaitUntil(() => GenerateBase.vector3s != null);
        vector3s1 = GenerateBase.vector3s;
        vector3s1[new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z)] = 1;
        //Debug.Log(vector3s1[new Vector3(Mainpos.x, Mainpos.y - y, Mainpos.z + moveSpace)]);
    }

    public Vector3 getPos()
    {
        return new Vector3(Mainpos.x, Mainpos.y - yPers, Mainpos.z);
    }

    public Vector3 getNowPos()
    {
        return new Vector3(transform.position.x, transform.position.y - yPers, transform.position.z);
    }

    public Vector3 getPosPrev()
    {
        return new Vector3(MainposPrev.x, MainposPrev.y - yPers, MainposPrev.z);
    }

}
