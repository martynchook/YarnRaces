using System;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    //private Dictionary<int, String> scoreBord;
    private int finishPlace;

    private void Start()
    {
        finishPlace = 0;
    }
    
    public int GetPlace()
    {
        finishPlace++;
        return finishPlace;
    }
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.GetComponent<PlayerController>())
    //     {
    //         scoreBord.Add(other.GetComponent<PlayerController>().GetPlace(), other.GetComponent<PlayerController>().GetName());
    //     }
    //     if (other.GetComponent<EnemyController>())
    //     {
    //         scoreBord.Add(other.GetComponent<EnemyController>().GetPlace(), other.GetComponent<EnemyController>().GetName());  
    //     }
    // }
}