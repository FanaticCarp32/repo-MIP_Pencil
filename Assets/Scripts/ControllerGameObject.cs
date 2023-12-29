using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGameObject : MonoBehaviour
{
    bool step = false;
    public new GameObject gameObject;
    DirectionGameObject direction;
    Vector3 newPos = Vector3.zero;
    Vector3 oldPos = Vector3.zero;
    static public bool animatorWorkGameObject = false;
    public AnimationCurve animationCurve;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = gameObject.transform.position;
        newPos = gameObject.transform.position;
        direction = gameObject.AddComponent<DirectionGameObject>();
        direction.toRIGHT();
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerPers.animatorWork == true)
        {
            step = true;
        }
        else
        {
            if (step && animatorWorkGameObject == false)
            {
                moveGameObject();
                step = false;
            }
        }
    }

    private void moveGameObject()
    {
        if (direction.getUP() == true)
        {
            oldPos = newPos;
            newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + ControllerPers.moveSpace);
        }
        else if (direction.getDOWN() == true)
        {
            oldPos = newPos;
            newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - ControllerPers.moveSpace);
        }
        else if (direction.getLEFT() == true)
        {
            oldPos = newPos;
            newPos = new Vector3(gameObject.transform.position.x - ControllerPers.moveSpace, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (direction.getRIGHT() == true)
        {
            oldPos = newPos;
            newPos = new Vector3(gameObject.transform.position.x + ControllerPers.moveSpace, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        StartCoroutine(animation());
    }

    IEnumerator animation()
    {
        animatorWorkGameObject = true;
        for (float i = 0; i < 1; i += Time.deltaTime * 2)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, animationCurve.Evaluate(i));
            yield return null;
        }

        transform.position = newPos;
        animatorWorkGameObject = false;

    }
}
