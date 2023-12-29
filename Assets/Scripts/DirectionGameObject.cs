using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectionGameObject : MonoBehaviour
{
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;

    public void toUP() 
    {
        up = true;
        down = false;
        left = false;
        right = false;
    }
    public void toDOWN() 
    {
        up = false;
        down = true;
        left = false;
        right = false;
    }
    public void toLEFT() 
    {
        up = false;
        down = false;
        left = true; 
        right = false;
    }
    public void toRIGHT() 
    {
        up = false;
        down = false;
        left = false;
        right = true;
    }

    public bool getUP() { return up; }
    public bool getDOWN() { return down; }
    public bool getLEFT() { return left; }
    public bool getRIGHT() { return right; }

}
