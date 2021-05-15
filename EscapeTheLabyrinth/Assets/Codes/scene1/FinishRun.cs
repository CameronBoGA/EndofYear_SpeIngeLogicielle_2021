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

    public TimeSpan getResultTime()
    {
        switch (endReached)
        {
            case true:
                UnityEngine.Debug.Log("case: " + endReached);
                return ResTime;
                break;
            
            case false:
                UnityEngine.Debug.Log("case: " + endReached);
                return TimeSpan.Zero;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        finishTime = DateTime.Now;

        startTime = GameObject.Find("Agent").GetComponent<NPCMove>().getStartTime();

        ResTime = finishTime - startTime;

        endReached = true;
        

        //UnityEngine.Debug.Log("start" + startTime);
        //UnityEngine.Debug.Log("finish" + finishTime);
        //UnityEngine.Debug.Log("ResTime " + ResTime.TotalSeconds + " Secondes");
    }
}