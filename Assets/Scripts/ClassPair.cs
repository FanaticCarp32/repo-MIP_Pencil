using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassPair<T, U> : MonoBehaviour
{
    public ClassPair()
    {
    }

    public ClassPair(T first, U second)
    {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
}
