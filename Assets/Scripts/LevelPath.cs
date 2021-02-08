using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelPath : MonoBehaviour
{
    public Transform [] followPoints;

    public Transform GetNextPoint(int index)
    {
        return followPoints[index];
    }
    
    // private void Awake()
    // {
    //     followPoints = new Transform[transform.childCount];
    //     for (int i = 0; i < followPoints.Length; i++)
    //     {
    //         followPoints[i] = transform.GetChild(i);
    //     }
    // }
}
