using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLvl : MonoBehaviour
{
    [SerializeField]
    int lvlCount;
    [SerializeField]
    float lengthSpace;
    Vector3 startPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;
    int currLvl;
    [SerializeField]
    Vector2 velocity;
    [SerializeField]
    float time, speed;
    Vector3 pos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        currLvl = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endPos = Input.GetTouch(0).position;
            float x = endPos.x - startPos.x;
            float y = endPos.y - startPos.y;
            if (Mathf.Abs(x) <= Mathf.Abs(y))
            {
                
            }
            else
            {
                //right
                if (x > 0)
                {
                    if (currLvl > 0)
                    {
                        currLvl--;
                    }
                }
                //left
                else
                {
                    
                    if (currLvl < lvlCount - 1)
                    {
                        currLvl++;
                    }
                }
                pos = new Vector2(-currLvl * lengthSpace, transform.localPosition.y);
                Debug.Log(pos + "  " + currLvl);
            }

            
        }
        
        
        transform.localPosition = Vector2.SmoothDamp(transform.localPosition, pos, ref velocity, time, speed);
    }

}
