using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRun : MonoBehaviour
{
    private DateTime startTime;
    private DateTime finishTime;
    private TimeSpan ResTime;
    private bool endReached = false;

    // Recuperation de temps écoulé du test
    public TimeSpan getResultTime()
    {   
        switch (endReached)
        {
            case true:
                //UnityEngine.Debug.Log("case: " + endReached);
                return ResTime;
                break;
            
            case false:
                //UnityEngine.Debug.Log("case: " + endReached);
                return TimeSpan.Zero;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {   
        // capture tu temps actuel
        finishTime = DateTime.Now;
        //UnityEngine.Debug.Log("on finish" + finishTime);

        startTime = GameObject.Find("Agent").GetComponent<NPCMove>().getStartTime();
        //UnityEngine.Debug.Log("on start" + startTime);

        // calcule du temps passé en simu jusqu'a arrêt de l'IA sur le point
        ResTime = finishTime - startTime;

        endReached = true;
        //UnityEngine.Debug.Log( getResultTime() + "secondes");
    }
}